using Game.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(fileName = "BouncingStoneSkillData", menuName = "Skills/BouncingStoneSkillData")]
    public class BouncingStoneSkillData : SkillData
    {
        public float baseDamage = 10f;
        public float projectileSpeed = 5f;
        public int bounceCount = 3;
    }
}