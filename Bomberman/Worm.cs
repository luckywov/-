using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    //�����ƶ��ٶ�
    public float Speed = 2f;

    /// <summary>
    /// �������趨һ������˶�����
    /// </summary>
    /// <returns></returns>
    Vector2 randomDir()
    {
        //���������Ϊ-1��0��1��
        int r = (Random.value < 0.5) ? 1 : -1;
        //��Ŀ�����
        return (Random.value < 0.5) ? new Vector2(r, 0) : new Vector2(0, r);
    }

    /// <summary>
    /// ����˶�����
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    bool IsValidDir(Vector2 dir)
    {
        //��ȡ�����ʱ��λ��
        Vector2 pos = transform.position;

        //�ӹ��ﵱǰλ�÷���һ�����ߣ������������������޷��˶�����֮����
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

        return hit.collider.gameObject == gameObject;
    }

    /// <summary>
    /// �����˶������ö���
    /// </summary>
    void ChangeDir()
    {
        //��ȡ����Ķ�ά����
        Vector2 dir = randomDir();

        //����Ƿ����˶�
        if(IsValidDir(dir))
        {
            GetComponent<Rigidbody2D>().velocity = dir * Speed;
            GetComponent<Animator>().SetInteger("x", (int)dir.x);
            GetComponent<Animator>().SetInteger("y", (int)dir.y);
        }
    }

    /// <summary>
    /// ʵʱ��ȡ�˶�����
    /// </summary>
    private void Start()
    {
        InvokeRepeating(nameof(ChangeDir), 0, 0.5f);
    }
}
