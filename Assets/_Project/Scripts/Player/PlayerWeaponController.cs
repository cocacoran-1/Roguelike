using UnityEngine;
using System.Collections.Generic;
using Game.Weapon;

namespace Game.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private List<IWeaponSkill> weaponSkills = new List<IWeaponSkill>();
        private List<float> cooldownTimers = new List<float>();
        public int WeaponCount => weaponSkills.Count;

        private void Update()
        {
            for (int i = 0; i < weaponSkills.Count; i++)
            {
                cooldownTimers[i] -= Time.deltaTime;
                if (cooldownTimers[i] <= 0f)
                {
                    weaponSkills[i].Execute();
                    cooldownTimers[i] = weaponSkills[i].GetCooldown();
                }
            }
        }

        public void AddWeapon(IWeaponSkill newSkill)
        {
            weaponSkills.Add(newSkill);
            cooldownTimers.Add(0f);
        }

    }
}


