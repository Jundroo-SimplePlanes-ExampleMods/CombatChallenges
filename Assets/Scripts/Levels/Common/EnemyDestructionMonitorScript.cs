namespace Assets.Scripts.Levels.Common
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Jundroo.SimplePlanes.ModTools.PrefabProxies;
   using SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
   using UnityEngine;

   /// <summary>
   /// A script that monitors multiple enemies, setting a flag when they are all destroyed.
   /// </summary>
   /// <seealso cref="UnityEngine.MonoBehaviour" />
   public class EnemyDestructionMonitorScript : MonoBehaviour
   {
      /// <summary>
      /// The ground vehicles to monitor.
      /// </summary>
      private List<SimpleGroundVehicleScript> _groundVehicles;

      /// <summary>
      /// The ships that must be defeated in order to complete the level.
      /// </summary>
      [SerializeField]
      private ShipProxy[] _objectiveShips = null;

      /// <summary>
      /// The tanks that must be defeated in order to complete the level.
      /// </summary>
      [SerializeField]
      private AntiAircraftTankProxy[] _objectiveTanks = null;

      /// <summary>
      /// The turrets that must be defeated in order to complete the level.
      /// </summary>
      [SerializeField]
      private RotatingWeaponProxy[] _objectiveTurrets = null;

      /// <summary>
      /// Gets a value indicating whether all objectives have been destroyed.
      /// </summary>
      /// <value>
      /// <c>true</c> if all objectives have been destroyed; otherwise, <c>false</c>.
      /// </value>
      public bool AllObjectivesDestroyed { get; private set; }

      /// <summary>
      /// Gets the total number of objective targets.
      /// </summary>
      /// <value>
      /// The total number of objective targets.
      /// </value>
      public int ObjectivesTotalCount
      {
         get
         {
            return
               this._objectiveShips.Length +
               this._objectiveTanks.Length +
               this._objectiveTurrets.Length +
               (this._groundVehicles == null ? 0 : this._groundVehicles.Count);
         }
      }

      /// <summary>
      /// Adds a ship objective to monitor.
      /// </summary>
      /// <param name="ship">The ship objective to be added.</param>
      public void AddObjective(ShipProxy ship)
      {
         if (this._objectiveShips == null)
         {
            this._objectiveShips = new ShipProxy[1] { ship };
            return;
         }

         Array.Resize(ref this._objectiveShips, this._objectiveShips.Length + 1);
         this._objectiveShips[this._objectiveShips.Length - 1] = ship;
      }

      /// <summary>
      /// Adds a tank objective to monitor.
      /// </summary>
      /// <param name="tank">The tank objective to be added.</param>
      public void AddObjective(AntiAircraftTankProxy tank)
      {
         if (this._objectiveTanks == null)
         {
            this._objectiveTanks = new AntiAircraftTankProxy[1] { tank };
            return;
         }

         Array.Resize(ref this._objectiveTanks, this._objectiveTanks.Length + 1);
         this._objectiveTanks[this._objectiveTanks.Length - 1] = tank;
      }

      /// <summary>
      /// Adds the ground vehicle objective to monitor.
      /// </summary>
      /// <param name="vehicle">The vehicle objective to be added.</param>
      public void AddObjective(SimpleGroundVehicleScript vehicle)
      {
         if (this._groundVehicles == null)
         {
            this._groundVehicles = new List<SimpleGroundVehicleScript>();
         }

         this._groundVehicles.Add(vehicle);
      }

      /// <summary>
      /// Adds a turret objective to monitor.
      /// </summary>
      /// <param name="turret">The turret objective to be added.</param>
      public void AddObjective(RotatingWeaponProxy turret)
      {
         if (this._objectiveTurrets == null)
         {
            this._objectiveTurrets = new RotatingWeaponProxy[1] { turret };
            return;
         }

         Array.Resize(ref this._objectiveTurrets, this._objectiveTurrets.Length + 1);
         this._objectiveTurrets[this._objectiveTurrets.Length - 1] = turret;
      }

      /// <summary>
      /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
      /// </summary>
      protected virtual void Start()
      {
         this.StartCoroutine(this.MonitorLevelObjectivesCoroutine());
      }

      /// <summary>
      /// The coroutine that monitors the level objectives.
      /// </summary>
      /// <returns>The enumerator for the coroutine.</returns>
      private IEnumerator MonitorLevelObjectivesCoroutine()
      {
         while (true)
         {
            //yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 5; i++)
            {
               yield return null;
            }

            var allDestroyed = true;

            if (this._objectiveTanks != null)
            {
               for (int i = 0; i < this._objectiveTanks.Length; i++)
               {
                  if (this._objectiveTanks[i] != null && !this._objectiveTanks[i].IsDead)
                  {
                     allDestroyed = false;
                     break;
                  }
               }
            }

            if (this._objectiveShips != null)
            {
               for (int i = 0; i < this._objectiveShips.Length; i++)
               {
                  if (this._objectiveShips[i] != null && !this._objectiveShips[i].IsCriticallyDamaged)
                  {
                     allDestroyed = false;
                     break;
                  }
               }
            }

            if (this._objectiveTurrets != null)
            {
               for (int i = 0; i < this._objectiveTurrets.Length; i++)
               {
                  if (this._objectiveTurrets[i] != null && !this._objectiveTurrets[i].IsDisabled)
                  {
                     allDestroyed = false;
                     break;
                  }
               }
            }

            if (this._groundVehicles != null)
            {
               for (int i = 0; i < this._groundVehicles.Count; i++)
               {
                  if (this._groundVehicles[i] != null && !this._groundVehicles[i].IsDestroyed)
                  {
                     allDestroyed = false;
                     break;
                  }
               }
            }

            if (allDestroyed)
            {
               this.AllObjectivesDestroyed = true;
               ////break;
            }
         }
      }
   }
}