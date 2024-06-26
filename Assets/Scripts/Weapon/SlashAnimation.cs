﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnimation : MonoBehaviour
{

    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>(); 
    }

    private void Update()
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }

    //ta sẽ đặt 1 Animation event vào giây cuối của animation 
    //khi đó nó gọi event và event của nó chính là gọi hàm này 
    //và nhiệm vụ của hàm này là hủy object animation(vì khi chém xong thì ta phải hủy hiệu ứng chém đó)
    public void DestroySelf()
    {
        //phá hủy đối tượng mà script hiện tại đang được gắn vào
        Destroy(gameObject);
    }





}
