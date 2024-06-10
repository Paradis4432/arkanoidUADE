using System;
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

        public void Reset() {
            Vel = new Velocity();
            Ace = new Aceleration();
            VelAng = new Velocity();
            AceAng = new Aceleration();
        }

        private void Start() {
            ObjectRepository.RegisterObject(this);
        }

        private void OnDestroy() {
            ObjectRepository.UnregisterObject(this);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.05f); // Adjust the size as needed
        }
    }
}