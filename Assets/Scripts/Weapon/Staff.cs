using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour,IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    private void Update()
    {
        MouseFollowWithOffset();
    }
    public void Attack()
    {
        Debug.Log("Staff Attack");
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        //lấy vị trí của mouse
        Vector3 mousePos = Input.mousePosition;

        //lấy vị trí của player
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);



        //tính arctan của tỷ lệ giữa tọa độ y và tọa độ x của vị trí con trỏ chuột trong không gian.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //nếu vị trí của con chuột nằm trc player thì xoay vũ khí lại
        if (mousePos.x < playerScreenPoint.x)
        {
            //quay vũ khí trục x 1 góc 0 độ, y 1 góc -180 độ, z 1 góc angle
            //tại sao quay trục z (vì vũ khí sẽ chuyển theo chuột theo hướng trục z)
            //và lưu ý đầy là quay rotation không phải position
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }

        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }

   
}
