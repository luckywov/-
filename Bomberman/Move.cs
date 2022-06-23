using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //�����˶��ٶ�
    public float speed = 16f;

    /// <summary>
    /// ʵʱ��ȡ��������
    /// </summary>
    private void FixedUpdate()
    {
        //getAxisRaw ֻ�ܷ���-1��0.1����ֵ
        //�õ�ˮƽ��ֱָ��
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //����ը����2D���壬ͨ����ȡ��ֵ�����˶�
        GetComponent<Rigidbody2D>().velocity = new Vector2(h, v) * speed;

        //���Ŷ�Ӧ����
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
