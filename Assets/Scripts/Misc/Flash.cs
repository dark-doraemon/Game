using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class này là hiệu ứng quái bị đánh(player Xài cũng được)
public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;//Hiệu ứng quái bị đánh có thể tự chọn ở inspector
    [SerializeField] private float restoreDefaultMatTime = .2f;//thời gian hết hiệu ứng

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();

        //lưu material mặc định
        defaultMaterial = spriteRenderer.material;

    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;//thay đổi material khi bị đánh
        yield return new WaitForSeconds(restoreDefaultMatTime); 

        //chờ 1 khoảng thời gian để quái trở lại bình thường (bằng material mặc định ta đã lưu lại trước đó)
        spriteRenderer.material = defaultMaterial;
        //kiểm tra quái có chết không
        enemyHealth?.DetectDeath();
    }

}
