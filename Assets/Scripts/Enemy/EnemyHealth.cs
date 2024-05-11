using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    [SerializeField] private float knockBackThrust = 15f; //lực bị đẩy lùi

    private int currentHealth;
    private KnockBack knockBack;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();  
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakingDamage(int damage)
    {
        currentHealth -= damage;

        //ta cần vị trí của Nguồn gây sát thương để tính toán góc bị đẫy
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        Debug.Log(currentHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        //nếu là hết màu thì đối tượng gắn script này sẽ bị hủy
        if(currentHealth <=0)
        {
            Destroy(gameObject);
        }
    }
}
