using physics;
using physics.objects.impls;
using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField] private int maxHp = 3;
    [SerializeField] private Player player;
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

    [ContextMenu("Reset")]
    public void Reset() {
        Hp = maxHp;
        foreach (Movable obj in ObjectRepository.GetObjects())
        {
            obj.Reset();
        }

        player.holdBall = true;
    }
}