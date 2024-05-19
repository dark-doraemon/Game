using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//class này dùng để di chuyển camera theo player
public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        //tìm CinemachineVirtualCamera trong scene hiện tại, để khi chuyển qua scene mới nó vẫn dùng camera ở scene trc đó follow theo player
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;

        //khi player đã qua scene mới thì làm cho màn hình sáng lại
        UIFade.Instance.FadeToClear();
    }
}
