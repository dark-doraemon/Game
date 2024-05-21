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
        // Kiểm tra xem player đã chạm vào khu vực exit chưa
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Kiểm tra xem tất cả quái vật đã bị giết chưa
            if (AllEnemiesDefeated())
            {
                Debug.Log("Player đã chạm vào vùng exit và tất cả quái vật đã bị giết");

                // Gọi hàm để thiết lập tên transition
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);

                // Hiệu ứng chuyển cảnh
                UIFade.Instance.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());
            }
            else
            {
                Debug.Log("Player đã chạm vào vùng exit nhưng vẫn còn quái vật chưa bị giết");
                // Gợi ý cho người chơi là cần giết hết quái vật trước khi qua cổng
            }
        }
    }

    // Hàm kiểm tra xem tất cả quái vật đã bị giết chưa
    private bool AllEnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy) // Kiểm tra xem quái vật có còn hoạt động hay không
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
