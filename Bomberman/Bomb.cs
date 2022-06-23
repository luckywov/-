using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //爆炸火花预制体
    public GameObject ExplosionPrefab;

    private void OnDestroy()
    {
        //在爆炸位置生成火花预制体
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}
