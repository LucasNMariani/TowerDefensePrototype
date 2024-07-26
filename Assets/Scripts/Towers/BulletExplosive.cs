using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosive : BulletController
{
    [SerializeField]
    protected SphereCollider explosiveRange;
    [SerializeField]
    protected float explosiveRadius;

    private void Start()
    {
        explosiveRange.radius = explosiveRadius;
    }

    protected override void HitTarget(Transform enemy)
    {
        var enemies = enemy.gameObject.GetComponent<IDamageable>();
        if (enemies != null)
        {
            explosiveRange.gameObject.SetActive(true);
            GameObject particleInstance = (GameObject)Instantiate(impactParticleSystem, transform.position, transform.rotation);
            Destroy(particleInstance, 0.5f);
        }
    }
}
