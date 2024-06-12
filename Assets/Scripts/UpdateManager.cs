using System.Collections.Generic;
using System.Diagnostics;
using physics;
using physics.objects;
using physics.objects.impls;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class UpdateManager : MonoBehaviour {
    public Player player;
    private Stopwatch _stopwatch;

    private void Start() {
        _stopwatch = new Stopwatch();
    }

    private void Update() {
        if (ObjectRepository.GetObstacles().GetEnabledValues().Count == 0) // || paused
        {
            //Debug.Log("Player won");
            return;
        }

        player.UpdateMe();

        _stopwatch.Reset();
        _stopwatch.Start();

        int bCount = ObjectRepository.GetBalls().GetEnabledValues().Count;
        foreach (Ball enabledValue in ObjectRepository.GetBalls().GetEnabledValues())
        {
            enabledValue.UpdateMe();
        }

        _stopwatch.Stop();
        Debug.Log($"Time taken to update {bCount} Balls: {_stopwatch.ElapsedMilliseconds} ms"); // 50 balls 85 ms

        HashSet<PowerUp> powerUps = ObjectRepository.GetPowerUps().GetEnabledValues();
        HashSet<PowerUp>.Enumerator en = powerUps.GetEnumerator();
        while (en.MoveNext())
        {
            PowerUp current = en.Current;
            if (current is null)
                return;
            current.UpdateMe();

            if (!powerUps.Contains(current))
            {
                en = powerUps.GetEnumerator();
            }
        }
    }
}