using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：
//功能说明：单例抽象基类
//***************************************** 
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;//保留单例实例静态字段

    public static T Instance//单例字段的自读属性
    {
        get => m_instance;
    }

    //在 Awake 中将当前实例赋值给静态字段，确保只有一个实例
    protected virtual void Awake()
    {
        m_instance = this as T;
    }
}
