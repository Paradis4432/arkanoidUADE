using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace physics.objects.impls {
    public class Player : Rectangle {
        public bool holdBall = true;

        public Ball ball;

        [SerializeField] private float speed = 2;
        [SerializeField] private float limitLeftX = 94.7f;
        [SerializeField] private float limitRightX = 109.72f;

        public void SetBall(Ball ball) {
            this.ball = ball;
        }

        private void Update() {
            if (Input.GetKey(KeyCode.A) && transform.position.x > limitLeftX)
                transform.position += Vector3.left * (Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.D) && transform.position.x < limitRightX)
                transform.position += Vector3.right * (Time.deltaTime * speed);

            //AddForce(new Vector2(initialForceX, initialForceY));
            if (holdBall)
                ball.transform.position = transform.position + new Vector3(0, transform.localScale.y + 0.4f, 0);

            if (Input.GetKeyDown(KeyCode.Space) && holdBall)
            {
                holdBall = false;
                ball.AddForce(
                    new Vector2(
                        new Random().Next(ball.initialForceXMin, ball.initialForceXMax),
                        ball.initialForceY
                    )
                );
            }
        }

        protected new void Reset() {
            holdBall = true;
            base.Reset();
        }
    }
}