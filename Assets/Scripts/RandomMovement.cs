using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float changeDirectionInterval = 3.0f; // Thời gian để thay đổi hướng
    private Vector2 movementDirection;
    private float changeDirectionTimer;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeDirection();
    }

    void Update()
    {
        changeDirectionTimer += Time.deltaTime;

        if (changeDirectionTimer > changeDirectionInterval)
        {
            ChangeDirection();
            changeDirectionTimer = 0;
        }

        Move();
    }

    void ChangeDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
    }

    void Move()
    {
        Vector3 movement = new Vector3(movementDirection.x, movementDirection.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Update animator parameters
        if (animator != null)
        {
            animator.SetFloat("MoveX", movementDirection.x);
            animator.SetFloat("MoveY", movementDirection.y);
            animator.SetBool("IsMoving", movementDirection != Vector2.zero);
        }
    }
}

