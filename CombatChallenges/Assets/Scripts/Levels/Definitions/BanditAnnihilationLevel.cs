namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using Assault;
   using Jundroo.SimplePlanes.ModTools;

   /// <summary>
   /// The 'Bandit Assault 2' level.
   /// </summary>
   public class BanditAnnihilationLevel : AssaultLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = "Bandit Assault got a whole lot harder. Destroy all 10 targets, including the destroyer!";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "BanditAnnihilationLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "Bandit Assault 2";

      /// <summary>
      /// Initializes a new instance of the <see cref="BanditAnnihilationLevel"/> class.
      /// </summary>
      public BanditAnnihilationLevel()
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
            return "Destroy all 10 targets, including the destroyer!";
         }
      }
   }
}