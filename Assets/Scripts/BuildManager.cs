using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildManager : MonoBehaviour
{
    private GameObject turretToBuild;
    public List<TowerStruct> turretData = new List<TowerStruct>();

    private bool canBuildTurret;
    public static BuildManager bmInstance;

    private void Awake()
    {
        bmInstance = this;
    }

    private void Start()
    {
       /* torretToBuild = torretPrefabs[0].prefab;*/
        canBuildTurret = true;
        GameManager.instance.OnCompleteLevel += DisableCanBuildTorret;
        GameManager.instance.OnDefeatLevel += DisableCanBuildTorret;
    }

    public void SetTurretToBuild(int index)
    {
        turretToBuild = turretData[index].prefab;
    }

    //public void ExplosiveTorret()
    //{
    //    torretToBuild = torretData[1].prefab;
    //}

    public GameObject GetTurretToBuild()
    {
        return canBuildTurret && turretToBuild != null ? turretToBuild : null;
    }

    public void TurretPlaced()
    {
        Shop.instance.LoseMoney(SearchTurretToBuyPrize());
        turretToBuild = null;
    }

    private int SearchTurretToBuyPrize()
    {
        foreach (var torret in turretData)
        {
            if (torret.prefab.gameObject == turretToBuild) return torret.prize;
        }
        return 0;
    }
    public void DisableCanBuildTorret()
    {
        canBuildTurret = false;
    }
}
