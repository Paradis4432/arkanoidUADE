using physics;
using physics.objects;
using UnityEngine;
using UnityEngine.Serialization;

public class BallFactory : MonoBehaviour{
    public Ball ballPrefab;

    public Ball GetOrCreate() {
        // if found one in object container disabled values -> grab and return that 
        // return new instance of prefab
        
        Ball newBall = Instantiate(ballPrefab);
        return newBall;
    }
}