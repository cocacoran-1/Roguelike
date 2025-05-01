using Game.Weapon;
using UnityEngine;

namespace Game.Managers
{
    public class SkillDataManager : MonoBehaviour
    {
        public static SkillDataManager Instance { get; private set; }

        [Header("파이어 스킬 데이터")]
        public FireBallSkillData FireBallSkillData;
        public FirePillarSkillData FirePillarSkillData;
        public FireThrowerSkillData FireThrowerSkillData;
        public FireZoneSkillData FireZoneSkillData;

        [Header("라이트닝 스킬 데이터")]
        public LightningChainSkillData LightningChainSkillData;
        public BouncingStoneSkillData BouncingStoneSkillData;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
