using System.Collections.Generic;

namespace physics.objects {
    public class ObjectContainer<O> where O : Movable {
        public readonly HashSet<O> Values = new();
        public readonly HashSet<O> DisabledValues = new();

        public void Reset() {
            foreach (O disabledValue in DisabledValues)
            {
                disabledValue.Reset();
                Values.Add(disabledValue);
            }

            DisabledValues.Clear();
        }

        public void Disable(O obj) {
            if (!Values.Contains(obj))
                throw new System.Exception("Object not in container");
            Values.Remove(obj);
            DisabledValues.Add(obj);
        }
    }
}