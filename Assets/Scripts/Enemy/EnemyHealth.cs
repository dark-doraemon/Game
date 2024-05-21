using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    [SerializeField] private float knockBackThrust = 15f; //lực bị đẩy lùi

    [SerializeField] private GameObject slimeDeathVFXPrefab;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
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

        //nếu quái nhận damage thì kích hoạt hiệu ứng bị đánh
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        //nếu là hết máu thì đối tượng gắn script này sẽ bị hủy
        if(currentHealth <=0)
        {
            //nếu slime chết thì tạo hiệu ứng chết tại ví trí của slime
            Instantiate(slimeDeathVFXPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);

            //khi chết rớt ra coin
            GetComponent<PickUpSpawner>().DropItems();
        }
    }
}
