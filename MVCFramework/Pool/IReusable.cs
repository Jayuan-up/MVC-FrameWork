using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：Jay
//功能说明：对象池的接口
//***************************************** 
public interface IReusable
{
    void OnSpawn();//取出

    void OnUnSpawn();//回收

}
