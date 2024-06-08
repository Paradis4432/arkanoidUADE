namespace physics.objects {
    public class Rectangle : Movable {
        public float Width => transform.localScale.x / 2;
        public float Height => transform.localScale.y / 2;
        

        private void Start() {
            ObjectRepository.RegisterObject(this);
        }

        private void OnDestroy() {
            ObjectRepository.UnregisterObject(this);
        }

        private void Update() {
            CalculateFisics();
        }
    }
}