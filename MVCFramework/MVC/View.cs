using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
//*****************************************
//创建人：Jay
//功能说明：MVC视图层抽象类
//***************************************** 
public abstract class View : MonoBehaviour
{
    //定义了一个公共的抽象属性（字段）Name，子类需要提供GetName的实现
    public abstract string Name { get; }

    //定义了一个公共的属性AttentionList，用于存储关注的事件列表
    [HideInInspector]
    public List<string> AttentionList = new List<string>();

    //注册对特定事件关注的方法，目前该方法为空，子类需要重写该方法以实现具体逻辑
    public abstract void RegisterAttentionEvent();


    //处理事件的抽象方法，子类需要实现具体逻辑，参数为事件名和传递的数据
    public abstract void HandleEvent(string name , object data);

    //发送消息,参数为事件名和传递的数据（可选）
    protected void SendEvent(string eventName , object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    //获取指定类型的Model实例，并进行类型转换
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }
}
