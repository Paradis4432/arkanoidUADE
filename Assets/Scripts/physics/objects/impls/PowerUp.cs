using UnityEngine;

namespace physics.objects.impls {
    public class PowerUp : Rectangle {
        public Manager manager;

        public void UpdateMe() {
            CalculateFisics();

            if (CollidingWithObjects()) return;
        }

        private bool CollidingWithObjects() {
            foreach (Movable movable in ObjectRepository.GetObjects().GetEnabledValues())
            {
                switch (movable)
                {
                    case LooseArea looseArea: {
                        CollisionValues col = CollisionDetector.Between(this, looseArea);
                        if (!col.hit) continue;

                        ObjectRepository.UnregisterObject(this);
                        gameObject.SetActive(false);
                        return true;
                    }
                    case Player player: {
                        CollisionValues col = CollisionDetector.Between(this, player);
                        if (!col.hit) continue;
                        //Debug.Log(col);

                        // spawn new balls
                        ObjectRepository.UnregisterObject(this);
                        gameObject.SetActive(false);

                        Ball b = manager.ballFactory.GetOrCreate();

                        Transform pt = player.transform;
                        b.SetPosition(pt.position + new Vector3(0, pt.localScale.y, 0));
                        b.AddForce(player.GetShootingRandomForce());

                        return true;
                    }
                }
            }

            return false;
        }
    }
}