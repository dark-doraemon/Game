using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;  

    //collision là những đối tượng khác trong phạm vi collision của game object được gắn script này
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ta chỉ quan tầm tới EnemyHealth thôi nếu là EnemyHealth thì Debug.log
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakingDamage(damageAmount);
            Debug.Log("Enemy has been destroyed");
        }
    }
}
