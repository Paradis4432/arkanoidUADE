using System.Collections.Generic;
using UnityEngine;

namespace physics {
    public class ObjectRepository {
        private static readonly HashSet<Movable> Objects = new();

        public static void RegisterObject(Movable gameObject) {
            Debug.Log("Registering object " + gameObject);
            if (IsObjectRegistered(gameObject))
                Debug.LogError("Object already registered");

            Objects.Add(gameObject);
        }

        public static bool IsObjectRegistered(Movable gameObject) {
            return Objects.Contains(gameObject);
        }

        public static void UnregisterObject(Movable gameObject) {
            if (!IsObjectRegistered(gameObject))
                Debug.LogError("Object not registered");

            Objects.Remove(gameObject);
        }
        
        public static HashSet<Movable> GetObjects() {
            return Objects;
        }
    }
}