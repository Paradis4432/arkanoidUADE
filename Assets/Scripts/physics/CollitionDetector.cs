using physics.objects;
using UnityEngine;

namespace physics {
    public abstract class CollisionDetector {
        public static CollisionValues Between(Ball b0, Ball b1) {
            float dx = b1.PosX - b0.PosX;
            float dy = b1.PosY - b0.PosY;
            float d = Mathf.Sqrt(dx * dx + dy * dy);
            return new CollisionValues(dx, dy, d <= b0.Radius + b1.Radius);
        }

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