using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryCheck : MonoBehaviour
{
    [SerializeField] private GameObject cup; // Đối tượng cúp
    [SerializeField] private GameObject congratulationText; // Đối tượng dòng chữ chúc mừng

    private void Start()
    {
        // Đảm bảo cúp và dòng chữ chúc mừng ẩn khi bắt đầu
        cup.SetActive(false);
        congratulationText.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra xem tất cả quái vật đã bị giết chưa
        if (AllEnemiesDefeated())
        {
            ShowVictory();
        }
    }

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

    private void ShowVictory()
    {
        // Hiển thị cúp và dòng chữ chúc mừng
        cup.SetActive(true);
        congratulationText.SetActive(true);

        // Ngăn không cho kiểm tra lại sau khi đã hiển thị chiến thắng
        this.enabled = false;
    }
}
