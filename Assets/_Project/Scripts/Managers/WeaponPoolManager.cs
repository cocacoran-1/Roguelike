using UnityEngine;

namespace Game.Managers
{
    public class WeaponPoolManager : MonoBehaviour
    {
        public static WeaponPoolManager Instance { get; private set; }

        [Header("Fire")]
        [SerializeField] private GameObject fireBallPrefab;
        [SerializeField] private GameObject firePillarPrefab;
        [SerializeField] private GameObject fireThrowerFlamePrefab;
        [SerializeField] private Transform fireBallParent;
        [SerializeField] private Transform firePillarParent;
        [SerializeField] private Transform fireThrowerFlameParent;

        [Header("Lightning")]
        [SerializeField] private GameObject lightningChainPrefab;
        [SerializeField] private Transform lightningChainParent;
        [SerializeField] private GameObject bouncingStonePrefab;
        [SerializeField] private Transform bouncingStoneParent;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            ObjectPoolManager.Instance.CreatePool("FireBall", fireBallPrefab, 30, fireBallParent);
            ObjectPoolManager.Instance.CreatePool("FirePillar", firePillarPrefab, 20, firePillarParent);
            ObjectPoolManager.Instance.CreatePool("FireThrowerFlame", fireThrowerFlamePrefab, 50, fireThrowerFlameParent);
            ObjectPoolManager.Instance.CreatePool("BouncingStone", bouncingStonePrefab, 20, bouncingStoneParent);
        }
    }
}
