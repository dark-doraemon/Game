using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack {  get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //damageSource ta cần lấy vị trí nguồn sát thương
    //knockBackThrust lực đẩy lùi lên đối tượng bị knock back
    public void GetKnockedBack(Transform damageSource,float knockBackThrust)
    {
        gettingKnockedBack = true;//set true để cho biết rằng đối tượng đã bị knockback


        //tính toán vector sự khác biệt của đối tượng hiện tại (đối tượng đang áp dụng script này)
        //và vị trí nguồn gây sát thương
        //sao đó được chuẩn hóa (normalized)
        //tiếp đó nhân với lực đẩy lùi (knockBackThrust)
        //cuối cũng nhân với khối khối lượng (rb.mass)
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        //lưc được áp dụng lên rb được tính bằng vector ở trước đó 
        //với chế độ là Impulse, tức là sẽ được áp dụng ngay lập tức.
        rb.AddForce(difference,ForceMode2D.Impulse);

        //kích hoạt StartCoroutine của hàm KnockRoutine()
        //để hàm KnockRoutine() chạy trên 1 thread riêng không ảnh hưởng tới thread chính
        StartCoroutine(KnockRoutine());
    }

    //đặt lại trạng thái của đối tượng sao 1 khoảng thời gian
    private IEnumerator KnockRoutine()
    {
        //đợi 1 khoảng thời gian
        yield return new WaitForSeconds(knockBackTime);

        //sao khi đợi xong nó tiếp tục thưc hiện code phía dưới đây
        //nó chỉ chờ trong hàm này thôi (không ảnh hưởng tới main thread)
        rb.velocity = Vector2.zero;//đặt vận tốc của đối tường về 0
        gettingKnockedBack = false;//set false để cho biết rằng đối tượng đã hết bị knockback
    }
}
