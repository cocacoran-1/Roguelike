using Game.Managers;
using UnityEngine;
using Game.Player;

namespace Game.Weapon
{
    public class FireThrowerSkill : IWeaponSkill
    {
        private readonly FireThrowerSkillData skillData;
        private readonly Transform playerTransform;
        private readonly PlayerController playerController;
        private float fireTimer;

        public FireThrowerSkill(FireThrowerSkillData data, Transform player, PlayerController controller)
        {
            skillData = data;
            playerTransform = player;
            playerController = controller;
            fireTimer = 0f;
        }

        public float GetCooldown()
        {
            return skillData.cooldown;
        }

        public void Execute()
        {
            fireTimer += Time.deltaTime;
            if (fireTimer < skillData.fireInterval)
                return;

            fireTimer = 0f;

            Vector3 baseDir = playerController != null && playerController.inputVec != Vector2.zero
                ? playerController.inputVec.normalized
                : playerTransform.right;

            float halfAngle = skillData.coneAngle / 2f;
            for (int i = 0; i < skillData.projectilesPerShot; i++)
            {
                float angle = Random.Range(-halfAngle, halfAngle);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector3 dir = rotation * baseDir;

                GameObject flame = ObjectPoolManager.Instance.SpawnFromPool("FireThrowerFlame", playerTransform.position, Quaternion.FromToRotation(Vector3.right, dir));
                if (flame != null && flame.TryGetComponent(out ProjectileBase proj))
                {
                    proj.Init(skillData.damagePerShot, dir, skillData.projectileSpeed);
                }
            }
        }
    }
}
