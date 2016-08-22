namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using Assault;
   using Jundroo.SimplePlanes.ModTools;

   /// <summary>
   /// The 'USS Beast Assault 1' level.
   /// </summary>
   public class UssBeastAssaultLevel : AssaultLevelBase
   {
      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription = 
         "An enemy aircraft carrier, the USS Beast, is passing by Krakabloa. " + 
         "Now is the chance to take it out. Go sink the carrier but beware its escorts. " + 
         "The 3 destroyers guarding it will attack on sight.";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "UssBeastAssaultLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "USS Beast Assault 1";

      /// <summary>
      /// Initializes a new instance of the <see cref="UssBeastAssaultLevel"/> class.
      /// </summary>
      public UssBeastAssaultLevel()
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
            return "Destroy the USS Beast aircraft carrier.";
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
            return "The USS Beast is sinking. Well done!";
         }
      }
   }
}