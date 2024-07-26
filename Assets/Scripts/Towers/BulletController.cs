using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected float timeToDestroyBullet;
    protected float _timer;
    protected Transform target;
    [HideInInspector]
    public float bulletDamage;
    [SerializeField]
    protected GameObject impactParticleSystem;
    
    public virtual void SetBulletDamage(float dmg)
    {
        bulletDamage = dmg;
    }
    protected virtual void Update()
    {
        #region BulletGoToTarget
        if (target == null)
        {
            DestroyBullet();
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target.transform);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        #endregion

        //transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer > timeToDestroyBullet || target == null)
        {
            DestroyBullet();
            return;
        }
    }

    public virtual void SearchTarget(Transform _target)
    {
        target = _target;
    }


    protected virtual void HitTarget(Transform enemy)
    {
        var enemies = enemy.gameObject.GetComponent<IDamageable>();
        if (enemies != null)
        {
            //Debug.Log("Impactó la bala");
            enemies.TakeDamage(bulletDamage);
            if (impactParticleSystem != null)
            {
                GameObject particleInstance = (GameObject)Instantiate(impactParticleSystem, transform.position, transform.rotation);
                Destroy(particleInstance, 0.5f);
            }
            DestroyBullet();
        }
    }

    public virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
