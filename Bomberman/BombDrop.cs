using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour
{
    //声明炸弹
    public GameObject BombPrefab;

    private void Update()
    {
        //检测空格键 ，生成炸弹
         if(Input.GetKeyDown(KeyCode.Space))
        {
            //获取炸弹人当前坐标，在此位置上生成炸弹
            Vector2 pos = transform.position;
            Instantiate(BombPrefab, pos, Quaternion.identity);
        }
    }
}
