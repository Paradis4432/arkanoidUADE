using System.Collections.Generic;
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


        private static Dictionary<Rectangle, Vector2> cachePositions = new();

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
            //Debug.Log("dx: " + dx + " dy: " + dy + " d: " + d);

            return new CollisionValues(dx, dy, d <= ball.Radius);
        }

        public static CollisionValues Between(Rectangle r0, Rectangle r1) {
            float dx = r1.PosX - r0.PosX;
            float dy = r1.PosY - r0.PosY;

            // Check for collision using axis-aligned bounding box (AABB) method
            bool isColliding = Mathf.Abs(dx) * 2 < r0.Width + r1.Width &&
                               Mathf.Abs(dy) * 2 < r0.Height + r1.Height;

            return new CollisionValues(dx, dy, isColliding);
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