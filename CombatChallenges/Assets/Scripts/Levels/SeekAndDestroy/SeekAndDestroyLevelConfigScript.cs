namespace Assets.Scripts.Levels.SeekAndDestroy
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// The configuration script for seek and destroy levels.
   /// </summary>
   /// <seealso cref="UnityEngine.MonoBehaviour" />
   public class SeekAndDestroyLevelConfigScript : MonoBehaviour
   {
      /// <summary>
      /// The minimum distance between the player and an objective before the objective becomes targetable by the player.
      /// </summary>
      [SerializeField]
      private float _discoverableRange = 1000f;

      /// <summary>
      /// The minimum distance between the player and an objective before the target becomes hostile to the player.
      /// </summary>
      [SerializeField]
      private float _goHostileRange = 2500f;

      /// <summary>
      /// Gets the minimum distance between the player and an objective before the objective becomes targetable by the player.
      /// </summary>
      /// <value>
      /// The minimum distance between the player and an objective before the objective becomes targetable by the player.
      /// </value>
      public float DiscoverableRange
      {
         get
         {
            return this._discoverableRange;
         }
      }

      /// <summary>
      /// Gets the minimum distance between the player and an objective before the target becomes hostile to the player.
      /// </summary>
      /// <value>
      /// The minimum distance between the player and an objective before the target becomes hostile to the player.
      /// </value>
      public float GoHostileRange
      {
         get
         {
            return this._goHostileRange;
         }
      }
   }
}