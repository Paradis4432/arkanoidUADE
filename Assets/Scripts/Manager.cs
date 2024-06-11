using physics;
using UnityEngine;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour {
    [SerializeField] private int maxHp = 3;
    public static int Hp;

    private void Start() {
        Hp = maxHp;
    }

    [ContextMenu("Print objects")]
    public void PrintObjects() {
        foreach (Movable obj in ObjectRepository.GetObjects())
        {
            Debug.Log(obj);
        }
    }
}