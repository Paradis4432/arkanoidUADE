using UnityEngine;

namespace physics.objects.impls {
    public class LooseArea : Rectangle {
        public static Manager Manager;

        public static void HandleBallFalling() {
            // a ball might hit loosing area but it might not mean the player lost

            // if life == 1 -> go to lost screen
            // else if no active balls left loose 1 life
            // 

            Debug.Log("LOOSING");

            if (Manager.Hp == 1)
            {
                // go to lost screen
                return;
            }

            // check if there are any active balls left
            if (ObjectRepository.GetBalls().GetEnabledValues().Count == 0)
            {
                Manager.Hp--;
                // TODO reset

                Manager.Reset();
            }
        }
    }
}