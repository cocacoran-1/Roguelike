using UnityEngine;
using Game.Managers;

namespace Game.Weapon
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class FireZone : MonoBehaviour
    {
        private float damagePerSecond;
        private float radius;
        private float damageInterval = 0.5f;
        private float damageTimer;
        private Transform playerTransform;

        private Collider2D[] hitBuffer = new Collider2D[50];
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private ParticleSystem flameEffect;

        public void Init(float damage, float radius, Transform player)
        {
            this.damagePerSecond = damage;
            this.radius = radius;
            this.playerTransform = player;

            damageTimer = 0f;

            CircleCollider2D coll = GetComponent<CircleCollider2D>();
            coll.radius = radius;
            float scale = radius - radius * 0.15f;
            transform.localScale = new Vector3(scale, scale, 1f);
            coll.isTrigger = true;

            if (flameEffect != null)
                flameEffect.Play();
        }

        private void Update()
        {
            if (playerTransform != null)
            {
                transform.position = playerTransform.position;
            }

            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0f;
                ApplyDamage();
            }
        }

        private void ApplyDamage()
        {
            if (enemyLayer == 0)
            {
                Debug.LogError("enemyLayer가 설정되어 있지 않습니다.");
                return;
            }

            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, radius, hitBuffer, enemyLayer);
            for (int i = 0; i < hitCount; i++)
            {
                if (hitBuffer[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.OnDamaged(damagePerSecond * damageInterval, Vector2.zero);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
