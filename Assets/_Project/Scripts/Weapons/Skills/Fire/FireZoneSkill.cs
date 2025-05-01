using Game.Managers;
using UnityEngine;

namespace Game.Weapon
{
    public class FireZoneSkill : IWeaponSkill
    {
        private readonly FireZoneSkillData skillData;
        private readonly Transform playerTransform;

        public FireZoneSkill(FireZoneSkillData data, Transform player)
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
           
        }
    }
}
