using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地鼠被击打以后调用该函数
/// </summary>
public class Beaten : MonoBehaviour
{
    public int id;

    /// <summary>
    /// 在点击后0.35s销毁该地鼠
    /// </summary>
    void Update()
    {
        Destroy(gameObject, 0.35f);
        FindObjectOfType<GameControl>().holes[id].isAppear = false;
    }
}
