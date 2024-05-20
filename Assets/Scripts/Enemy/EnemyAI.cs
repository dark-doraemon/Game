using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
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
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition(); // Lấy vị trí roaming ngẫu nhiên
            enemyPathFinding.MoveTo(roamPosition); // Di chuyển tới vị trí đó
            FlipSprite(roamPosition); // Quay mặt dựa trên hướng di chuyển
            yield return new WaitForSeconds(2f); // Chờ 2 giây trước khi tiếp tục vòng lặp
        }
    }

    private void FlipSprite(Vector2 direction)
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Quay mặt sang trái
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Quay mặt sang phải
        }
    }
}
