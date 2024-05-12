using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //nếu mà player chạm vào khu vực exit
        if(other.gameObject.GetComponent<PlayerController>() )
        {
            Debug.Log("Player đã chạm vào vùng exit");
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
