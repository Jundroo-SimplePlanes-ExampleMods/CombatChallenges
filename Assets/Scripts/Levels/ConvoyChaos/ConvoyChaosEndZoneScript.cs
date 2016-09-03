namespace Assets.Scripts.Levels.ConvoyChaos
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
   using UnityEngine;

   /// <summary>
   /// The script for the end zone of a convoy chaos level.
   /// </summary>
   /// <seealso cref="UnityEngine.MonoBehaviour" />
   public class ConvoyChaosEndZoneScript : MonoBehaviour
   {
      /// <summary>
      /// Gets a value indicating whether a vehicle has reached the end zone.
      /// </summary>
      /// <value>
      /// <c>true</c> if a vehicle has reached the end zone; otherwise, <c>false</c>.
      /// </value>
      public bool VehicleHasReachedEndZone { get; private set; }

      /// <summary>
      /// OnTriggerEnter is called when the Collider other enters the trigger.
      /// </summary>
      /// <param name="other">The collider that entered the trigger.</param>
      protected virtual void OnTriggerEnter(Collider other)
      {
         var vehicle = SimpleGroundVehicleScript.GetComponentInParent(other);
         if (vehicle != null)
         {
            this.VehicleHasReachedEndZone = true;
            
            vehicle._Speed = 0;
         }
      }
   }
}