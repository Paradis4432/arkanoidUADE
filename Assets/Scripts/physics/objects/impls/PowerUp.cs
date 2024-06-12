using UnityEngine;

namespace physics.objects.impls {
    public class PowerUp : Ball {
        private BallFactory _ballFactory;

        private new void Start() {
            AddForce(new Vector2(0, -1));

            ObjectRepository.RegisterObject(this);
        }

        private void Update() {
            CalculateFisics();

            // player and loose area because its second most likely and just a few things
            if (CollidingWithObjects()) return;
        }

        public new void OnDestroy() {
            // TODO some error idk
        }


        private bool CollidingWithObjects() {
            foreach (Movable movable in ObjectRepository.GetObjects().GetEnabledValues())
            {
                switch (movable)
                {
                    case LooseArea looseArea: {
                        CollisionValues col = CollisionDetector.Between(this, looseArea);
                        if (!col.hit) continue;

                        Destroy(gameObject);
                        return true;
                    }
                    case Player player: {
                        CollisionValues col = CollisionDetector.Between(this, player);
                        if (!col.hit) continue;
                        //Debug.Log(col);
                        // spawn extra balls:
                        /*Ball b = ballFactory.GetOrCreate(); // spawn a new powerup that falls to player
            b.SetPosition(ball.transform.position);
            b.AddForce(new Vector2(ball.initialForceXMin, ball.initialForceY));*/

                        Ball b = _ballFactory.GetOrCreate();
                        Transform pt = player.transform;
                        b.SetPosition(pt.position + new Vector3(0, pt.localScale.y, 0));
                        b.AddForce(player.GetShootingRandomForce());

                        Destroy(gameObject);
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetBallFactory(BallFactory ballFactory) {
            _ballFactory = ballFactory;
        }
    }
}