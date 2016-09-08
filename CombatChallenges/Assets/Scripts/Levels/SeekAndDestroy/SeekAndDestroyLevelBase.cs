namespace Assets.Scripts.Levels.SeekAndDestroy
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Common;
   using Jundroo.SimplePlanes.ModTools;
   using Jundroo.SimplePlanes.ModTools.PrefabProxies;
   using UnityEngine;

   /// <summary>
   /// A base class for seek and destroy type levels.
   /// </summary>
   /// <seealso cref="Jundroo.SimplePlanes.ModTools.Level" />
   public abstract class SeekAndDestroyLevelBase : Level
   {
      /// <summary>
      /// The name of the root game object for the level so that it can be loaded from the mod.
      /// </summary>
      private string _levelGameObjectName;

      /// <summary>
      /// The number of targets that have been destroyed so far.
      /// </summary>
      private int _targetsDestroyed;

      /// <summary>
      /// Initializes a new instance of the <see cref="SeekAndDestroyLevelBase"/> class.
      /// </summary>
      /// <param name="levelName">The name of the level.</param>
      /// <param name="levelDescription">The level description.</param>
      /// <param name="levelGameObjectName">The name of the root game object for the level so that it can be loaded from the mod.</param>
      public SeekAndDestroyLevelBase(string levelName, string levelDescription, string levelGameObjectName)
         : base(levelName, MapNames.Default, levelDescription)
      {
         this._levelGameObjectName = levelGameObjectName;
      }

      /// <summary>
      /// Gets the number of targets to be destroyed in order to complete the level.
      /// </summary>
      /// <value>
      /// The number of targets to be destroyed in order to complete the level.
      /// </value>
      public abstract int NumberOfTargets { get; }

      /// <summary>
      /// Gets the end level dialog message for when the player suffers critical damage.
      /// </summary>
      /// <value>
      /// The end level dialog message for when the player suffers critical damage.
      /// </value>
      protected virtual string CriticalDamageMessage
      {
         get
         {
            return "Your aircraft has been critically damaged. Try again?";
         }
      }

      /// <summary>
      /// Gets the enemy destruction monitor script.
      /// </summary>
      /// <value>
      /// The enemy destruction monitor script.
      /// </value>
      protected EnemyDestructionMonitorScript EnemyMonitor { get; set; }

      /// <summary>
      /// Gets a value indicating whether or not the level has been initialized.
      /// </summary>
      /// <value>
      /// A value indicating whether or not the level has been initialized.
      /// </value>
      protected bool? Initialized { get; set; }

      /// <summary>
      /// Gets the message displayed to the player when starting the level.
      /// </summary>
      /// <value>
      /// The message displayed to the player when starting the level.
      /// </value>
      protected virtual string StartMessage
      {
         get
         {
            return string.Format("Find and destroy all tanks on the island.\n{0} targets remaining.", this.NumberOfTargets);
         }
      }

      /// <summary>
      /// Gets the success message for the end level dialog when the player destroys all targets.
      /// </summary>
      /// <value>
      /// The success message for the end level dialog when the player destroys all targets.
      /// </value>
      protected virtual string SuccessMessage
      {
         get
         {
            return "The targets have been eliminated. Well done!";
         }
      }

      /// <summary>
      /// Attempts to initialize the level.
      /// </summary>
      protected virtual void Initialize()
      {
         // Only try to initialize if we have not already failed.
         if (!this.Initialized.HasValue)
         {
            try
            {
               var obj = ServiceProvider.Instance.ResourceLoader.LoadGameObject(this._levelGameObjectName);
               if (obj == null)
               {
                  Debug.LogErrorFormat("Unable to instantiate game object: {0}", this._levelGameObjectName);
                  this.Initialized = false;
                  return;
               }

               this.EnemyMonitor = obj.GetComponent<EnemyDestructionMonitorScript>();
               if (this.EnemyMonitor == null)
               {
                  Debug.LogErrorFormat("Unable to get the enemy destruction monitor script from the level game object '{0}'", this._levelGameObjectName);
                  this.Initialized = false;
                  return;
               }

               this.SpawnTanks(this.EnemyMonitor);

               if (!string.IsNullOrEmpty(this.StartMessage))
               {
                  ServiceProvider.Instance.GameWorld.ShowStatusMessage(this.StartMessage);
               }

               this.Initialized = true;
            }
            catch (Exception ex)
            {
               Debug.LogException(ex);
               this.Initialized = false;
            }
         }
      }

      /// <summary>
      /// Update is called every frame.
      /// </summary>
      protected override void Update()
      {
         base.Update();

         // Initialize the level if not yet initialized.
         // If initialization failed, skip the update.
         if (!(this.Initialized ?? false))
         {
            this.Initialize();
            return;
         }

         // Check for win/lose conditions and end the level if needed.
         if (this.EnemyMonitor.AllObjectivesDestroyed)
         {
            this.EndLevel(true, this.SuccessMessage, 0);
         }
         else if (ServiceProvider.Instance.PlayerAircraft.CriticallyDamaged)
         {
            this.EndLevel(false, this.CriticalDamageMessage, 0);
         }
      }

      /// <summary>
      /// Called when a target has been destroyed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
      private void ObjectiveDestroyed(object sender, EventArgs e)
      {
         this._targetsDestroyed++;
         var remaining = this.NumberOfTargets - this._targetsDestroyed;
         if (remaining > 0)
         {
            ServiceProvider.Instance.GameWorld.ShowStatusMessage(string.Format("Target destroyed! {0} targets remaining.", remaining));
         }
      }

      /// <summary>
      /// Spawns the objective tanks.
      /// </summary>
      /// <param name="enemyMonitorScript">The enemy monitor script.</param>
      private void SpawnTanks(EnemyDestructionMonitorScript enemyMonitorScript)
      {
         var spawnPoints = enemyMonitorScript.GetComponentsInChildren<SeekAndDestroySpawnPointScript>().ToList();

         var parent = new GameObject("ObjectiveTargets").transform;
         parent.SetParent(enemyMonitorScript.transform, false);

         for (int i = 0; i < this.NumberOfTargets; i++)
         {
            var index = UnityEngine.Random.Range(0, spawnPoints.Count);
            var spawnPoint = spawnPoints[index];

            var obj = new GameObject("Objective" + i);
            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
            obj.transform.SetParent(parent, true);

            var proxyScript = obj.AddComponent<AntiAircraftTankProxy>();
            enemyMonitorScript.AddObjective(proxyScript);

            var objectiveScript = obj.AddComponent<SeekAndDestroyTankObjectiveScript>();
            objectiveScript.Destroyed += this.ObjectiveDestroyed;

            spawnPoints.RemoveAt(index);
         }

         foreach (var spawnPoint in spawnPoints)
         {
            spawnPoint.gameObject.SetActive(false);
         }
      }
   }
}