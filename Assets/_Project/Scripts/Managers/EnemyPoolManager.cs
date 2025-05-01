using UnityEngine;

namespace Game.Managers
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public static EnemyPoolManager Instance { get; private set; }

        [SerializeField] private GameObject normalEnemyPrefab;
        [SerializeField] private GameObject specialEnemyPrefab;

        [SerializeField] private Transform enemyNormalParent;
        [SerializeField] private Transform enemySpecialParent;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            ObjectPoolManager.Instance.CreatePool("Enemy_Normal", normalEnemyPrefab, 50, enemyNormalParent);
            ObjectPoolManager.Instance.CreatePool("Enemy_Special", specialEnemyPrefab, 20, enemySpecialParent);
        }
    }
}
