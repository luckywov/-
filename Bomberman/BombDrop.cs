using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour
{
    //����ը��
    public GameObject BombPrefab;

    private void Update()
    {
        //���ո�� ������ը��
         if(Input.GetKeyDown(KeyCode.Space))
        {
            //��ȡը���˵�ǰ���꣬�ڴ�λ��������ը��
            Vector2 pos = transform.position;
            Instantiate(BombPrefab, pos, Quaternion.identity);
        }
    }
}
