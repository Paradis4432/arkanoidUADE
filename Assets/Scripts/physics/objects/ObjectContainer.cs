using System;
using System.Collections.Generic;

namespace physics.objects {
    public class ObjectContainer<TO> where TO : Movable {
        private readonly HashSet<TO> _values = new();
        private readonly HashSet<TO> _disabledValues = new();

        public void Reset() {
            //_values.Clear();
            foreach (TO disabledValue in _disabledValues)
            {
                disabledValue.ResetMovable();
                _values.Add(disabledValue);
            }

            _disabledValues.Clear();
        }

        public void Disable(TO obj) {
            if (!_values.Contains(obj))
                throw new Exception("Object not in container");

            //Debug.Log("disabling " + obj + " in " + this + " container");
            _values.Remove(obj);
            _disabledValues.Add(obj);
            //Debug.Log("_disabledValues: " + _disabledValues.Count);
        }

        public void DisableAll() {
            foreach (TO movable in _values)
            {
                movable.gameObject.SetActive(false);
                _disabledValues.Add(movable);
            }

            _values.Clear();
        }

        public void Enable(TO obj) {
            if (!_disabledValues.Contains(obj))
                throw new Exception("Object is not disabled");

            _disabledValues.Remove(obj);
            _values.Add(obj);
        }

        public void Add(TO obj) {
            _values.Add(obj);
        }

        public bool Contains(TO obj) {
            return _values.Contains(obj);
        }

        public void Remove(TO obj) {
            _values.Remove(obj);
        }

        public void Clear() {
            foreach (TO movable in _values) movable.Delete();
            foreach (TO movable in _disabledValues) movable.Delete();

            _values.Clear();
            _disabledValues.Clear();
        }

        public HashSet<TO> GetEnabledValues() {
            return _values;
        }

        public HashSet<TO> GetDisabledValues() {
            return _disabledValues;
        }
    }
}