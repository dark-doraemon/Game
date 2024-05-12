using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


//class này dùng để kích hoạt trigger attack khi nhấn cuột trái
public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator swordAnimator;

    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    [SerializeField] private GameObject slashAnimationPrefab;//hiệu ứng slash
    [SerializeField] private Transform slashAnimationSpawnPoint;//ví trí mà hiệu ứng slash sẽ xuất hiện
    [SerializeField] private Transform weaponCollider;
    private GameObject slashAnimation;

    [SerializeField] private float swordAttackCountDown = .5f;
    private bool attackButtonDown, isAttacking = false;

    private void Awake()
    {
        swordAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();

        activeWeapon = GetComponentInParent<ActiveWeapon>();//lấy component của gameobject cha (vì sword nằm trong active weapon)
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Attack()
    {
        //nếu mà nhấn chuột phải và isAttacking == false (chưa đánh thì bắt đầu dánh)
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;

            swordAnimator.SetTrigger("Attack");

            //SetActive(true) được gọi để kích hoạt game object.
            weaponCollider.gameObject.SetActive(true);

            //khi mà tấn công thì khởi tạo hiệu ứng slash, tại vì trí slashAnimationSpawnPoint
            slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;

            //khởi tạo 1 thread riêng cho AttackCountDownRoutine()
            StartCoroutine(AttackCountDownRoutine());
        }
    }


    //hàm này có nhiệm vụ là count down sao mỗi đòn đánh
    private IEnumerator AttackCountDownRoutine()
    {
        yield return new WaitForSeconds(swordAttackCountDown);
        //nó sẽ đợi 1 khoản thời gian thì isAttacking mới thành false
        //thì trước đó isAttacking sẽ là true mà isAttacking == true thì player không thể đánh tiếp
        isAttacking = false;//thành false nên player mới có thể đánh tiếp
    }

    public void DoneAttackingAnimationEvent()
    {
        //khi tấn công xong ta sẽ tắt weaponCollider
        weaponCollider.gameObject.SetActive(false);
    }


    //dùng để lật hiệu ứng slash khi vung kiếm lên
    public void SwingUpFlipAnimEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    private void Start()
    {
        //nó sẽ lấy Attack trong GameContols class(ta đã cài đặt hàm này bằng giao diện)
        //playerControls.Combat.Attack.started += _ => Attack();
        //khởi tạo hàm StartAttacking cho started  và StopAttacking cho canceled
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }


    private void MouseFollowWithOffset()
    {
        //lấy vị trí của mouse
        Vector3 mousePos = Input.mousePosition;

        //lấy vị trí của player
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);



        //tính arctan của tỷ lệ giữa tọa độ y và tọa độ x của vị trí con trỏ chuột trong không gian.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //nếu vị trí của con chuột nằm trc player thì xoay vũ khí lại
        if (mousePos.x < playerScreenPoint.x)
        {
            //quay vũ khí trục x 1 góc 0 độ, y 1 góc -180 độ, z 1 góc angle
            //tại sao quay trục z (vì vũ khí sẽ chuyển theo chuột theo hướng trục z)
            //và lưu ý đầy là quay rotation không phải position
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }

        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }


}
