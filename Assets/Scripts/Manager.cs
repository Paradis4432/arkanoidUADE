using physics;
using UnityEngine;

public class Manager : MonoBehaviour {
    [ContextMenu("Print objects")]
    public void PrintObjects() {
        foreach (Movable obj in ObjectRepository.GetObjects())
        {
            Debug.Log(obj);
        }
    }
}