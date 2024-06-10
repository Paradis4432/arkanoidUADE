using physics.objects.impls;
using UnityEngine;

namespace physics.objects {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : Movable {
        public int initialForceXMin = -2000;
        public int initialForceXMax = 2000;
        public float initialForceY = 300f;
        public float Radius => transform.localScale.x / 2;

        private void Update() {
            CalculateFisics();

            // walls first, less objects and more likely to hit first
            if (CollidingWithWalls()) return;
            // player and loose area because its second most likely and just a few things
            if (CollidingWithOthers()) return;
            // check all bricks
            if (CollidingWithObstacles()) return;
        }

        private bool CollidingWithWalls() {
            foreach (Wall wall in ObjectRepository.GetWalls())
            {
                CollisionValues col = CollisionDetector.Between(this, wall);

                if (!col.hit) continue;

                TurnBasedOn(col.distX, col.distY);

                return true;
            }

            return false;
        }

        private bool CollidingWithObstacles() {
            foreach (Obstacle obstacle in ObjectRepository.GetObstacles())
            {
                CollisionValues col = CollisionDetector.Between(this, obstacle);

                if (!col.hit) continue;

                TurnBasedOn(col.distX, col.distY);

                Destroy(obstacle.gameObject);

                return true;
            }

            return false;
        }

        private bool CollidingWithOthers() {
            foreach (Movable movable in ObjectRepository.GetObjects())
            {
                switch (movable)
                {
                    case LooseArea looseArea: {
                        CollisionValues col = CollisionDetector.Between(this, looseArea);

                        if (!col.hit) continue;

                        looseArea.HandleBallFalling();
                        return true;
                    }
                    case Player player: {
                        CollisionValues col = CollisionDetector.Between(this, player);

                        if (!col.hit) continue;

                        TurnBasedOn(col.distX, col.distY);
                        return true;
                    }
                }
            }

            return false;
        }

        private void TurnBasedOn(float dx, float dy) {
            if (Mathf.Abs(dx) > 0.16f) Vel.X = -Vel.X;
            if (Mathf.Abs(dy) > 0.16f) Vel.Y = -Vel.Y;
        }
    }
}