using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNexus : EntityHealth
{
    //protected override void OnTriggerEnter(Collider other)
    //{
    //    var enemies = other.GetComponent<Enemies>();
    //    if (enemies != null)
    //    {
    //        Debug.Log("Nexo dañado");
    //        TakeDamage(enemies.damageToNexus);
    //    }
    //}

    public override void Death()
    {
        GameManager.instance.Defeat();
    }
}
