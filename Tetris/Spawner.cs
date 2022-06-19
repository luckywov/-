using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] Blocks;         //储存方块组的数组
    public Sprite[] sprites;            //存储方块组图片的数组
    public static bool isFirst = true;  //是否是第一次产生方块
    public static int current = 0;      //当前方块的序号
    public static int next = 0;         //下一个产生方块的序号
    void Start()
    {
        SpawnerNext();
    }

    public void SpawnerNext()
    {
        #region
        ////用产生随机数的方式随机产生方块组
        //int i = Random.Range(0, Blocks.Length);
        ////随机产生方块
        //Instantiate(Blocks[i], transform.position, Quaternion.identity);
        #endregion
        if(isFirst)
        {
            isFirst = false;
            current = Random.Range(0, Blocks.Length);
            next = Random.Range(0, Blocks.Length);
        }
        else
        {
            current = next;
            next = Random.Range(0, Blocks.Length);
        }
        //随机产生方块
        Instantiate(Blocks[current], transform.position, Quaternion.identity);
        //在界面中显示出图片
        GameObject.Find("Image").GetComponent<Image>().sprite = sprites[next];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
