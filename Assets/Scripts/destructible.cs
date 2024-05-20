using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//những game object nào có class này là có thể bị phá hủy(vd : slime)
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.GetComponent<DamgeSource>() || other.gameObject.GetComponent<Projectile>())
        //{
        //    Instantiate(destroyVFX, transform.position, Quaternion.identity);

        //    Destroy(gameObject);
        //}
    }
}
