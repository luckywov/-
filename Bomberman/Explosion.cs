using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //������������岻�Ǿ�̬���ԣ�������
        if(!other.gameObject.isStatic)
        {
            Destroy(other.gameObject);
        }
    }
}
