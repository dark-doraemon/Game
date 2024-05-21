using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class này là những thứ như coin,máu,mana
public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goinCoinPrefab;


    //nếu gameobject bị phá hủy như enemy, stuff
    //thì sẽ tạo ra 1 coin
    public void DropItems()
    {
        Instantiate(goinCoinPrefab, transform.position, Quaternion.identity);
    }
}
