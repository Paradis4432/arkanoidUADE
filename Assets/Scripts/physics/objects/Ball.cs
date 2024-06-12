using DefaultNamespace;
using physics.objects.impls;
using UnityEngine;

namespace physics.objects {
    //[RequireComponent(typeof(SpriteRenderer))]
    public class Ball : Movable {
        private static float _distanceHitTresHold; // static to share across balls

        public int initialForceXMin = -1;
        public int initialForceXMax = 1;
        public float initialForceY = 2f;

        public float Radius => transform.localScale.x / 2;
        public PowerUpFactory powerUpFactory;

        protected new void Start() {
            base.Start();
            if (_distanceHitTresHold != 0f) return;

            //_distanceHitTresHold = transform.localScale.x / 3.125f; // -> div por valores > 1.02
            _distanceHitTresHold = Radius / 2;
            //Debug.Log("T:" + _distanceHitTresHold);
            // T puede ser cualquier valor > 1.02 | 0.16 = 0.5 / T => T = 3.125f
        }

        public void UpdateMe() {
            CalculateFisics();

            // walls first, less objects and more likely to hit first
            if (CollidingWithWalls()) return;
            // player and loose area because its second most likely and just a few things
            if (CollidingWithObjects()) return;
            // check all bricks
            if (CollidingWithObstacles()) return;
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
            int r = Random.Range(0, 100);
            Vector2 position = transform.position;
            float rad = Radius;
            foreach (Obstacle obstacle in ObjectRepository.GetObstacles().GetEnabledValues())
            {
                CollisionValues col = CollisionDetector.Between(position, rad, obstacle); // 72% of total
                // 22% getX 
                // 22% getY
                // 5% rect.getWidth and ball.getRad
                // CollidingWithObstacles -> 28 ish ms

                if (!col.hit) continue;


                TurnBasedOn(col.distX, col.distY);

                //Debug.Log(col);
                //powerUpManager.AttemptSpawnPowerUp(this);

                if (Manager.PowerUpsLeft > 0 && r < 1000)
                {
                    PowerUp powerUp = powerUpFactory.GetOrCreate();
                    powerUp.SetPosition(transform.position);
                    powerUp.AddForce(new Vector2(0, -1));
                    Manager.PowerUpsLeft--;
                }

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
                        AddForce(player.GetVelocity().X, player.GetVelocity().Y);
                        return true;
                    }
                }
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