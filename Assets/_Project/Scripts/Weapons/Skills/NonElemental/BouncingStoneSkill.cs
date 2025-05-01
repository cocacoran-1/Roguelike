using UnityEngine;
using Game.Managers;
using System.Collections.Generic;

namespace Game.Weapon
{
    public class BouncingStoneSkill : IWeaponSkill
    {
        private BouncingStoneSkillData skillData;
        private Transform playerTransform;
        private Collider2D[] buffer = new Collider2D[50];

        public BouncingStoneSkill(BouncingStoneSkillData data, Transform player)
        {
            skillData = data;
            playerTransform = player;
        }

        public float GetCooldown()
        {
            return skillData.cooldown;
        }

        public void Execute()
        {
            Collider2D target = WeaponUtils.FindClosestTarget(
                playerTransform.position,
                skillData.range,
                buffer,
                EnemyManager.Instance.EnemyLayerMask
            );

            if (target == null) return;

            Vector3 dir = (target.transform.position - playerTransform.position).normalized;

            GameObject stone = ObjectPoolManager.Instance.SpawnFromPool(
                "BouncingStone",
                playerTransform.position,
                Quaternion.FromToRotation(Vector3.right, dir)
            );

            if (stone != null && stone.TryGetComponent(out BouncingStone proj))
            {
                proj.Init(skillData.baseDamage, dir, skillData.projectileSpeed, skillData.bounceCount);
            }
        }
    }
}
