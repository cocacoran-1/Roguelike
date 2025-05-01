using UnityEngine;
using Game.Player;
using Game.Weapon;
using Game.Managers;

namespace Game.Weapon
{
    public class LightWeapon : MonoBehaviour
    {
        private PlayerWeaponController playerWeaponController;

        private void Awake()
        {
            playerWeaponController = GetComponent<PlayerWeaponController>();
            if (playerWeaponController == null)
            {
                Debug.LogError("PlayerWeaponController를 찾을 수 없습니다.");
            }
        }

        private void EquipInitialSkills()
        {
            if (playerWeaponController == null) return;

        }
    }
}
