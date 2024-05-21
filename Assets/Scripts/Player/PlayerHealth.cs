using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f; //lực bị đẩy
    [SerializeField] private float damageRecoveryTime = 1f; //thời gian phục hổi
    [SerializeField] private int damageFromEnemy = 1;

    private int curentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        curentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if(enemy)
        {
            TakeDamage(damageFromEnemy,other.transform);
        }
        
    }

    public void TakeDamage(int damageAmount,Transform hitTransfrom)
    {

        if(!canTakeDamage) return;

        knockBack.GetKnockedBack(hitTransfrom,knockBackThrustAmount);

        StartCoroutine (flash.FlashRoutine());  
        canTakeDamage = false;
        curentHealth -= damageAmount;
        //khi nhận damage thì sẽ có 1 khoảng thời gian bị nháy và đó chính là khoảng thời gian không bị nhân damage
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);

        canTakeDamage = true;
    }

}
