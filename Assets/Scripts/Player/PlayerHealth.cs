using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f; // Lực bị đẩy
    [SerializeField] private float damageRecoveryTime = 1f; // Thời gian phục hồi
    [SerializeField] private int damageFromEnemy = 1;

    const string HEALTH_SLIDER_TEXT = "Health Slider";


    private Slider healthSilder;
    private int curentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        isDead = false;
        curentHealth = maxHealth;

        UpdateHealthSlider();
    }

    public void UpdateHealthSlider()
    {
        // Kiểm tra xem healthSilder đã được gán chưa
        if (healthSilder == null)
        {

            healthSilder = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            // Tìm đối tượng "Health Slider" trong scene
            GameObject healthSliderObj = GameObject.Find("Health Slider");
            if (healthSliderObj != null)
            {
                // Lấy component Slider từ đối tượng "Health Slider"
                healthSilder = healthSliderObj.GetComponent<Slider>();
            }
            else
            {
                // Nếu không tìm thấy "Health Slider", xuất thông báo lỗi và thoát khỏi hàm
                Debug.LogError("Health Slider not found!");
                return;
            }
        }
        // Cập nhật giá trị và giá trị tối đa cho Slider
        healthSilder.maxValue = maxHealth;
        healthSilder.value = curentHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(damageFromEnemy, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (curentHealth < maxHealth)
        {
            curentHealth += 1;
            UpdateHealthSlider();
        }
    }

    private void CheckIfPlayerDeath()
    {
        if (curentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);

            curentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
            Debug.Log("Player has been killed");
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        SceneManager.LoadScene(TOWN_TEXT);
    }

    public void TakeDamage(int damageAmount, Transform hitTransfrom)
    {
        if (!canTakeDamage) return;

        knockBack.GetKnockedBack(hitTransfrom, knockBackThrustAmount);

        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        this.curentHealth -= damageAmount;
        // Khi nhận damage thì sẽ có 1 khoảng thời gian bị nháy và đó chính là khoảng thời gian không bị nhận damage
        StartCoroutine(DamageRecoveryRoutine());

        UpdateHealthSlider();

        CheckIfPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);

        canTakeDamage = true;
    }
}
