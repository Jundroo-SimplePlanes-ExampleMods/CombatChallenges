namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Assault;
   using Jundroo.SimplePlanes.ModTools;
   using SimplePlanesReflection.Assets.Scripts.Levels.Damage;
   using UnityEngine;

   /// <summary>
   /// The 'Snowstone Base Assault 2' level.
   /// </summary>
   public class SnowstoneBaseAnnihilationLevel : AssaultLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = "The Snowstone mountain base has received some upgrades. Go destroy all enemy targets.";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "SnowstoneBaseAnnihilationLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "Snowstone Base Assault 2";

      /// <summary>
      /// Initializes a new instance of the <see cref="SnowstoneBaseAnnihilationLevel"/> class.
      /// </summary>
      public SnowstoneBaseAnnihilationLevel()
         : base(Name, LevelDescription, LevelGameObject)
      {
      }

      /// <summary>
      /// Gets the start location for the level.
      /// </summary>
      /// <value>
      /// The start location for the level.
      /// </value>
      protected override LevelStartLocation StartLocation
      {
         get
         {
            return StartLocations.AvalancheAirport;
         }
      }

      /// <summary>
      /// Gets the message displayed to the player when starting the level.
      /// </summary>
      /// <value>
      /// The message displayed to the player when starting the level.
      /// </value>
      protected override string StartMessage
      {
         get
         {
            return "Destroy all targets at the Snowstone mountain base.";
         }
      }

      /// <summary>
      /// Attempts to initialize the level.
      /// </summary>
      protected override void Initialize()
      {
         base.Initialize();

         if (this.Initialized ?? false)
         {
            this.EnemyMonitor.StartCoroutine(this.DisableOriginalTurretsCoroutine());
         }
      }

      /// <summary>
      /// A coroutine that waits for the original turrets to be spawned and then disables them.
      /// </summary>
      /// <returns>The enumerator for the coroutine.</returns>
      private IEnumerator DisableOriginalTurretsCoroutine()
      {
         // Let a few frames pass so the plane can be repositioned and the turrets spawned.
         yield return null;
         yield return null;
         yield return null;
         yield return null;
         yield return null;

         this.KillTurret("MissileLauncherLeft");
         this.KillTurret("MissileLauncherRight");
         this.KillTurret("LaserTurretLeft");
         this.KillTurret("LaserTurretRight");
      }

      /// <summary>
      /// Kills the turret with the specified name.
      /// </summary>
      /// <param name="gameObjectName">The name of the game object to kill.</param>
      private void KillTurret(string gameObjectName)
      {
         var obj = GameObject.Find(gameObjectName);
         if (obj == null)
         {
            Debug.LogErrorFormat("Turret '{0}' could not be found so it could not be disabled.", gameObjectName);
            return;
         }

         // We need to grab the damageable body and kill it so the player's
         // targeting system will no longer allow it to be selected.
         var damageableBody = new DamageableBody(obj.GetComponent(DamageableBody.RealType));
         if (damageableBody.RealObject == null)
         {
            Debug.LogErrorFormat("Turret '{0}' could not be disabled because the damageable body component could not be found.", gameObjectName);
         }

         damageableBody.OnDamageReceived(DamageType.Explosion, 999999, Vector3.zero, null);

         // After we have dealt with targeting system, we can just disable the whole object.
         obj.SetActive(false);
      }
   }
}