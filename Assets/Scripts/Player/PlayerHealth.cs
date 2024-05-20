using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f; //lực bị đẩy
    [SerializeField] private float damageRecoveryTime = 1f; //thời gian phục hổi


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


        //Nếu gameobject chạm player là enemy
        if (enemy && canTakeDamage)
        {
            TakeDamage(1);// trừ máu
            knockBack.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damageAmount)
    {
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
