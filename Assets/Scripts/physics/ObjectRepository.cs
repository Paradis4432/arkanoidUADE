using System;
using System.Collections.Generic;
using physics.objects;
using physics.objects.impls;
using UnityEngine;

namespace physics {
    public abstract class ObjectRepository {
        // objects used as a more general set
        private static readonly HashSet<Movable> Objects = new();

        // type based sets:
        private static readonly HashSet<Obstacle> Obstacles = new();
        private static readonly HashSet<Wall> Walls = new();
        private static readonly HashSet<Ball> Balls = new();

        public static void RegisterObject(Movable gameObject) {
            Debug.Log("Registering object " + gameObject);
            if (IsObjectRegistered(gameObject))
                throw new Exception("Object already registered");

            switch (gameObject)
            {
                case Obstacle obstacle:
                    Obstacles.Add(obstacle);
                    break;
                case Wall wall:
                    Walls.Add(wall);
                    break;
                case Ball ball:
                    Balls.Add(ball);
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
                Ball ball => Balls.Contains(ball),
                _ => Objects.Contains(gameObject)
            };
        }

        public static void UnregisterObject(Movable gameObject) {
            if (!IsObjectRegistered(gameObject))
                Debug.LogError("Object " + gameObject + " not registered");

            switch (gameObject)
            {
                case Obstacle obstacle:
                    Obstacles.Remove(obstacle);
                    break;
                case Wall wall:
                    Walls.Remove(wall);
                    break;
                case Ball ball:
                    Balls.Remove(ball);
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

        public static HashSet<Ball> GetBalls() {
            return Balls;
        }

        public static HashSet<Movable> GetObjects() {
            return Objects;
        }
    }
}