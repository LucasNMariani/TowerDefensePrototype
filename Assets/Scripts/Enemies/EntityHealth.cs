using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EntityHealth : MonoBehaviour, IDamageable
{
    #region Health
    [Header("Health Atributes")]
    [SerializeField]
    protected float maxLife;
    protected float currentLife;
    public float MaxLife => maxLife;
    [SerializeField] protected Image _healthBar;
    #endregion

    protected virtual void Start()
    {
        currentLife = maxLife;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (dmg > 0 && currentLife > 0)
        {
            currentLife -= dmg;
            if (currentLife <= 0)
            {
                currentLife = 0;
                Death();
            }
            _healthBar.fillAmount = currentLife / maxLife;
        }
    }

    public virtual void Death()
    {
        //Debug.Log("Murió");
        Destroy(gameObject);
    }

}
