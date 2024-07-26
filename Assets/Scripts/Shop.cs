using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    [SerializeField] int money = 200;
    public int GetMoney => money;
    BuildManager buildManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        buildManager = BuildManager.bmInstance;
    }

    public void PurchaseTorret(int orderInList)
    {
        Debug.Log("Doble Torret Purchased");

        if (CanBuy(ref money, orderInList))
        {
           Debug.Log("Can purchase Torret");
           buildManager.SetTurretToBuild(orderInList);
        }
        else
        {
            Debug.Log("Can't purchase");
        }
    }

    public void AddMoney(int moneyIndex)
    {
        money += moneyIndex;
    }

    public void LoseMoney(int itemPrize)
    {
        money -= itemPrize;
    }

    //public void PurchaseExplosiveTorret()
    //{
    //    Debug.Log("Doble Torret Purchased");

    //    if (CanBuy(ref gameManager.money, 1))
    //    {
    //        Debug.Log("Se pudo comprar torreta explosiva");
    //        buildManager.ExplosiveTorret();
    //    }
    //    else
    //    {
    //        Debug.Log("No se puede comprar");
    //    }
    //}

    public bool CanBuy(ref int money, int orderInList)
    {
        var itemPrize = buildManager.turretData[orderInList].prize;
        return money >= itemPrize ? true : false;
    }
}