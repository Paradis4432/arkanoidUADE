using System.Collections.Generic;
using System.Diagnostics;
using DefaultNamespace;
using physics;
using physics.objects;
using physics.objects.impls;
using UnityEngine;

public class UpdateManager : MonoBehaviour {
    public Player player;
    private Stopwatch _stopwatch;

    private void Start() {
        _stopwatch = new Stopwatch();
    }

    private int updateCounter;

    private void Update() {
        if (++updateCounter >= 10)
        {
            CacheManager.Clear();
            updateCounter = 0;
        }

        if (ObjectRepository.GetObstacles().GetEnabledValues().Count == 0) // || paused
        {
            //Debug.Log("Player won");
            return;
        }

        player.UpdateMe();

        _stopwatch.Reset();
        _stopwatch.Start();

        //int bCount = ObjectRepository.GetBalls().GetEnabledValues().Count;

        /*foreach (Ball enabledValue in ObjectRepository.GetBalls().GetEnabledValues())
            enabledValue.UpdateMe();*/
        HashSet<Ball> balls = ObjectRepository.GetBalls().GetEnabledValues();
        HashSet<Ball>.Enumerator ballsEn = balls.GetEnumerator();
        while (ballsEn.MoveNext())
        {
            Ball current = ballsEn.Current;
            if (current is null)
                return;
            current.UpdateMe(); // checks player, loose area

            if (!balls.Contains(current))
            {
                ballsEn = balls.GetEnumerator();
            }
        }

        HashSet<PowerUp> powerUps = ObjectRepository.GetPowerUps().GetEnabledValues();
        //int puCount = powerUps.Count;
        HashSet<PowerUp>.Enumerator powerUpsEn = powerUps.GetEnumerator();
        while (powerUpsEn.MoveNext())
        {
            PowerUp current = powerUpsEn.Current;
            if (current is null)
                return;
            current.UpdateMe(); // checks player, loose area

            if (!powerUps.Contains(current))
            {
                powerUpsEn = powerUps.GetEnumerator();
            }
        }

        _stopwatch.Stop();
        //Debug.Log($"Time taken to update {bCount} Balls and {puCount} power ups: {_stopwatch.ElapsedMilliseconds} ms "); // 50 balls 85 ms for 480 bricks with deep profiling
    }
}