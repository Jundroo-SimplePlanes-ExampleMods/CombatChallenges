namespace Assets.Scripts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Jundroo.SimplePlanes.ModTools;
   using UnityEngine;

   /// <summary>
   /// A collection of common starting locations.
   /// </summary>
   public static class StartLocations
   {
      /// <summary>
      /// Gets the start location for Avalanche airport.
      /// </summary>
      /// <value>
      /// The start location for Avalanche airport.
      /// </value>
      public static LevelStartLocation AvalancheAirport
      {
         get
         {
            return new LevelStartLocation()
            {
               Position = new Vector3(5581.7f, 63.47459f, 141011f),
               Rotation = new Vector3(0f, 90f, 0f),
               InitialSpeed = 0f,
               InitialThrottle = 0f,
               StartOnGround = true,
            };
         }
      }

      /// <summary>
      /// Gets the start location for Yeager airport.
      /// </summary>
      /// <value>
      /// The start location for Yeager airport.
      /// </value>
      public static LevelStartLocation YeagerAirport
      {
         get
         {
            return new LevelStartLocation()
            {
               Position = new Vector3(26392.92f, 369.7402f, 53185.99f),
               Rotation = new Vector3(0f, 189.1688f, 0f),
               InitialSpeed = 0f,
               InitialThrottle = 0f,
               StartOnGround = true,
            };
         }
      }
   }
}