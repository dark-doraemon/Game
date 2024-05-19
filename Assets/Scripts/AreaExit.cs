using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private float waitToLoadTime = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //nếu mà player chạm vào khu vực exit
        if(other.gameObject.GetComponent<PlayerController>() )
        {
            Debug.Log("Player đã chạm vào vùng exit");
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            //SceneManager.LoadScene(sceneToLoad);

            //khi qua 1 khu vực mới thì tạo hiệu ứng chuyển cảnh tối lại
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while(waitToLoadTime >=0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);

    }
}
