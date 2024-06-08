using UnityEngine;

namespace physics.objects.impls {
    public class Player : Rectangle{
        [SerializeField] private float speed = 2;
    
        private void Update() {
            if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * (Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * (Time.deltaTime * speed);
        }
    }
}