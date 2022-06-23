using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //设置运动速度
    public float speed = 16f;

    /// <summary>
    /// 实时获取键盘输入
    /// </summary>
    private void FixedUpdate()
    {
        //getAxisRaw 只能返回-1，0.1三个值
        //得到水平或垂直指令
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //访问炸弹人2D刚体，通过获取的值进行运动
        GetComponent<Rigidbody2D>().velocity = new Vector2(h, v) * speed;

        //播放对应动画
        GetComponent<Animator>().SetInteger("x", (int)h);
        GetComponent<Animator>().SetInteger("y", (int)v);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "worm(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
