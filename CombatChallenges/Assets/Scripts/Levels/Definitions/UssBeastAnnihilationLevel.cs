namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using Assault;
   using Jundroo.SimplePlanes.ModTools;

   /// <summary>
   /// The 'USS Beast Assault 2' level.
   /// </summary>
   public class UssBeastAnnihilationLevel : AssaultLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = 
         "An enemy aircraft carrier, the USS Beast, is passing by Krakabloa. " + 
         "Now is the chance to take it out. Go sink the carrier and its 3 destroyer escorts. " + 
         "Be careful, the 3 destroyers will attack on sight.";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "UssBeastAnnihilationLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "USS Beast Assault 2";

      /// <summary>
      /// Initializes a new instance of the <see cref="UssBeastAnnihilationLevel"/> class.
      /// </summary>
      public UssBeastAnnihilationLevel()
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
            return StartLocations.YeagerAirport;
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
            return "Destroy the USS Beast aircraft carrier and its 3 destroyer escorts.";
         }
      }

      /// <summary>
      /// Gets the success message for the end level dialog when the player destroys all targets.
      /// </summary>
      /// <value>
      /// The success message for the end level dialog when the player destroys all targets.
      /// </value>
      protected override string SuccessMessage
      {
         get
         {
            return "All the ships have been destroyed or are currently sinking. Well done!";
         }
      }
   }
}