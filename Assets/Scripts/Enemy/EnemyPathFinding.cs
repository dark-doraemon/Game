using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }



    //duy chuyển với vị trí target 
    public void MoveTo(Vector2 targetPosition)
    {
        //vị trí mà slime sẽ duy chuyển tới là vị trí của target
        moveDirection = targetPosition;
    }
}
