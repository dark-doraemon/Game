using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }


    //di weapon theo chuột
    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //tính toán vị trí của đối tượng đến vị trí của con trỏ
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}

