using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float roamChangeDirFloat = 2f;
    private enum State
    {
        Roaming
    }

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
        StartCoroutine(RoamingRoutine());
    }

    private Vector2 GetRoamingPosition()
    {
        //return new Vector2(Random.Range(0, 0), Random.Range(0, 0)).normalized;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)).normalized;
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition(); // lấy vị trí roaming ngẫu nhiên bằng hàm GetRoamingPosition()
            enemyPathFinding.MoveTo(roamPosition);//di chuyển tới vị trí đó
            yield return new WaitForSeconds(roamChangeDirFloat);//chờ 2 giây trc khi tiếp tục vòng lặp
        }
    }
}
