using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//class này chủ yếu là tạo ra các vị trí ngẫu nhiên đễ slime duy chuyến tới
//sao đó EnemyPathFinding sẽ lấy các vị trí này để duy chuyển
public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathFinding enemyPathFinding;


    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }


    private void Start()
    {
        //Coroutine là cơ chế thực thi các hàm bất đồng bộ mà không cần tạo ra các luồng mới.
        StartCoroutine(RoamingRoutine());
    }


    private Vector2 GetRoamingPosition()
    {
        //return new Vector2(Random.Range(0, 0), Random.Range(0,0)).normalized;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)).normalized;
    }

    private IEnumerator RoamingRoutine()
    {
        while(state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition(); // lấy vị trí roaming ngẫu nhiên bằng hàm GetRoamingPosition()
            enemyPathFinding.MoveTo(roamPosition);//di chuyển tới vị trí đó
            yield return new WaitForSeconds(2f);//chờ 2 giây trc khi tiếp tục vòng lặp
        }
    }
}
