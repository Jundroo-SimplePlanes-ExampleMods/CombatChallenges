namespace Assets.Scripts.Levels.ConvoyChaos
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Common;
   using SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
   using UnityEngine;

   /// <summary>
   /// A component that wraps the some of the reflection proxy objects for convoys,
   /// allowing them to be spawned and configured via this script.
   /// </summary>
   /// <seealso cref="UnityEngine.MonoBehaviour" />
   public class ConvoySpawner : MonoBehaviour
   {
      /// <summary>
      /// A value indicating whether or not the convoy is hostile.
      /// </summary>
      [SerializeField]
      private bool _isHostile = true;

      /// <summary>
      /// A value indicating whether or not the spawner should add spawned vehicles to a parent script monitoring for destruction of objectives.
      /// </summary>
      [SerializeField]
      private bool _monitorForDestruction = true;

      /// <summary>
      /// The target velocity of the convoy.
      /// </summary>
      [SerializeField]
      private float _targetVelocity = 20f;

      /// <summary>
      /// The vehicle types and order that make up the convoy.
      /// </summary>
      [SerializeField]
      private ConvoyVehicleType[] _vehicles = null;

      /// <summary>
      /// The waypoints for the convoy.
      /// </summary>
      [SerializeField]
      private Transform[] _waypoints = null;

      /// <summary>
      /// Used to draw gizmos that are pickable and always drawn.
      /// </summary>
      protected virtual void OnDrawGizmos()
      {
         if (this._waypoints == null || this._waypoints.Length < 2)
         {
            return;
         }

         Gizmos.color = Color.yellow;

         var previous = this._waypoints[0];
         if (previous != null)
         {
            Gizmos.DrawWireSphere(previous.position, 1f);
         }

         for (int i = 1; i < this._waypoints.Length; i++)
         {
            var current = this._waypoints[i];

            if (current == null)
            {
               continue;
            }

            Gizmos.DrawWireSphere(current.position, 1f);

            if (previous == null)
            {
               continue;
            }

            Gizmos.DrawLine(previous.position, current.position);

            previous = current;
         }
      }

      /// <summary>
      /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
      /// </summary>
      protected virtual void Start()
      {
         var convoyProvider = BasicConvoyProviderScript.AddComponent(this.gameObject);
         convoyProvider.ConfigureConvoy(this._vehicles);

         var convoy = ConvoyScript.AddComponent(this.gameObject);
         convoy.SetConvoyProvider(convoyProvider);
         convoy.Initialize(this._waypoints, this._isHostile);

         if (this._monitorForDestruction)
         {
            this.StartCoroutine(this.LateInitializationCoroutine());
         }
      }

      /// <summary>
      /// The coroutine that handles some late initialization for the spawner.
      /// </summary>
      /// <returns>The enumerator for the coroutine.</returns>
      private IEnumerator LateInitializationCoroutine()
      {
         yield return null;
         yield return null;

         EnemyDestructionMonitorScript monitor = null;
         if (this._monitorForDestruction)
         {
            monitor = this.GetComponentInParent<EnemyDestructionMonitorScript>();
            if (monitor == null)
            {
               Debug.LogError("Unable to find the enemy destruction monitor script", this);
            }
         }

         var vehicles = SimpleGroundVehicleScript.GetComponentsInChildren(this);
         for (int i = 0; i < vehicles.Length; i++)
         {
            var vehicle = vehicles[i];

            if (monitor != null)
            {
               monitor.AddObjective(vehicle);
            }

            vehicle._TargetVelocity = this._targetVelocity;
            vehicle._Speed = 500f;
         }
      }
   }
}