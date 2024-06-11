using physics.objects.impls;
using UnityEngine;

namespace physics.objects {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : Movable {
        private static float _distanceHitTresHold; // static to share across balls

        public int initialForceXMin = -2000;
        public int initialForceXMax = 2000;
        public float initialForceY = 300f;
        public float Radius => transform.localScale.x / 2;

        protected new void Start() {
            base.Start();
            if (_distanceHitTresHold != 0f) return;

            _distanceHitTresHold = transform.localScale.x / 3.125f; // -> div por valores > 1.02
            // T puede ser cualquier valor > 1.02 | 0.16 = 0.5 / T => T = 3.125f
        }

        private void Update() {
            CalculateFisics();

            // walls first, less objects and more likely to hit first
            if (CollidingWithWalls()) return;
            // player and loose area because its second most likely and just a few things
            if (CollidingWithObjects()) return;
            // check all bricks
            if (CollidingWithObstacles()) return;
            if (CollidingWithOtherBalls()) return;
        }

        private bool CollidingWithWalls() {
            foreach (Wall wall in ObjectRepository.GetWalls().GetEnabledValues())
            {
                CollisionValues col = CollisionDetector.Between(this, wall);
                if (!col.hit) continue;

                TurnBasedOn(col.distX, col.distY);

                return true;
            }

            return false;
        }

        private bool CollidingWithObstacles() {
            foreach (Obstacle obstacle in ObjectRepository.GetObstacles().GetEnabledValues())
            {
                CollisionValues col = CollisionDetector.Between(this, obstacle);
                if (!col.hit) continue;

                TurnBasedOn(col.distX, col.distY);

                //Destroy(obstacle.gameObject);
                obstacle.Disable();

                return true;
            }

            return false;
        }

        private bool CollidingWithObjects() {
            foreach (Movable movable in ObjectRepository.GetObjects().GetEnabledValues())
            {
                switch (movable)
                {
                    case LooseArea looseArea: {
                        CollisionValues col = CollisionDetector.Between(this, looseArea);
                        if (!col.hit) continue;

                        looseArea.HandleBallFalling(this);
                        return true;
                    }
                    case Player player: {
                        CollisionValues col = CollisionDetector.Between(this, player);
                        if (!col.hit) continue;
                        //Debug.Log(col);

                        TurnBasedOn(col.distX, col.distY);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CollidingWithOtherBalls() {
            foreach (Ball ball in ObjectRepository.GetBalls().GetEnabledValues())
            {
                CollisionValues col = CollisionDetector.Between(this, ball);

                if (!col.hit) continue;

                // TODO ball to ball hit, make each ball go in the opposite direction
                return true;
            }

            return false;
        }

        private void TurnBasedOn(float dx, float dy) {
            // 0.16 for 0.5 scale
            if (Mathf.Abs(dx) > _distanceHitTresHold) Vel.X = -Vel.X;
            if (Mathf.Abs(dy) > _distanceHitTresHold) Vel.Y = -Vel.Y;
        }
    }
}