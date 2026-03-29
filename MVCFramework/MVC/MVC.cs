
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.SearchService;
//*****************************************
//创建人：
//功能说明：MVC统合
//***************************************** 
public abstract class MVC 
{
    //注册字典
    //Name - Model
    public static Dictionary<string , Model> Models = new Dictionary<string , Model>();
    //Name - View
    public static Dictionary<string , View> Views = new Dictionary<string , View>();
    //EventName - Type(Controller)事件名称与控制器类型的字典
    public static Dictionary<string , Type> CommandMap = new Dictionary<string , Type>();


    //注册视图View,将view对象存储在Views字典中
    public static void RegisterView(View view)
    {
        //防止重复注册
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }

        view.RegisterAttentionEvent();
        //添加到字典里
        Views[view.Name] = view;
    }

    //注册模型Model，将model对象存储在Models字典中
    public static void RegisterModel(Model model)
    {
        //添加到字典里
        Models[model.Name] = model;
    }

    //注册控制器Controller , 将事件名称及控制器类型都存储在CommandMap字典中
    public static void RegisterController(string eventName , Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //获取指定类型的视图View对象
    public static T GetView<T>() where T : View
    {
        foreach (var v in Views.Values)
        {
            //判断视图字典中的值，是否含有指定的类型T
            if (v is T)
            {
                return (T)v;//返回符合的视图对象
            }
        }
        return null;
    }

    //获取指定类型的模型Model对象
    public static T GetModel<T>() where T : Model
    {
        foreach (var m in Models.Values)
        {
            //判断模型字典中的值，是否含有指定的类型T
            if (m is T)
            {
                return (T)m;//返回符合的模型对象
            }
        }
        return null;
    }



    public static void SendEvent(string eventName , object data = null)
    {
        //控制器执行
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];

            //通过反射实例化控制器
            Controller c = Activator.CreateInstance(t) as Controller;

            c.Execute(data);
        }

        //视图处理
        foreach (var v in Views.Values)
        {
            //如果v的事件关系列表包含了eventName事件名称
            if (v.AttentionList.Contains(eventName))
            {
                //执行事件函数
                v.HandleEvent(eventName, data);
            }
        }
    }

}
