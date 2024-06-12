namespace physics.objects {
    public class Rectangle : Movable {
        public float Width => transform.localScale.x;
        public float Height => transform.localScale.y;
    }
}