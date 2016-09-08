namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using ConvoyChaos;
   using Jundroo.SimplePlanes.ModTools;
   using UnityEngine;

   /// <summary>
   /// The 'Convoy Chaos: Maywar 2' level.
   /// </summary>
   public class ConvoyChaosMaywar2Level : ConvoyChaosLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = "The chaos continues... More convoys to destroy and this time they may fight back!";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "MaywarConvoyChaos2Level";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "Convoy Chaos: Maywar 2";

      /// <summary>
      /// Initializes a new instance of the <see cref="ConvoyChaosMaywar2Level"/> class.
      /// </summary>
      public ConvoyChaosMaywar2Level()
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
            return new LevelStartLocation()
            {
               InitialSpeed = 50f,
               InitialThrottle = 1f,
               Position = new Vector3(68212f, 1550f, -35833f),
               Rotation = new Vector3(0f, 90f, 0f),
               StartOnGround = false,
            };
         }
      }
   }
}