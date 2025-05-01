using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class FirePillarSkill : IWeaponSkill
    {
        private readonly FirePillarSkillData skillData;
        private readonly Transform playerTransform;
        private readonly LayerMask enemyLayerMask;
        private readonly Collider2D[] hitBuffer = new Collider2D[60];

        public FirePillarSkill(FirePillarSkillData data, Transform player, LayerMask enemyLayer)
        {
            skillData = data;
            playerTransform = player;
            enemyLayerMask = enemyLayer;
        }

        public float GetCooldown()
        {
            return skillData.cooldown;
        }

        public void Execute()
        {
            Collider2D[] buffer = new Collider2D[10];
            List<Collider2D> visibleEnemies = WeaponUtils.FindEnemiesInViewport(playerTransform.position, skillData.range, buffer, enemyLayerMask);

            if (visibleEnemies.Count == 0)
            {
                Debug.LogWarning("No visible enemies found for FirePillarSkill.");
                return;
            }

            int targetCount = Mathf.Min(skillData.maxTargets, visibleEnemies.Count);
            List<Collider2D> selectedTargets = new List<Collider2D>();

            for (int i = 0; i < targetCount; i++)
            {
                if (visibleEnemies.Count == 0) break;
                int randomIndex = Random.Range(0, visibleEnemies.Count);
                selectedTargets.Add(visibleEnemies[randomIndex]);
                visibleEnemies.RemoveAt(randomIndex);
            }

            foreach (var target in selectedTargets)
            {
                Vector3 position = target.transform.position;
                GameObject pillar = ObjectPoolManager.Instance.SpawnFromPool("FirePillar", position, Quaternion.identity);

                if (pillar != null && pillar.TryGetComponent(out FirePillar firePillar))
                {
                    firePillar.Init(skillData.baseDamage, skillData.duration);
                }
            }
        }
    }
}
