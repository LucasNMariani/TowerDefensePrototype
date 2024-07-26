using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    string       _targetTag;
    Transform    _target;
    Transform[]  _bulletShootingPointTransform;
    GameObject[] _bulletShootingPointGO;
    GameObject   _bulletPrefab;
    GameObject   _towerHead;
    float        _shootingRange;
    float        _shootingRate;
    float        _lazerDmg;
    bool         _useLaser;
    bool         _targetEnemyWithMoreHealth;
    LineRenderer _lineRenderer;


    float timeToNextBullet;

    public void SetVariables(float radiusOfCollider, string tagName, GameObject[] shootingPointGO, Transform[] shootingPointT, float shootRate, GameObject bulletPrefabRef, GameObject towerHeadRef, bool useLaser, LineRenderer lr, float lazerDmg, bool targetMoreHealth)
    {
        GetComponent<SphereCollider>().radius = radiusOfCollider;
        _shootingRange = radiusOfCollider;
        _targetTag = tagName;
        _bulletShootingPointGO = shootingPointGO;
        _bulletShootingPointTransform = shootingPointT;
        _shootingRate = shootRate;
        _bulletPrefab = bulletPrefabRef;
        _towerHead = towerHeadRef;
        _useLaser = useLaser;
        _lineRenderer = lr;
        _lazerDmg = lazerDmg;
        _targetEnemyWithMoreHealth = targetMoreHealth;
    }

    private void Update()
    {
        if (_target == null)
        {
            if (_useLaser)
                if (_lineRenderer.enabled)
                    _lineRenderer.enabled = false;
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (_targetEnemyWithMoreHealth)
        //{
        //    float enemyDistance = Mathf.Infinity;
        //    float _maxEnemyCurrentLife = 0;
        //    foreach (GameObject enemy in enemies)
        //    {
        //        Enemies e = enemy.GetComponent<Enemies>();
        //        float dist = Vector3.Distance(transform.position, enemy.transform.position);
        //        if (_maxEnemyCurrentLife == 0) _maxEnemyCurrentLife = e.MaxLife;
        //        if (e.MaxLife > _maxEnemyCurrentLife)
        //        {
        //            possibleTarget = enemy;
        //            enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
        //        }
        //    }
        //    if (possibleTarget != null && enemyDistance <= _shootingRange) _target = possibleTarget.transform;
        //    else _target = null;
        //}
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_targetTag);
        GameObject possibleTarget = null;

        float shortestEnemyDistance = Mathf.Infinity;
        float _maxEnemyCurrentLife = 0; //Usar linq para buscar el que  más vida tiene tal vez order by, where?
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestEnemyDistance)
            {
                shortestEnemyDistance = dist;
                possibleTarget = enemy;
            }

            if (_targetEnemyWithMoreHealth)
            {
                Enemies e = enemy.GetComponent<Enemies>();
                if (_maxEnemyCurrentLife == 0) _maxEnemyCurrentLife = e.MaxLife;

                if (e.MaxLife > _maxEnemyCurrentLife && _maxEnemyCurrentLife != 0)
                {
                    shortestEnemyDistance = dist;
                    possibleTarget = enemy;
                }
            }
        }

        if (possibleTarget != null && shortestEnemyDistance <= _shootingRange) _target = possibleTarget.transform;
        else _target = null;

        if (_target != null)
        {
            _towerHead.transform.LookAt(_target.transform);

            if (_useLaser) Laser();
            else Shoot();
        }
    }

    void Laser()
    {
        _target.gameObject.GetComponent<IDamageable>().TakeDamage(_lazerDmg * Time.deltaTime * 2);

        if (!_lineRenderer.enabled)
            _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _bulletShootingPointTransform[0].position);
        _lineRenderer.SetPosition(1, _target.position);
    }

    void Shoot()
    {
        if (Time.time > timeToNextBullet)
        {
            for (int i = 0; i < _bulletShootingPointGO.Length; i++)
            {
                var bullet = Instantiate(_bulletPrefab, _bulletShootingPointTransform[i].position, _bulletShootingPointTransform[i].rotation);
                AudioManager.instance.TowerShootAudio();
                BulletController bulletReference = bullet.GetComponent<BulletController>();
                if (bullet != null) bulletReference.SearchTarget(_target);
                timeToNextBullet = Time.time + _shootingRate;
            }
        }
    }
}