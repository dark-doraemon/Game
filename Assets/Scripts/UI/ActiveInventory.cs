using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class này dùng để gắn vào game obeject ActiveInventory(this)
public class ActiveInventory : Singleton<ActiveInventory>
{ 
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        //Inventory là action Map 
        //Keyboard là action của Inventory map được set bằng giao diện của PlayerControls
        //chúng ta set 1 callack cho Keyboard.performed là ToggleActiveSlot
        //ta đã set nhấn 1,2,3,4,5 ở giao diện 
        //nếu nhấn thì Keyboard.performed sẽ gọi call back là ToggleActiveSlot 
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        //(int)ctx.ReadValue<float>() : đọc từ Keyboard.performed khi ta nhấn 1,2,3,4,5
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void EquipStartWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        //ta lấy từ index = 0 nên phải trừ 1
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        //trong Active Inventory chứa các Inventory 
        //Trong Inventory chứa Active và Item
        //ta duyệt qua các Inventory và trong Active Inventory
        //và mỗi Inventory ta GetChild(0) nghĩa là lấy active và set là false 
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        //chọn ô vũ khí đang sử dụng
        //set active true tại index của vũ khí (indexNum)
        //
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    //thay đổi vũ khí
    private void ChangeActiveWeapon()
    {
        //Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrelab.name);

        //nếu mà player đã có vũ khí rồi
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            //thì hủy vũ khí đó
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        //nếu mà slot vũ khí hiện tại không có vũ khí nào thì set vũ khí hiện tại là null
        if(transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        //lấy weapon tại ví trị slot đang chọn
        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum)
            .GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrelab;

        //khởi tạo 1 weapon mới
        //GameObject newWeapon = Instantiate(weaponToSpawn,ActiveWeapon.Instance.transform.position,Quaternion.identity);
        GameObject newWeapon = Instantiate(weaponToSpawn,ActiveWeapon.Instance.transform);

        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f,0f,0f);  

        //set transform của newWeapon = là ActiveWeapon transform
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        //tạo vũ khí mới
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

    }
}
