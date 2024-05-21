using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCountdown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;
    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathFinding enemyPathFinding;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
        //StartCoroutine(RoamingRoutine());
    }

    private void Update()
    {
        MovementStateControl();
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        //return new Vector2(Random.Range(0, 0), Random.Range(0, 0)).normalized;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)).normalized;
    }

    private void MovementStateControl()
    {
        switch(state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;

        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathFinding.MoveTo(roamPosition);

        //khi đang roaming
        //nếu mà player trong khoảng cách đánh của enemy thì chuyển trạng thái sang đánh
        if(Vector2.Distance(transform.position,PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        //nếu không thì lấy ví trí khác
        if(timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    //hàm đánh player
    private void Attacking()
    {

        //check lại xem có player có trong tầm dánh không
        if(Vector2.Distance(transform.position,PlayerController.Instance.transform.position) > attackRange)
        {
            //nếu không thì đi roam tiếp
            state = State.Roaming;
        }


        //đánh player
        if(attackRange != 0 && canAttack)
        {
            //khi đánh xong thì chuyển trạng thái không cho đánh nữa
            canAttack = false;

            (enemyType as IEnemy)?.Attack();

            //khi đánh phải dừng di chuyển
            if (stopMovingWhileAttacking)
            {
                enemyPathFinding.StopMoving();
            }
            else
            {
                enemyPathFinding.MoveTo(roamPosition);
            }

            //khi đánh xong thì đợi CD
            StartCoroutine(AttackCountdownRountine());
        }
    }

    private IEnumerator AttackCountdownRountine()
    {
        yield return new WaitForSeconds(attackCountdown);

        canAttack = true;
    }


}
