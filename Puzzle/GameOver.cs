using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    public static int _trueNum;//������ȷλ�õ���Ƭ������

    public static void Judge()
    {
        //if(_trueNum==_allPicNum)
        //{
        //    Debug.Log("��Ϸ����");
        //}
        int flag = 0;
        var array = CreatePic.pic;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (array[i, j].transform.position.x == j &&
                    array[i, j].transform.position.y == i)
                {
                    //Debug.Log(array[i, j] + "z:  "+
                    //    Mathf.Round(array[i, j].transform.eulerAngles.z));
                    if (Mathf.Round(array[i, j].transform.eulerAngles.z) == 0)
                    {
                        flag++;
                        Debug.Log("flag:" + flag);
                    }
                }
            }
        }
        if (flag == 9)
        {
           Debug.Log("��Ϸ������������������������");
        }

    }
}
