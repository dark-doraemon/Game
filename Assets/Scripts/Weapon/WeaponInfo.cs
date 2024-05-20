using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;



//CreateAssetMenu dùng để tạo menu cho chính chúng ta
//nếu ta chuột phải vào asset để tạo cái gì đó thì ta sẽ thấy được menu "New Weapon" 
//khi click vào nó sẽ tạo 1 ScriptableObject có các thuộc tính giống class ở phía dưới
[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrelab;
    public float weaponCountdown;
}
