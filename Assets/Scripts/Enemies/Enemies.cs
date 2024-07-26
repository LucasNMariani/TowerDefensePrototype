using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : EntityHealth
{
    #region Movement
    [Header("Movement Atributes")]
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Transform targetWp;
    protected WaypointMovement wpControl;
    #endregion

    [Header("Enemies Variables")]
    public int damageToNexus;
    [SerializeField]
    protected AudioClip deathEnemySound;
    [SerializeField]
    protected Animator enemyAnimator;
    [SerializeField]
    protected int enemyBounty;


    protected override void Start()
    {
        base.Start();
        wpControl = new WaypointMovement(speed, this.transform, targetWp);
        wpControl.LookAtFirstWp();
    }

    protected virtual void Update()
    {
        wpControl.OnUpdate();
    }

    public override void TakeDamage(float dmg)
    {
        if (dmg > 0 && currentLife > 0)
        {
            currentLife -= dmg;
            if (currentLife <= 0)
            {
                currentLife = 0;
                EnemyDeath();
            }
            _healthBar.fillAmount = currentLife / maxLife;
        }
        if (currentLife <= maxLife * 50 / 100)
        {
            enemyAnimator.SetBool("InjuredBool", true);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        wpControl.NextWpOnTrigger(other);

        var nexus = other.GetComponent<EndNexus>();
        if (nexus != null)
        {
            WaveManager.instance.DeathEnemyCount();
            nexus.TakeDamage(damageToNexus);
            Debug.Log("Nexo dañado");
            Death();
        }
    }

    protected void EnemyDeath()
    {
        WaveManager.instance.DeathEnemyCount();
        Shop.instance.AddMoney(enemyBounty);
        AudioManager.instance.DeathEnemiesAudio();
        enemyAnimator.SetBool("DeathBool", true);
    }
}
