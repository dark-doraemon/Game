using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;


//class này dùng để kích hoạt trigger attack khi nhấn cuột trái
public class Sword : MonoBehaviour, IWeapon
{
    //private PlayerControls playerControls;
    private Animator swordAnimator;

    //private PlayerController playerController;
    //private ActiveWeapon activeWeapon;
    //private PlayerControls playerControls;

    [SerializeField] private GameObject slashAnimationPrefab;//hiệu ứng slash
    [SerializeField] private Transform slashAnimationSpawnPoint;//ví trí mà hiệu ứng slash sẽ xuất hiện
    private Transform weaponCollider;
    [SerializeField] private float swordAttackCountDown = .5f;
    [SerializeField] private WeaponInfo weaponInfo;


    private GameObject slashAnimation;
    



    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }


    private void Awake()
    {
        swordAnimator = GetComponent<Animator>();
        //playerControls = new PlayerControls();

        //activeWeapon = GetComponentInParent<ActiveWeapon>();//lấy component của gameobject cha (vì sword nằm trong active weapon)
        //playerController = GetComponentInParent<PlayerController>();
    }


    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }


    //hàm này có nhiệm vụ là count down sao mỗi đòn đánh
    private IEnumerator AttackCountDownRoutine()
    {
        yield return new WaitForSeconds(swordAttackCountDown);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
        //nó sẽ đợi 1 khoản thời gian thì isAttacking mới thành false
        // trước đó isAttacking sẽ là true mà isAttacking == true thì player không thể đánh tiếp
        //isAttacking = false;//thành false nên player mới có thể đánh tiếp
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

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    private void Update()
    {
        MouseFollowWithOffset();
        //Attack();
    }


    private void MouseFollowWithOffset()
    {
        //lấy vị trí của mouse
        Vector3 mousePos = Input.mousePosition;

        //lấy vị trí của player
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);



        //tính arctan của tỷ lệ giữa tọa độ y và tọa độ x của vị trí con trỏ chuột trong không gian.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //nếu vị trí của con chuột nằm trc player thì xoay vũ khí lại
        if (mousePos.x < playerScreenPoint.x)
        {
            //quay vũ khí trục x 1 góc 0 độ, y 1 góc -180 độ, z 1 góc angle
            //tại sao quay trục z (vì vũ khí sẽ chuyển theo chuột theo hướng trục z)
            //và lưu ý đầy là quay rotation không phải position
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }

        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    public void Attack()
    {
        swordAnimator.SetTrigger("Attack");

        //SetActive(true) được gọi để kích hoạt game object.
        weaponCollider.gameObject.SetActive(true);

        //khi mà tấn công thì khởi tạo hiệu ứng slash, tại vì trí slashAnimationSpawnPoint
        slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;

        //khởi tạo 1 thread riêng cho AttackCountDownRoutine()
        //StartCoroutine(AttackCountDownRoutine());
    }

    
}
