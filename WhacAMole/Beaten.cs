using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���󱻻����Ժ���øú���
/// </summary>
public class Beaten : MonoBehaviour
{
    public int id;

    /// <summary>
    /// �ڵ����0.35s���ٸõ���
    /// </summary>
    void Update()
    {
        Destroy(gameObject, 0.35f);
        FindObjectOfType<GameControl>().holes[id].isAppear = false;
    }
}
