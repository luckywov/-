using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //��ը��Ԥ����
    public GameObject ExplosionPrefab;

    private void OnDestroy()
    {
        //�ڱ�ըλ�����ɻ�Ԥ����
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}
