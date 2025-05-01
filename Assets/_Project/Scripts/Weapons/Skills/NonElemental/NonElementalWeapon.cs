using UnityEngine;
using Game.Player;
using Game.Weapon;
using Game.Managers;

public class NonElementalWeapon : MonoBehaviour
{
   private PlayerWeaponController playerWeaponController;
    private void Awake()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
    }

    private void EquipInitialSkills()
    {
        if (playerWeaponController == null) return;

        playerWeaponController.AddWeapon(new BouncingStoneSkill(
            SkillDataManager.Instance.BouncingStoneSkillData,
            PlayerManager.Instance.PlayerTransform
        ));
    }

}
