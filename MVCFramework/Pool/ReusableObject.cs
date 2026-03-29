using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：
//功能说明：抽象对象池类,继承对象池接口
//***************************************** 
public abstract class ReusableObject : MonoBehaviour ,IReusable
{
    public abstract void OnSpawn();//取出

    public abstract void OnUnSpawn();//回收
    

}
