using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        knockBack = GetComponent<KnockBack>();  

    }

    private void FixedUpdate()
    {

        if (knockBack.gettingKnockedBack == true)
        {
            return;
        }
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

        if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }



    //di chuyển với vị trí target 
    public void MoveTo(Vector2 targetPosition)
    {
        //vị trí mà slime sẽ duy chuyển tới là vị trí của target
        moveDirection = targetPosition;
    }
}
