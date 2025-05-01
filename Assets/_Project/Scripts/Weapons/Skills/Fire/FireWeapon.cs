using UnityEngine;
using Game.Player;
using Game.Weapon;
using Game.Managers;

namespace Game.Weapon
{
    public class FireWeapon : MonoBehaviour
    {
        private PlayerWeaponController playerWeaponController;

        private void Awake()
        {
            playerWeaponController = GetComponent<PlayerWeaponController>();
        }
        private void EquipInitialSkills()
        {
            if (playerWeaponController == null) return;

            playerWeaponController.AddWeapon(new FireBallSkill(
                SkillDataManager.Instance.FireBallSkillData,
                PlayerManager.Instance.PlayerTransform,
                EnemyManager.Instance.EnemyLayerMask));

            playerWeaponController.AddWeapon(new FirePillarSkill(
                SkillDataManager.Instance.FirePillarSkillData,
                PlayerManager.Instance.PlayerTransform,
                EnemyManager.Instance.EnemyLayerMask));

            playerWeaponController.AddWeapon(new FireThrowerSkill(
                SkillDataManager.Instance.FireThrowerSkillData,
                PlayerManager.Instance.PlayerTransform,
                PlayerManager.Instance.PlayerController));

            playerWeaponController.AddWeapon(new FireZoneSkill(
                SkillDataManager.Instance.FireZoneSkillData,
                PlayerManager.Instance.PlayerTransform));
        }
    }
}
