using physics.objects;
using physics.objects.impls;
using UnityEngine;

namespace powerups {
    [RequireComponent(typeof(BallFactory))]
    public class PowerUpManager : MonoBehaviour {
        [SerializeField] private PowerUp powerUpPrefab;

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
            /*Ball b = ballFactory.GetOrCreate(); // spawn a new powerup that falls to player
            b.SetPosition(ball.transform.position);
            b.AddForce(new Vector2(ball.initialForceXMin, ball.initialForceY));*/
            // spawn falling powerup
            PowerUp pw = Instantiate(powerUpPrefab);
            pw.SetPosition(ball.transform.position);
            pw.SetBallFactory(ballFactory);
        }
    }
}