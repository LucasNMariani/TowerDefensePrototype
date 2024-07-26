using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower Variables")]
    [SerializeField] protected string _enemyTag;
    [SerializeField] protected float _towerDamage;
    [SerializeField] protected float _rangeAttack;
    [SerializeField] protected float _shootingRate;
    [SerializeField] protected int _moneyForRemove;

    [Header("Tower References")]
    [SerializeField] protected GameObject _towerHead;
    [SerializeField] protected Transform[] _bulletShootingPointReference;
    [SerializeField] protected GameObject[] _shootingPoints;
    [SerializeField] protected GameObject _bulletPrefab;
    protected RangeDetector _rangeDetectorReference;

    [Header("Use lazer")]
    [SerializeField] private bool _useLaser = false;
    [SerializeField] private bool _targetEnemyWithMoreHealth = false;
    [SerializeField] private LineRenderer _lineRenderer;

    protected void Start()
    {
        _rangeDetectorReference = gameObject.GetComponentInChildren<RangeDetector>();
        _rangeDetectorReference.SetVariables(_rangeAttack, _enemyTag,_shootingPoints,_bulletShootingPointReference,_shootingRate,_bulletPrefab, _towerHead, _useLaser, _lineRenderer, _towerDamage, _targetEnemyWithMoreHealth);
        if (!_useLaser) _bulletPrefab.GetComponent<BulletController>().SetBulletDamage(_towerDamage);
    }
    protected void OnMouseDown()
    {
        if (GameManager.instance.CanRemoveTurret == true)
        {
            GameManager.instance.CanRemoveTurret = false;
            DestroyTorret();
        }
    }

    public void DestroyTorret()
    {
        Shop.instance.AddMoney(_moneyForRemove);
        Destroy(gameObject);
    }
}