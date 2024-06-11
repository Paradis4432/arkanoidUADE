using UnityEngine;

namespace physics {
    public abstract class Movable : MonoBehaviour {
        public float PosX => transform.position.x;
        public float PosY => transform.position.y;

        protected float Ang { get; set; } = 0;
        protected Velocity Vel { get; private set; } = new();
        private Aceleration Ace { get; set; } = new();
        private Velocity VelAng { get; set; } = new();
        private Aceleration AceAng { get; set; } = new();
        private float Mass { get; set; } = 1;

        private Vector2 _initialPos;

        protected void CalculateFisics() {
            Vel.X += Ace.X;
            Vel.Y += Ace.Y;
            VelAng.X += AceAng.X;
            VelAng.Y += AceAng.Y;
            transform.position += new Vector3(Vel.X, Vel.Y, 0);
            transform.Rotate(new Vector3(0, 0, VelAng.X));

            Ace.Reset();
        }


        protected void Start() {
            ObjectRepository.RegisterObject(this);
            _initialPos = transform.position;
        }

        public virtual void Reset() {
            //Debug.Log("resetting: " + this);
            Vel = new Velocity();
            Ace = new Aceleration();
            VelAng = new Velocity();
            AceAng = new Aceleration();
            transform.position = _initialPos;
            gameObject.SetActive(true);
        }

        public void AddForce(Vector2 force) {
            Ace.X += force.x / Mass;
            Ace.Y += force.y / Mass;
        }

        public void AddTorque(float torque, float force = 1) {
            float inertia = Mass * force;
            AceAng.X += torque / inertia;
        }

        public void Disable() {
            //Debug.Log("disabling " + this);
            ObjectRepository.UnregisterObject(this);
            gameObject.SetActive(false);
        }

        public void Delete() {
            ObjectRepository.UnregisterObject(this);
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.05f); // Adjust the size as needed
        }
    }
}