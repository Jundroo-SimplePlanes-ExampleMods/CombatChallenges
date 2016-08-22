namespace SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using Assets.Scripts.Parts.Targeting;
   using UnityEngine;

   namespace Assets
   {
      namespace Scripts
      {
         namespace Levels
         {
            namespace Damage
            {
               public enum DamageType
               {
                  Unknown = 0,
                  Collision = 1,
                  Explosion = 2,
                  StandardBullets = 3,
               }

               public partial class DamageableBody : ProxyType<DamageableBody>
               {
                  private MethodInfo _onDamageReceived = GetMethod("OnDamageReceived", DamageTypeEnum.RealType, typeof(float), typeof(Vector3), typeof(Vector3?));

                  public DamageableBody(object realObject) : base(realObject)
                  {
                  }

                  public void OnDamageReceived(DamageType type, float damage, Vector3 position, Vector3? normal)
                  {
                     this._onDamageReceived.Invoke(this.RealObject, new object[] { (int)type, damage, position, normal });
                  }
               }

               public partial class DamageTypeEnum : ProxyType<DamageType>
               {
               }
            }

            namespace Enemies
            {
               public enum ConvoyVehicleType
               {
                  Truck,
                  AATank,
               }

               public partial class AntiAircraftTankScript : ProxyType<AntiAircraftTankScript>
               {
                  private FieldInfo __groundTarget = GetField("_groundTarget");

                  private PropertyInfo _isHostile = GetProperty("IsHostile");

                  public AntiAircraftTankScript(object realObject) : base(realObject)
                  {
                  }

                  public GroundTarget _groundTarget
                  {
                     get
                     {
                        return new GroundTarget(this.__groundTarget.GetValue(this.RealObject));
                     }
                  }

                  public bool IsHostile
                  {
                     get
                     {
                        return (bool)this._isHostile.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._isHostile.SetValue(this.RealObject, value, null);
                     }
                  }
               }

               public partial class BasicConvoyProviderScript : ProxyType<BasicConvoyProviderScript>
               {
                  private FieldInfo __convoyPrefabs = GetField("_convoyPrefabs");

                  public BasicConvoyProviderScript(object realObject) : base(realObject)
                  {
                  }

                  public static BasicConvoyProviderScript AddComponent(GameObject gameObject)
                  {
                     return new BasicConvoyProviderScript(gameObject.AddComponent(RealType));
                  }

                  public void ConfigureConvoy(params ConvoyVehicleType[] vehicleTypes)
                  {
                     if (vehicleTypes == null)
                     {
                        return;
                     }

                     var prefabs = new GameObject[vehicleTypes.Length];
                     for (int i = 0; i < vehicleTypes.Length; i++)
                     {
                        switch (vehicleTypes[i])
                        {
                           case ConvoyVehicleType.AATank:

                              prefabs[i] = Resources.Load<GameObject>("Prefabs/Convoy/Vehicles/APCConvoy");
                              break;

                           case ConvoyVehicleType.Truck:

                              prefabs[i] = Resources.Load<GameObject>("Prefabs/Convoy/Vehicles/ConvoyTruck");
                              break;

                           default:

                              Debug.LogErrorFormat("Convoy vehicle type '{0}' is not currently supported.", vehicleTypes[i]);
                              break;
                        }
                     }

                     this.__convoyPrefabs.SetValue(this.RealObject, prefabs);
                  }
               }

               public partial class ConvoyScript : ProxyType<ConvoyScript>
               {
                  private FieldInfo __convoyProvider = GetField("_convoyProvider");

                  private FieldInfo __startingWaypoints = GetField("_startingWaypoints");

                  private MethodInfo _initialize = GetMethod("Initialize");

                  private PropertyInfo _isHostile = GetProperty("IsHostile");

                  private PropertyInfo _waypoints = GetProperty("Waypoints");

                  public ConvoyScript(object realObject) : base(realObject)
                  {
                  }

                  public bool IsHostile
                  {
                     get
                     {
                        return (bool)this._isHostile.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._isHostile.SetValue(this.RealObject, value, null);
                     }
                  }

                  public List<Transform> Waypoints
                  {
                     get
                     {
                        return (List<Transform>)this._waypoints.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._waypoints.SetValue(this.RealObject, value, null);
                     }
                  }

                  public static ConvoyScript AddComponent(GameObject gameObject)
                  {
                     return new ConvoyScript(gameObject.AddComponent(RealType));
                  }

                  public void Initialize(IEnumerable<Transform> waypoints, bool initiallyHostile)
                  {
                     this._initialize.Invoke(this.RealObject, new object[0]);

                     this.__startingWaypoints.SetValue(this.RealObject, new Transform[0]);
                     this.Waypoints = new List<Transform>(waypoints);
                     this.IsHostile = initiallyHostile;
                  }

                  public void SetConvoyProvider(BasicConvoyProviderScript convoyProvider)
                  {
                     this.__convoyProvider.SetValue(this.RealObject, convoyProvider.RealObject);
                  }
               }

               public partial class SimpleGroundVehicleScript : ProxyType<SimpleGroundVehicleScript>
               {
                  private FieldInfo __speed = GetField("_speed");

                  private FieldInfo __targetVelocity = GetField("_targetVelocity");

                  private PropertyInfo _isDestroyed = GetProperty("IsDestroyed");

                  private PropertyInfo _isHostile = GetProperty("IsHostile");

                  public SimpleGroundVehicleScript(object realObject) : base(realObject)
                  {
                  }

                  public float _Speed
                  {
                     get
                     {
                        return (float)this.__speed.GetValue(this.RealObject);
                     }

                     set
                     {
                        this.__speed.SetValue(this.RealObject, value);
                     }
                  }

                  public float _TargetVelocity
                  {
                     get
                     {
                        return (float)this.__targetVelocity.GetValue(this.RealObject);
                     }

                     set
                     {
                        this.__targetVelocity.SetValue(this.RealObject, value);
                     }
                  }

                  public bool IsDestroyed
                  {
                     get
                     {
                        return (bool)this._isDestroyed.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._isDestroyed.SetValue(this.RealObject, value, null);
                     }
                  }

                  public bool IsHostile
                  {
                     get
                     {
                        return (bool)this._isHostile.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._isHostile.SetValue(this.RealObject, value, null);
                     }
                  }
               }
            }
         }

         namespace Parts
         {
            namespace Targeting
            {
               public partial class GroundTarget : ProxyType<GroundTarget>
               {
                  private PropertyInfo _maxVisibilityRange = GetProperty("MaxVisibleRange");

                  public GroundTarget(object realObject) : base(realObject)
                  {
                  }

                  public float MaxVisibleRange
                  {
                     get
                     {
                        return (float)this._maxVisibilityRange.GetValue(this.RealObject, null);
                     }

                     set
                     {
                        this._maxVisibilityRange.SetValue(this.RealObject, value, null);
                     }
                  }
               }
            }
         }
      }
   }

   /// <summary>
   /// A helper class used for reflecting on various SimplePlanes related code.
   /// </summary>
   public static class ReflectionHelper
   {
      /// <summary>
      /// Initializes static members of the <see cref="ReflectionHelper"/> class.
      /// </summary>
      static ReflectionHelper()
      {
         AssemblyCSharp = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => x.GetName().Name.ToLower() == "assembly-csharp")
            .FirstOrDefault();

         if (AssemblyCSharp == null)
         {
            Debug.LogError("Could not find the default assembly for the game");
         }
      }

      /// <summary>
      /// Gets the default assembly for the game.
      /// </summary>
      /// <value>
      /// The default assembly for the game.
      /// </value>
      public static Assembly AssemblyCSharp { get; private set; }
   }

   /// <summary>
   /// A proxy class that wraps an underlying SimplePlanes type and provides reflection based methods for interacting with that type.
   /// </summary>
   /// <typeparam name="T">The concrete type of the proxy class.</typeparam>
   public abstract class ProxyType<T>
   {
      /// <summary>
      /// A collection of common binding flags usable for most reflection methods.
      /// </summary>
      private static readonly BindingFlags AllBindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

      /// <summary>
      /// The real type represented by this proxy type.
      /// </summary>
      private static Type _realType;

      /// <summary>
      /// Initializes a new instance of the <see cref="ProxyType{T}"/> class.
      /// </summary>
      /// <param name="realObject">The real object being wrapped by this proxy.</param>
      protected ProxyType(object realObject)
      {
         this.RealObject = realObject;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ProxyType{T}"/> class.
      /// </summary>
      protected ProxyType()
      {
         this.RealObject = Activator.CreateInstance(RealType);
      }

      /// <summary>
      /// Gets the real type represented by this proxy type.
      /// </summary>
      /// <value>
      /// The real type represented by this proxy type.
      /// </value>
      public static Type RealType
      {
         get
         {
            if (_realType == null)
            {
               var typeFullName = typeof(T).FullName.Substring(23);
               _realType = ReflectionHelper.AssemblyCSharp.GetType(typeFullName, true);
            }

            return _realType;
         }
      }

      /// <summary>
      /// Gets or sets the real object being wrapped by this proxy.
      /// </summary>
      /// <value>
      /// The real object being wrapped by this proxy.
      /// </value>
      public object RealObject { get; protected set; }

      /// <summary>
      /// Gets the specified field.
      /// </summary>
      /// <param name="name">The name of the field to retrieve.</param>
      /// <returns>The field info object.</returns>
      public static FieldInfo GetField(string name)
      {
         var realType = RealType;

         var field = realType.GetField(name, AllBindings);
         if (field == null)
         {
            Debug.LogErrorFormat(
               "Could not find field via reflection: {0}.{1}",
               realType.FullName,
               name);
         }

         return field;
      }

      /// <summary>
      /// Gets the specified method info.
      /// </summary>
      /// <param name="name">The name of the method to retrieve.</param>
      /// <param name="parameters">The parameters types for the method.</param>
      /// <returns>The requested method info object.</returns>
      public static MethodInfo GetMethod(string name, params Type[] parameters)
      {
         var realType = RealType;

         var method = realType.GetMethod(name, AllBindings, null, parameters, null);
         if (method == null)
         {
            Debug.LogErrorFormat(
               "Could not find method via reflection: {0}.{1}({2})",
               realType.FullName,
               name,
               string.Join(", ", parameters.Select(x => x.FullName).ToArray()));
         }

         return method;
      }

      /// <summary>
      /// Gets the specified property.
      /// </summary>
      /// <param name="name">The name of the property to retrieve.</param>
      /// <returns>The property info object.</returns>
      public static PropertyInfo GetProperty(string name)
      {
         var realType = RealType;

         var field = realType.GetProperty(name, AllBindings);
         if (field == null)
         {
            Debug.LogErrorFormat(
               "Could not find property via reflection: {0}.{1}",
               realType.FullName,
               name);
         }

         return field;
      }
   }
}