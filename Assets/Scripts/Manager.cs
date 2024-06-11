using physics;
using physics.objects;
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
        foreach (Movable enabledValue in ObjectRepository.GetObjects().GetEnabledValues())
            Debug.Log("Enabled Object: " + enabledValue);
        foreach (Obstacle enabledValue in ObjectRepository.GetObstacles().GetEnabledValues())
            Debug.Log("Enabled Obstacle: " + enabledValue);
        foreach (Wall enabledValue in ObjectRepository.GetWalls().GetEnabledValues())
            Debug.Log("Enabled Wall: " + enabledValue);
        foreach (Ball enabledValue in ObjectRepository.GetBalls().GetEnabledValues())
            Debug.Log("Enabled Ball: " + enabledValue);
        foreach (Movable enabledValue in ObjectRepository.GetObjects().GetDisabledValues())
            Debug.Log("Disabled Object: " + enabledValue);
        foreach (Obstacle enabledValue in ObjectRepository.GetObstacles().GetDisabledValues())
            Debug.Log("Disabled Obstacle: " + enabledValue);
        foreach (Wall enabledValue in ObjectRepository.GetWalls().GetDisabledValues())
            Debug.Log("Disabled Wall: " + enabledValue);
        foreach (Ball enabledValue in ObjectRepository.GetBalls().GetDisabledValues())
            Debug.Log("Disabled Ball: " + enabledValue);
    }

    [ContextMenu("Reset")]
    public void Reset() {
        ObjectRepository.GetObjects().Reset();
        ObjectRepository.GetObstacles().Reset();
        ObjectRepository.GetWalls().Reset();
        ObjectRepository.GetBalls().Reset();

        player.Reset(); 
    }
}