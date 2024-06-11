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

        private new void Start() {
            if (_distanceHitTresHold != 0f) return;

            //_distanceHitTresHold = transform.localScale.x / 3.125f; -> div por valores > 1.02
            //_distanceHitTresHold = 0.32f; 
            _distanceHitTresHold = 0.01f;
            //_distanceHitTresHold = 1.02f;
        }

        private void Update() {
            CalculateFisics();

            // walls first, less objects and more likely to hit first
            if (CollidingWithWalls()) return;
            // player and loose area because its second most likely and just a few things
            if (CollidingWithObjects()) return;
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

        private bool CollidingWithObjects() {
            foreach (Movable movable in ObjectRepository.GetObjects())
            {
                switch (movable)
                {
                    case LooseArea looseArea: {
                        CollisionValues col = CollisionDetector.Between(this, looseArea);
                        if (!col.hit) continue;

                        LooseArea.HandleBallFalling();
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

        private void TurnBasedOn(float dx, float dy) {
            // 0.16 for 0.5 scale
            if (Mathf.Abs(dx) > _distanceHitTresHold) Vel.X = -Vel.X;

            Debug.Log(dy);
            // 0,24 > 0.01
            if (Mathf.Abs(dy) > _distanceHitTresHold) Vel.Y = -Vel.Y;
        }
    }
}