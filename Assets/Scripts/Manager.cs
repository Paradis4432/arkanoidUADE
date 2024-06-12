using physics;
using physics.objects;
using physics.objects.impls;
using UnityEngine;

[RequireComponent(typeof(BallFactory))]
public class Manager : MonoBehaviour {
    public static bool Debugging = false;
    public static int PowerUpsLeft = 50;
    public static int Hp;

    [SerializeField] private int maxHp = 3;
    [SerializeField] private Player player;

    public BallFactory ballFactory;

    private void Start() {
        Hp = maxHp;
        ballFactory = GetComponent<BallFactory>();

        if (!Debugging)
            player.SetBall(ballFactory.GetOrCreate());

        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
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
        ObjectRepository.GetPowerUps().DisableAll();
        //ObjectRepository.GetBalls().Reset(); // balls should not reset
        // spawn only one

        player.Reset(); // player reset 
        player.SetBall(ballFactory.GetOrCreate());

        Hp = maxHp;
    }
}