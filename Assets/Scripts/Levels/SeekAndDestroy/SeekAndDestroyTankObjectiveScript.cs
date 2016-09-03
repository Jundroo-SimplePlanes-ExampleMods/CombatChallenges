namespace Assets.Scripts.Levels.SeekAndDestroy
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Jundroo.SimplePlanes.ModTools.PrefabProxies;
   using SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
   using SimplePlanesReflection.Assets.Scripts.Parts.Targeting;
   using UnityEngine;

   public class SeekAndDestroyTankObjectiveScript : MonoBehaviour
   {
      /// <summary>
      /// The configuration script for the level.
      /// </summary>
      private SeekAndDestroyLevelConfigScript _config;

      /// <summary>
      /// A value indicating whether this objective has been discovered by the player.
      /// </summary>
      private bool _discovered;

      /// <summary>
      /// A value indicating whether this objective is destroyed.
      /// </summary>
      private bool _isDestroyed;

      /// <summary>
      /// A value indicating whether this objective is hostile to the player.
      /// </summary>
      private bool _isHostile;

      /// <summary>
      /// The reflection proxy for the real tank script.
      /// </summary>
      private AntiAircraftTankScript _tank;

      /// <summary>
      /// The tank proxy script.
      /// </summary>
      private AntiAircraftTankProxy _tankProxy;

      /// <summary>
      /// The target use by the player's targeting system to represent the tank.
      /// </summary>
      private GroundTarget _target;

      /// <summary>
      /// Occurs when the objective is destroyed.
      /// </summary>
      public event EventHandler<EventArgs> Destroyed;

      /// <summary>
      /// Awake is called when the script instance is being loaded.
      /// </summary>
      protected virtual void Awake()
      {
         this._tankProxy = this.GetComponent<AntiAircraftTankProxy>();
         if (this._tankProxy == null)
         {
            Debug.LogErrorFormat("Could not find the anti aircraft tank proxy script on game object '{0}'", this.name);
            this.gameObject.SetActive(false);
            return;
         }

         this._config = this.GetComponentInParent<SeekAndDestroyLevelConfigScript>();
         if (this._config == null)
         {
            Debug.LogErrorFormat("Could not find the level configuration script.", this.name);
            this.gameObject.SetActive(false);
            return;
         }

         this.StartCoroutine(this.InitializeCoroutine());
      }

      /// <summary>
      /// Used to draw gizmos that are pickable and always drawn.
      /// </summary>
      protected virtual void OnDrawGizmos()
      {
         if (this._config == null)
         {
            this._config = this.GetComponentInParent<SeekAndDestroyLevelConfigScript>();
            if (this._config == null)
            {
               return;
            }
         }

         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(this.transform.position, this._config.GoHostileRange);

         Gizmos.color = Color.blue;
         Gizmos.DrawWireSphere(this.transform.position, this._config.DiscoverableRange);
      }

      /// <summary>
      /// Update is called every frame, if the MonoBehaviour is enabled.
      /// </summary>
      protected virtual void Update()
      {
         if (!this._discovered || !this._isHostile)
         {
            var distance = (this.transform.position - ServiceProvider.Instance.PlayerAircraft.MainCockpitPosition).magnitude;

            if (!this._isHostile && distance <= this._config.GoHostileRange)
            {
               this._isHostile = true;
               this._tank.IsHostile = true;
            }

            if (!this._discovered && distance <= this._config.DiscoverableRange)
            {
               this._discovered = true;
               this._target.MaxVisibleRange = 15000f;
            }
         }
      }

      /// <summary>
      /// The coroutine that handles the monitoring of the objective, checking when it is destroyed.
      /// </summary>
      /// <returns>The enumerator for the coroutine.</returns>
      private IEnumerator DestroyedCheckCoroutine()
      {
         while (!this._isDestroyed)
         {
            yield return new WaitForSeconds(0.5f);

            if (this._tankProxy.IsDead)
            {
               this._isDestroyed = true;
               this.OnDestroyed();
            }
         }
      }

      /// <summary>
      /// The coroutine that handles initialization.
      /// </summary>
      /// <returns>The enumerator for the coroutine.</returns>
      private IEnumerator InitializeCoroutine()
      {
         while (this._target == null || this._target.RealObject == null)
         {
            if (this._tank == null || this._tank.RealObject == null)
            {
               object tankScript = null;
               if (this._tankProxy.RealObject != null)
               {
                  tankScript = this._tankProxy.RealObject.GetComponent(AntiAircraftTankScript.RealType);
               }

               this._tank = AntiAircraftTankScript.Wrap(tankScript);
               if (this._tank.RealObject == null)
               {
                  continue;
               }
            }

            this._target = this._tank._groundTarget;

            yield return null;
         }

         this._tank.IsHostile = false;
         this._target.MaxVisibleRange = this._config.DiscoverableRange;

         this.StartCoroutine(this.DestroyedCheckCoroutine());
      }

      /// <summary>
      /// Called when the objective is destroyed.
      /// </summary>
      private void OnDestroyed()
      {
         var handler = this.Destroyed;
         if (handler != null)
         {
            handler(this, new EventArgs());
         }
      }
   }
}