using UnityEngine;

namespace physics {
    public class Positionable {
        public float X { get; set; }
        public float Y { get; set; }

        public Positionable(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }

        public void Reset() {
            X = 0;
            Y = 0;
        }
        
        public override string ToString() {
            return "{" +
                   "X=" + X +
                   ", Y=" + Y +
                   '}';
        }
    }
}