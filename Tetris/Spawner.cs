using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] Blocks;         //���淽���������
    public Sprite[] sprites;            //�洢������ͼƬ������
    public static bool isFirst = true;  //�Ƿ��ǵ�һ�β�������
    public static int current = 0;      //��ǰ��������
    public static int next = 0;         //��һ��������������
    void Start()
    {
        SpawnerNext();
    }

    public void SpawnerNext()
    {
        #region
        ////�ò���������ķ�ʽ�������������
        //int i = Random.Range(0, Blocks.Length);
        ////�����������
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
        //�����������
        Instantiate(Blocks[current], transform.position, Quaternion.identity);
        //�ڽ�������ʾ��ͼƬ
        GameObject.Find("Image").GetComponent<Image>().sprite = sprites[next];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
