using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] dùng để hiển thị trên inspector
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashCountDown = 1f;
    [SerializeField] private float dashTime = 0.2f;

    [SerializeField] private TrailRenderer trailRenderer;//tạo hiệu ứng vẽ đuôi theo sau một đối tượng khi nó di chuyển

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator; //Player animator
    private SpriteRenderer mySpriteRender;//render ra mấy cái animation

    public static PlayerController Instance;

    public bool FacingLeft { get; set; }
    private bool isDashing { get; set; } = false;

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    public void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed += dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    public IEnumerator EndDashRoutine()
    {
        //đợi thời gian dash 
        yield return new WaitForSeconds(dashTime);

        moveSpeed -= dashSpeed;
        trailRenderer.emitting = false;

        //đợi count down dash
        yield return new WaitForSeconds(dashCountDown);
        isDashing = false;
    }

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;

        //WorldToScreenPoint dùng để chuyển đổi vị trí tử 1 điểm trong không gian trò chơi sang màn hình
        //trong trường hợp này transform.position là vị trí của đối tượng muốn chuyển đổi 
        //transform.position vị trí của đối tượng (ở đây là Player)
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);


        //nếu mà vị trí chuột ở phía trước Player trên trục X 
        //trục X--------------mouse position------------Player--------
        //thì lật Player lại phía trục X
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            FacingLeft = true;
        }

        //ngược lại thì không
        else
        {
            mySpriteRender.flipX = false;
            FacingLeft = false;
        }
    }
}
