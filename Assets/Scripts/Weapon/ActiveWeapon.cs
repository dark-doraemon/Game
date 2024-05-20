using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    

    private PlayerControls playerControls;
    private float timeBetweenAttacks;


    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();//tạo instance của class này(ActiveWeapon)

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }


    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        AttackCountDown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCountdown;//lấy CD của vũ khí đó
    }

    public void AttackCountDown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttackRoutine());
    }

    private IEnumerator TimeBetweenAttackRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        isAttacking = false;
    }    

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    public void Attack()
    {
        //kiểm tra player có vũ khí không
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            //nếu mà nhấn chuột phải và isAttacking == false (đang không đánh, do mỗi lần đánh là có thời gian countdown là isAttacking)
            if (attackButtonDown &&  isAttacking == false)
            {
                (CurrentActiveWeapon as IWeapon).Attack();
                AttackCountDown();//hàm countdown, countdown xong mới đánh tiếp
            }
        }
    }
}
