using System;
using System.Collections.Generic;
using physics.objects;
using UnityEngine;

namespace DefaultNamespace {
    public class CacheManager {
        private static readonly Dictionary<Rectangle, RectangleData> RectMap = new();

        public static RectangleData GetOrSave(Rectangle rec) {
            if (RectMap.TryGetValue(rec, out RectangleData value))
                return value;
            RectangleData newValue = new(rec.Width / 2, rec.Height / 2, rec.PosX, rec.PosY);
            RectMap[rec] = newValue;
            return newValue;
        }

        public static void Clear() {
            RectMap.Clear();
        }
    }
    
    public class RectangleData {
        public float w;
        public float h;
        public float px;
        public float py;
        
        public RectangleData(float width, float height, float posX, float posY) {
            w = width;
            h = height;
            px = posX;
            py = posY;  

        }
    }
}