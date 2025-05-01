using Game.Managers;
using UnityEngine;

namespace Game.Weapon
{
    public class FireBallSkill : IWeaponSkill
    {
        private readonly FireBallSkillData skillData;
        private readonly Transform playerTransform;
        private readonly LayerMask enemyLayerMask;
        private readonly Collider2D[] hitBuffer = new Collider2D[60];

        public FireBallSkill(FireBallSkillData data, Transform player, LayerMask enemyLayer)
        {
            skillData = data;
            playerTransform = player;
            enemyLayerMask = enemyLayer;
        }

        public void Execute()
        {
            Collider2D closest = WeaponUtils.FindClosestTarget(
                playerTransform.position,
                skillData.range,
                hitBuffer,
                enemyLayerMask
            );

            if (closest == null)
                return;

            Vector3 dir = (closest.transform.position - playerTransform.position).normalized;
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("FireBall", playerTransform.position, Quaternion.FromToRotation(Vector3.right, dir));
            if (bullet != null && bullet.TryGetComponent(out ProjectileBase proj))
            {
                proj.Init(skillData.baseDamage, dir, skillData.projectileSpeed);
            }
        }

        public float GetCooldown()
        {
            return skillData.cooldown;
        }
    }
}
