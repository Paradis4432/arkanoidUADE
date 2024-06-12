using System;
using System.Collections.Generic;
using physics.objects;
using physics.objects.impls;
using UnityEngine;

namespace physics {
    public abstract class ObjectRepository {
        // objects used as a more general set
        private static readonly ObjectContainer<Movable> Objects = new();

        // type based sets:
        private static readonly ObjectContainer<Obstacle> Obstacles = new();
        private static readonly ObjectContainer<Wall> Walls = new();
        private static readonly ObjectContainer<Ball> Balls = new();
        private static readonly ObjectContainer<PowerUp> PowerUps = new();

        public static void RegisterObject(Movable gameObject) {
            //Debug.Log("Registering object " + gameObject);
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
                case PowerUp powerUp:
                    PowerUps.Add(powerUp);
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
                PowerUp powerUp => PowerUps.Contains(powerUp),
                _ => Objects.Contains(gameObject)
            };
        }

        public static void UnregisterObject(Movable gameObject) {
            if (!IsObjectRegistered(gameObject))
                throw new Exception("Object " + gameObject + " not registered");

            switch (gameObject)
            {
                case Obstacle obstacle:
                    Obstacles.Disable(obstacle);
                    break;
                case Wall wall:
                    Walls.Disable(wall);
                    break;
                case Ball ball:
                    Balls.Disable(ball);
                    break;
                case PowerUp powerUp:
                    PowerUps.Disable(powerUp);
                    break;
                default:
                    Objects.Disable(gameObject);
                    break;
            }
        }

        public static ObjectContainer<Obstacle> GetObstacles() {
            return Obstacles;
        }

        public static ObjectContainer<Wall> GetWalls() {
            return Walls;
        }

        public static ObjectContainer<Ball> GetBalls() {
            return Balls;
        }

        public static ObjectContainer<Movable> GetObjects() {
            return Objects;
        }

        public static ObjectContainer<PowerUp> GetPowerUps() {
            return PowerUps;
        }
    }
}