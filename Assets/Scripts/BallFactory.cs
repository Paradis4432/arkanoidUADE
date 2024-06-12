using physics;
using physics.objects;
using UnityEngine;

public class BallFactory : MonoBehaviour {
    public Ball ballPrefab;

    public Ball GetOrCreate() {
        // if found one in object container disabled values -> grab and return that 
        // return new instance of prefab
        
        //Debug.Log("requesting new ball, disabled: " + ObjectRepository.GetBalls().GetDisabledValues().Count);
        foreach (Ball disabledValue in ObjectRepository.GetBalls().GetDisabledValues())
        {
            //Debug.Log("using: " + disabledValue);
            ObjectRepository.GetBalls().Enable(disabledValue);
            disabledValue.ResetMovable();
            return disabledValue;
        }

        //Debug.Log("created a new ball, disabled: " + ObjectRepository.GetBalls().GetDisabledValues().Count);
        Ball newBall = Instantiate(ballPrefab);
        return newBall;
    }
}