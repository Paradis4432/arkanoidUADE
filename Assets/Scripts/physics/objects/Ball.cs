using physics.objects.impls;
using UnityEngine;

namespace physics.objects {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : Movable {
        public float initialForceX = 30f;
        public float initialForceY = 300f;
        public float Radius => transform.localScale.x / 2;

        private void Start() {
            ObjectRepository.RegisterObject(this);
            //AddForce(new Vector2(initialForceX, initialForceY));
        }

        private void OnDestroy() {
            ObjectRepository.UnregisterObject(this);
        }

        private void Update() {
            CalculateFisics();

            foreach (Movable movable in ObjectRepository.GetObjects()) {
                if (movable is not Rectangle rectangle) continue;
        
                CollisionValues col = CollisionDetector.Between(this, rectangle);
                Debug.Log(col);
                if (!col.hit) continue;

                if (Mathf.Abs(col.distX) > 0.70f) {
                    Vel.X = -Vel.X;
                    if (rectangle is Obstacle obstacle) Destroy(obstacle.gameObject);
                    break;
                }

                if (Mathf.Abs(col.distY) > 0.70f) {
                    Vel.Y = -Vel.Y;
                    if (rectangle is Obstacle obstacle) Destroy(obstacle.gameObject);
                    break;
                }
            }
        }

    }
}