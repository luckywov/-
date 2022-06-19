using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//С����Ҫһ����������������ʧ�Ĳ��ʣ�Bounciness = 1
public class Ball : MonoBehaviour
{
    public float BallSpeed = 8f;//С���ٶ�
    int num = 0;
    public GameObject Racket;
    private void Start()
    {
        tag = "Ball";
    }
    void Update()
    {
        if (Input.anyKey && num == 0)//num�ж�С���ǲ��ǵ�һ���뿪���
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * BallSpeed;
            ++num;
        }
        //if (transform.position.y < -8)//С���������س���
        //    SceneManager.LoadScene("Game");
    }
    ///<summary>
    ///�����Ӵ�λ�÷��������Ĺ�ʽ
    /// </summary>
    float HitFactor(Vector2 ballPos,Vector2 racketPos,float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }
    ///<summary>
    ///С�����ײ������
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
