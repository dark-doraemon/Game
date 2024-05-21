using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.PlasticSCM.Editor.WebApi;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {  get; private set; }   
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f; //lực bị đẩy
    [SerializeField] private float damageRecoveryTime = 1f; //thời gian phục hổi
    [SerializeField] private int damageFromEnemy = 1;

    private int curentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;


    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        isDead = false;
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

    private void CheckIfPlayerDeath()
    {
        if (curentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);

            curentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
            Debug.Log("Player has been killed");
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        SceneManager.LoadScene(TOWN_TEXT);
    }

    public void TakeDamage(int damageAmount,Transform hitTransfrom)
    {

        if(!canTakeDamage) return;

        knockBack.GetKnockedBack(hitTransfrom,knockBackThrustAmount);

        StartCoroutine (flash.FlashRoutine());  
        canTakeDamage = false;
        this.curentHealth -= damageAmount;
        //khi nhận damage thì sẽ có 1 khoảng thời gian bị nháy và đó chính là khoảng thời gian không bị nhân damage
        StartCoroutine(DamageRecoveryRoutine());

        CheckIfPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);

        canTakeDamage = true;
    }

}
