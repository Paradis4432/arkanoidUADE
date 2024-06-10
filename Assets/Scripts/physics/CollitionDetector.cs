using physics.objects;
using UnityEngine;

namespace physics {
    public abstract class CollisionDetector {
        public static bool Between(Ball b0, Ball b1) {
            return false;
        }

        /*public static CollisionValues Between(Ball b, Rectangle r) {
            float distX = b.PosX - r.PosX;
            float distY = b.PosY - r.PosY;
            double dist = Mathf.Sqrt(distX * distX + distY * distY);
            //Debug.Log("X: " + distX + " Y: " + distY + " DIST: " + dist);
            //return dist <= b.Radius * 2;
            return new CollisionValues(distX, distY, dist <= b.Radius * 2);
        }*/
        public static CollisionValues Between(Ball ball, Rectangle rectangle) {
            // top left is scale / 2 to the left and width is scale
            float cx;
            float cy;

            float w = rectangle.Width / 2;
            float h = rectangle.Height / 2;

            float rx = rectangle.PosX;
            float ry = rectangle.PosY;

            if (ball.PosX > rx + w) cx = rx + w;
            else if (ball.PosX < rx - w) cx = rx - w;
            else cx = ball.PosX;

            if (ball.PosY > ry + h) cy = ry + h;
            else if (ball.PosY < ry - h) cy = ry - h;
            else cy = ball.PosY;


            float dx = cx - ball.PosX;
            float dy = cy - ball.PosY;
            float d = Mathf.Sqrt(dx * dx + dy * dy);
            
            return new CollisionValues(dx, dy, d <= ball.Radius);
        }

        public static bool Between(Rectangle r0, Rectangle r1) {
            return false;
        }
    }

    public class CollisionValues {
        public float distX;
        public float distY;
        public bool hit;

        public CollisionValues(float distX, float distY, bool hit) {
            this.distX = distX;
            this.distY = distY;
            this.hit = hit;
        }

        public override string ToString() {
            return "CollisionValues{" +
                   "distX=" + distX +
                   ", distY=" + distY +
                   ", hit=" + hit +
                   '}';
        }
    }
}