using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    //怪物移动速度
    public float Speed = 2f;

    /// <summary>
    /// 给怪物设定一个随机运动方向
    /// </summary>
    /// <returns></returns>
    Vector2 randomDir()
    {
        //设置随机数为-1，0，1；
        int r = (Random.value < 0.5) ? 1 : -1;
        //三目运算符
        return (Random.value < 0.5) ? new Vector2(r, 0) : new Vector2(0, r);
    }

    /// <summary>
    /// 检测运动方向
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    bool IsValidDir(Vector2 dir)
    {
        //获取怪物此时的位置
        Vector2 pos = transform.position;

        //从怪物当前位置发射一条射线，如果碰到物体则怪物无法运动，反之可以
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

        return hit.collider.gameObject == gameObject;
    }

    /// <summary>
    /// 怪物运动，调用动画
    /// </summary>
    void ChangeDir()
    {
        //获取随机的二维向量
        Vector2 dir = randomDir();

        //检测是否能运动
        if(IsValidDir(dir))
        {
            GetComponent<Rigidbody2D>().velocity = dir * Speed;
            GetComponent<Animator>().SetInteger("x", (int)dir.x);
            GetComponent<Animator>().SetInteger("y", (int)dir.y);
        }
    }

    /// <summary>
    /// 实时获取运动方向
    /// </summary>
    private void Start()
    {
        InvokeRepeating(nameof(ChangeDir), 0, 0.5f);
    }
}
