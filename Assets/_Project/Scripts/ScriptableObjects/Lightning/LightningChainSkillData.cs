using Game.Weapon;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/LightningChainSkillData")]
public class LightningChainSkillData : SkillData
{
    public float damage = 10f;
    public int maxChains = 4;
}
