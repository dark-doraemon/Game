using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class này dùng để tạo Instance của các đối tương kế thừa từ nó
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }

        //nếu game object không nằm trong gameobject cha
        if(!gameObject.transform.parent)
        {
            //các object trong hàm DontDestroyOnLoad sẽ không bị hủy khi chuyển scene
            DontDestroyOnLoad(gameObject);
        }
    }
}
