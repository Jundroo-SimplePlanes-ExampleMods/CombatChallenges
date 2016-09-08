namespace Assets.Scripts.Levels.Definitions
{
   using System;
   using Jundroo.SimplePlanes.ModTools;
   using SeekAndDestroy;

   /// <summary>
   /// The 'Seek and Destroy: Krakabloa 3' level.
   /// </summary>
   public class KrakabloaSeekAndDestroy3Level : SeekAndDestroyLevelBase
   {
      /// <summary>
      /// The total number of tanks to be destroyed in order to complete the level.
      /// </summary>
      private const int ObjectiveCount = 12;

      /// <summary>
      /// The level description.
      /// </summary>
      private static readonly string LevelDescription =
         string.Format("Find and destroy all {0} tanks on the island. ", ObjectiveCount) +
         "Your targeting system will not pick them up until you get close. " +
         "Be careful, they may spot you first and they are not very friendly.";

      /// <summary>
      /// The level game object name.
      /// </summary>
      private static readonly string LevelGameObject = "KrakabloaSeekAndDestroyLevel";

      /// <summary>
      /// The level name.
      /// </summary>
      private static readonly string Name = "Seek&Destroy: Krakabloa 3";

      /// <summary>
      /// Initializes a new instance of the <see cref="KrakabloaSeekAndDestroy3Level"/> class.
      /// </summary>
      public KrakabloaSeekAndDestroy3Level()
         : base(Name, LevelDescription, LevelGameObject)
      {
      }

      /// <summary>
      /// Gets the number of targets to be destroyed in order to complete the level.
      /// </summary>
      /// <value>
      /// The number of targets to be destroyed in order to complete the level.
      /// </value>
      public override int NumberOfTargets
      {
         get
         {
            return ObjectiveCount;
         }
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
   }
}