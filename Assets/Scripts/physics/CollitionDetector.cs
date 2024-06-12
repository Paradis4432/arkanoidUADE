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
            return Between(ball.PosX, ball.PosY, ball.Radius, rectangle);
        }

        public static CollisionValues Between(Vector2 bPos, float r, Rectangle rectangle) {
            return Between(bPos.x, bPos.y, r, rectangle);
        }

        public static CollisionValues Between(float bpx, float bpy, float r, Rectangle rectangle) {
            RectangleData rec = CacheManager.GetOrSave(rectangle);
            return Between(bpx, bpy, r, rec.w, rec.h, rec.px, rec.py);
            //return Between(bpx, bpy, r, rectangle.Width / 2, rectangle.Height / 2, rectangle.PosX, rectangle.PosY);
        }

        public static CollisionValues Between(float bpx, float bpy, float r, float w, float h, float rx, float ry) {
            // top left is scale / 2 to the left and width is scale
            float cx;
            float cy;

            if (bpx > rx + w) cx = rx + w;
            else if (bpx < rx - w) cx = rx - w;
            else cx = bpx;

            if (bpy > ry + h) cy = ry + h;
            else if (bpy < ry - h) cy = ry - h;
            else cy = bpy;


            float dx = cx - bpx;
            float dy = cy - bpy;
            float d = Mathf.Sqrt(dx * dx + dy * dy);
            //Debug.Log("dx: " + dx + " dy: " + dy + " d: " + d);

            return new CollisionValues(dx, dy, d <= r);
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