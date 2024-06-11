using System.Collections.Generic;
using physics.objects.impls;
using UnityEngine;

namespace physics {
    public abstract class ObjectRepository {
        // objects used as a more general set
        private static readonly HashSet<Movable> Objects = new();

        // type based sets:
        private static readonly HashSet<Obstacle> Obstacles = new();
        private static readonly HashSet<Wall> Walls = new();

        public static void RegisterObject(Movable gameObject) {
            if (IsObjectRegistered(gameObject))
                Debug.LogError("Object already registered");

            switch (gameObject)
            {
                case Obstacle obstacle:
                    Obstacles.Add(obstacle);
                    break;
                case Wall wall:
                    Walls.Add(wall);
                    break;
                default:
                    Objects.Add(gameObject);
                    break;
            }
        }

        private static bool IsObjectRegistered(Movable gameObject) {
            return gameObject switch {
                Obstacle obstacle => Obstacles.Contains(obstacle),
                Wall wall => Walls.Contains(wall),
                _ => Objects.Contains(gameObject)
            };
        }

        public static void UnregisterObject(Movable gameObject) {
            if (!IsObjectRegistered(gameObject))
                Debug.LogError("Object not registered");

            switch (gameObject)
            {
                case Obstacle obstacle:
                    Obstacles.Remove(obstacle);
                    break;
                case Wall wall:
                    Walls.Remove(wall);
                    break;
                default:
                    Objects.Remove(gameObject);
                    break;
            }
        }

        public static HashSet<Obstacle> GetObstacles() {
            return Obstacles;
        }

        public static HashSet<Wall> GetWalls() {
            return Walls;
        }

        public static HashSet<Movable> GetObjects() {
            return Objects;
        }
    }
}