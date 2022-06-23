using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰到的物体不是静态属性，则被消除
        if(!other.gameObject.isStatic)
        {
            Destroy(other.gameObject);
        }
    }
}
