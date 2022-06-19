using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//小球需要一个弹性且无能量损失的材质：Bounciness = 1
public class Ball : MonoBehaviour
{
    public float BallSpeed = 8f;//小球速度
    int num = 0;
    public GameObject Racket;
    private void Start()
    {
        tag = "Ball";
    }
    void Update()
    {
        if (Input.anyKey && num == 0)//num判断小球是不是第一次离开横板
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * BallSpeed;
            ++num;
        }
        //if (transform.position.y < -8)//小球掉落后重载场景
        //    SceneManager.LoadScene("Game");
    }
    ///<summary>
    ///球与板接触位置发生反弹的公式
    /// </summary>
    float HitFactor(Vector2 ballPos,Vector2 racketPos,float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }
    ///<summary>
    ///小球的碰撞发生器
    /// </summary>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="racket"&&num==1)
        {
            float x = HitFactor(transform.position,
                col.transform.position, col.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * BallSpeed;
        }
    }

    public void AllBack()
    {
        var x = Racket.transform.position.x- transform.position.x;
        var y = Racket.transform.position.y - transform.position.y;
        GetComponent<Rigidbody2D>().velocity = (new Vector2(x, y)).normalized*BallSpeed;   
    }
        
}
