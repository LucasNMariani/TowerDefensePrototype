﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRange : MonoBehaviour
{
    [SerializeField]
    private BulletController bullet;

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(bullet.bulletDamage);
            bullet.DestroyBullet();
        }
    }
}