using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：
//功能说明：
//***************************************** 
public abstract class Controller
{
    //执行命令的的抽象方法，需要子类实现具体逻辑
    public abstract void Execute(object data);


    //获取指定类型的Model实例，并进行类型转换
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    //获取指定类型的View实例，并进行类型转换
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }


    //注册View
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    //注册Model
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    //注册Controller
    protected void RegisterController(string eventName , Type controllerType)
    {
        MVC.RegisterController(eventName,controllerType);
    }


}
