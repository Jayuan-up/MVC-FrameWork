using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：
//功能说明：总对象池(单例模式)
//***************************************** 
public class ObjectPool : MonoSingleton<ObjectPool>
{
    //资源目录
    public string ResourceDir = "";
    //字典-储存不同类型Subpool的字典
    public Dictionary<string , SubPool> m_pools = new Dictionary<string , SubPool>();

    //取出指定名称的物体
    public GameObject Spawn(string name , Transform parentTrans)
    {
        SubPool pool = null;
        //如果字典中不包含指定名称的子池子
        if (  !  m_pools.ContainsKey(name))
        {
            //调用注册新的子池子的方法
            RegisterNew(name , parentTrans);
        }
        //获取指定名称的子池子
        pool = m_pools[name];
        //从子池子中取出物体
        return pool.Spawn();

    }

    //注册一个新的子对象池
    public void RegisterNew(string name , Transform parentTrans)
    {
        //构建资源路径
        string path = ResourceDir + "/" + name;
        //从资源路径中加载对应的游戏对象（预制体）
        GameObject go = Resources.Load<GameObject>(path);
        //新建池子
        SubPool pool = new SubPool(parentTrans , go);
        //将新创建的子池子添加到字典中
        m_pools.Add(pool.Name, pool);
    }

    //回收指定的游戏对象（单个物体）
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        //遍历子池子
        foreach(var p in m_pools.Values)
        {
            //如果当前子池子包含这个物体
            if (p.Contain(go))
            {
                pool = p;
                break;
            }
        }

        if (pool != null)
        {
            //调用该子池子的 UnSpawn 方法，回收指定游戏物体
            pool.UnSpawn(go);
        }
    }

    //回收所有物体
    public void UnSpawnAll()
    {
        foreach(var p in m_pools.Values)
        {
            //调用所有子池子的 UnSpawnAll 方法，回收所有资源
            p.UnSpawnAll();
        }
    }

    //清除对象池字典
    public void Clear()
    {
        m_pools.Clear();
    }
}
