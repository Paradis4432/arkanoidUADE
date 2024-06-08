using UnityEngine;
using UnityEngine.UIElements;

namespace physics {
    public class Movable : MonoBehaviour {
        protected float Ang { get; set; } = 0;
        public Velocity Vel { get; set; } = new(); // TODO check extra sets
        protected Aceleration Ace { get; set; } = new();
        protected Velocity VelAng { get; set; } = new();
        protected Aceleration AceAng { get; set; } = new();
        protected float Mass { get; set; } = 1;
        
        public float PosX => transform.position.x;
        public float PosY => transform.position.y;

        protected void CalculateFisics() {
            Vel.X += Ace.X * Time.deltaTime; 
            Vel.Y += Ace.Y * Time.deltaTime; 
            VelAng.X += AceAng.X * Time.deltaTime; 
            VelAng.Y += AceAng.Y * Time.deltaTime; 
            transform.position += new Vector3(Vel.X, Vel.Y, 0) * Time.deltaTime; 
            transform.Rotate(new Vector3(0, 0, VelAng.X * Time.deltaTime)); 

            Ace.Reset();
        }

        public void AddForce(Vector2 force) {
            Ace.X += force.x / Mass;
            Ace.Y += force.y / Mass;
        }

        public void AddTorque(float torque, float force = 1) {
            float inertia = Mass * force;
            AceAng.X += torque / inertia;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.05f); // Adjust the size as needed
        }
    }
}