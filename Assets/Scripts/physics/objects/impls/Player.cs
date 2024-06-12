using UnityEngine;
using Random = System.Random;

namespace physics.objects.impls {
    public class Player : Rectangle {
        public bool holdBall = true;

        private Ball ball;

        [SerializeField] private float speed = 2;

        public void SetBall(Ball ball) {
            this.ball = ball;
        }

        private void Update() {
            if (CollidingWithWalls())
            {
                Vel.X = -Vel.X * 1.2f;
            }

            CalculateFisics();

            if (Input.GetKey(KeyCode.A))
                AddForce(Vector3.left * (Time.deltaTime * speed));
            if (Input.GetKey(KeyCode.D))
                AddForce(Vector3.right * (Time.deltaTime * speed));

            if (holdBall)
                ball.transform.position = transform.position + new Vector3(0, transform.localScale.y + 0.4f, 0);

            if (Input.GetKeyDown(KeyCode.Space) && holdBall)
            {
                holdBall = false;
                ball.AddForce(GetShootingRandomForce());
            }
        }

        public Vector2 GetShootingRandomForce() {
            return new Vector2(
                new Random().Next(ball.initialForceXMin, ball.initialForceXMax),
                ball.initialForceY
            );
        }

        private bool CollidingWithWalls() {
            foreach (Wall wall in ObjectRepository.GetWalls().GetEnabledValues())
            {
                CollisionValues col = CollisionDetector.Between(this, wall);
                if (!col.hit) continue;

                return true;
            }

            return false;
        }

        public override void Reset() {
            holdBall = true;
            base.Reset();
        }
    }
}