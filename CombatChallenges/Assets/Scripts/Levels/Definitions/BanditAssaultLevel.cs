namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using Assault;
   using Jundroo.SimplePlanes.ModTools;

   /// <summary>
   /// The 'Bandit Assault 1' level.
   /// </summary>
   public class BanditAssaultLevel : AssaultLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = "Destroy all 4 tanks at Bandit Airport. Be careful, they have support and base defenses!";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "BanditAssaultLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "Bandit Assault 1";

      /// <summary>
      /// Initializes a new instance of the <see cref="BanditAssaultLevel"/> class.
      /// </summary>
      public BanditAssaultLevel()
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
            return "Destroy all 4 tanks at Bandit Airport.";
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
            return "The targets at Bandit Airport have been eliminated. Well done!";
         }
      }
   }
}