using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;//hiển ứng của mũi tên khi va chạm

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }


    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();    

        //nếu và mũi tên và chạm với enemy hoặc không thể bị phá hủy
        //thì tạo hiệu ứng va chạm của mũi tên
        if(!other.isTrigger && (enemyHealth || indestructible))
        {
            //enemyHealth?.TakingDamage(weaponInfo.weaponDamage);
            Instantiate(particleOnHitPrefabVFX,transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }


    //xác định khoảng cách bắn nếu xa quá thì hủy mũi tên
    private void DetectFireDistance()
    {
        //do vị trí hiện tại của mũi tên và vị trí ban đầu của mũi tên nếu lớn hơn tầm bắn của mũi tên thì mũi tên bị hủy
        if(Vector3.Distance(transform.position,startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject) ;
        }
    }

    //khi bắn mũi tên thì hàm này sẽ thay đổi vị trí của mũi tên
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
