using physics;
using physics.objects.impls;
using UnityEngine;

namespace DefaultNamespace {
    public class PowerUpFactory : MonoBehaviour {
        public PowerUp powerUpPrefab;

        public PowerUp GetOrCreate() {
            foreach (PowerUp disabledValue in ObjectRepository.GetPowerUps().GetDisabledValues())
            {
                ObjectRepository.GetPowerUps().Enable(disabledValue);
                disabledValue.Reset();
                return disabledValue;
            }

            PowerUp pup = Instantiate(powerUpPrefab);
            pup.gameObject.SetActive(true);
            return pup;
        }
    }
}