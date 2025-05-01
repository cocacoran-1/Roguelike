using Game.Managers;
using Game.Weapon;
using System.Collections.Generic;
using UnityEngine;

public class BouncingStone : ProjectileBase
{
    private List<Collider2D> hitTargets = new List<Collider2D>();
    private Collider2D[] buffer = new Collider2D[50];

    public void Init(float damage, Vector3 dir, float speed, int maxBounce)
    {
        base.Init(damage, dir, speed);
        bounceCount = maxBounce;
        hitTargets.Clear();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (hitTargets.Contains(other)) return;

        hitTargets.Add(other);

        if (other.TryGetComponent(out IDamageable dmg))
            dmg.OnDamaged(damage, Vector2.zero);

        if (bounceCount <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        Collider2D next = WeaponUtils.FindNextClosest(
            transform.position,
            10f,
            buffer,
            EnemyManager.Instance.EnemyLayerMask,
            hitTargets
        );

        if (next == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 newDir = (next.transform.position - transform.position).normalized;
        direction = newDir;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, newDir);
        bounceCount--;
    }
}
