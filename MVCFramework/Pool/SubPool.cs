
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：
//功能说明：子对象池类
//***************************************** 
public class SubPool 
{
    //游戏物体列表(集合) 存储一组GameObject对象
    [HideInInspector]
    List<GameObject> m_objects = new List<GameObject>();
    //预制体
    GameObject m_prefab;

    //预制体名字属性，只读
    public string Name 
    {
        get { return m_prefab.name; }
    }

    //父物体位置
    Transform m_parent;

    //构造函数
    public SubPool( Transform parent , GameObject prefab)
    {
        m_parent = parent;
        m_prefab = prefab;
    }

    //取出(生成对象的方法)
    public GameObject Spawn()
    {
        GameObject go = null;
        //遍历集合，找到未被激活的物体
        foreach (var obj in m_objects)
        {
            if ( ! obj.activeSelf )
            {
                go = obj;
                break;
            }
        }

        //集合里没有多余的,实例化一个新的对象并且添加到集合中
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            //添加到父物体上
            go.transform.parent = m_parent;
            //添加到集合上
            m_objects.Add(go);
        }

        //设置属性，调用接口生成方法  //发送消息，执行OnSpawn
        go.SetActive(true);
        go.SendMessage("OnSpawn" , SendMessageOptions.DontRequireReceiver);

        return go;
    }

    //回收单个物体
    public void UnSpawn(GameObject go)
    {
        //检查集合中是否包含该对象，如果是，则将其设置为非激活状态，并调用其 OnUnSpawn 方法（如果存在）
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收全部物体
    public void UnSpawnAll()
    {
        //遍历所有集合中的对象，将激活状态的对象都回收
        foreach (var obj in m_objects)
        {
            if (obj.activeSelf)
            {
                //回收已经被激活的
                UnSpawn(obj);
            }
        }
    }

    //检查集合中是否包含该对象
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
