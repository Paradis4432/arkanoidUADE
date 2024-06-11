using physics.objects;
using UnityEngine;

namespace powerups {
    [RequireComponent(typeof(BallFactory))]
    public class PowerUpManager : MonoBehaviour {
        public int powerUpChance = 10;
        public int powerUpsLeft = 3;
        public BallFactory ballFactory;

        /**
         * spawns a powerup if possible
         */
        [ContextMenu("Attempt Spawn Power Up")]
        public void AttemptSpawnPowerUp(Ball ball) {
            //if (powerUpsLeft == 0) return;

            powerUpsLeft--;
            Ball b = ballFactory.GetOrCreate(); // spawn a new powerup that falls to player
            b.SetPosition(ball.transform.position);
            b.AddForce(new Vector2(ball.initialForceXMin, ball.initialForceY));


            Debug.Log("spawning power up");
        }
    }
}