namespace Assets.Scripts.Levels.SeekAndDestroy
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// A script defining a spawn point for a seek and destroy target.
   /// </summary>
   /// <seealso cref="UnityEngine.MonoBehaviour" />
   public class SeekAndDestroySpawnPointScript : MonoBehaviour
   {
      /// <summary>
      /// The configuration script for the level.
      /// </summary>
      private SeekAndDestroyLevelConfigScript _config;

      /// <summary>
      /// Awake is called when the script instance is being loaded.
      /// </summary>
      protected virtual void Awake()
      {
         this._config = this.GetComponentInParent<SeekAndDestroyLevelConfigScript>();
         if (this._config == null)
         {
            Debug.LogErrorFormat("Could not find the level configuration script.", this.name);
            this.gameObject.SetActive(false);
            return;
         }
      }

      /// <summary>
      /// Used to draw gizmos that are pickable and always drawn.
      /// </summary>
      protected virtual void OnDrawGizmos()
      {
         if (this._config == null)
         {
            this._config = this.GetComponentInParent<SeekAndDestroyLevelConfigScript>();
            if (this._config == null)
            {
               return;
            }
         }

         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(this.transform.position, this._config.GoHostileRange);

         Gizmos.color = Color.blue;
         Gizmos.DrawWireSphere(this.transform.position, this._config.DiscoverableRange);

         Gizmos.color = Color.black;

         var originalMatrix = Gizmos.matrix;
         var localMatrix = this.transform.localToWorldMatrix;

         var mainTransform = Matrix4x4.TRS(new Vector3(0, .75f, 0.5f), Quaternion.Euler(0, 0, 0), new Vector3(5.5f, 6.25f, 11.75f));

         Gizmos.matrix = localMatrix * mainTransform;
         Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

         Gizmos.matrix = originalMatrix;
      }
   }
}