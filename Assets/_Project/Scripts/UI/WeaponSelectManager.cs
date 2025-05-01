using UnityEngine;
using Game.Managers;
using Game.Player;
using Game.Weapon;

namespace Game.UI
{
    public class WeaponSelectManager : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponController playerWeaponController;
        [SerializeField] private GameObject fireZonePrefab;
        private const int MaxWeapons = 6;
        private FireZone activeFireZone;
        public void SelectFireBall()
        {
            if (playerWeaponController.WeaponCount >= MaxWeapons)
                return;

            playerWeaponController.AddWeapon(new FireBallSkill(
                SkillDataManager.Instance.FireBallSkillData,
                PlayerManager.Instance.PlayerTransform,
                EnemyManager.Instance.EnemyLayerMask));
        }

        public void SelectFirePillar()
        {
            if (playerWeaponController.WeaponCount >= MaxWeapons)
                return;

            playerWeaponController.AddWeapon(new FirePillarSkill(
                SkillDataManager.Instance.FirePillarSkillData,
                PlayerManager.Instance.PlayerTransform,
                EnemyManager.Instance.EnemyLayerMask));
        }

        public void SelectFireThrower()
        {
            if (playerWeaponController.WeaponCount >= MaxWeapons)
                return;

            playerWeaponController.AddWeapon(new FireThrowerSkill(
                SkillDataManager.Instance.FireThrowerSkillData,
                PlayerManager.Instance.PlayerTransform,
                PlayerManager.Instance.PlayerController));
        }

        public void SelectFireZone()
        {
            if (activeFireZone != null)
                return;

            GameObject zoneObj = Instantiate(
                fireZonePrefab,
                PlayerManager.Instance.PlayerTransform.position,
                Quaternion.identity,
                playerWeaponController.transform
                );
            if (zoneObj != null && zoneObj.TryGetComponent(out FireZone fireZone))
            {
                fireZone.Init(
                    SkillDataManager.Instance.FireZoneSkillData.damagePerSecond,
                    SkillDataManager.Instance.FireZoneSkillData.radius,
                    PlayerManager.Instance.PlayerTransform
                );
                activeFireZone = fireZone;
            }
        }
        public void SelectLightningChain()
        {
            if (playerWeaponController.WeaponCount >= MaxWeapons)
                return;

           
        }

    }
}