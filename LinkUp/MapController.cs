using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject tile;
    public static int rowNum = 14;//������
    public static int columNum = 18;//������
    public static int[,] temp_map;//��ʼ��ż�������Լ���������ҵ�����
    public static int[,] test_map;//�洢�����Һ��temp_map�Լ�����Χ����һȦ0
    public Sprite[] tiles;//��ͼ����
    public static float xMove = 0.71f;//���ڵ����Ƽ��
    public static float yMove = 0.71f;//���ڵ����Ƽ��

    private void Awake()
    {
        FindObjectOfType<DrawLine>().CreateLine();
        //����test_mapҪ��temp_map���ܼ���һȦ���ڱ߽����0���������о�+2
        test_map = new int[columNum + 2, rowNum + 2];
        temp_map = new int[columNum, rowNum];
        for(int i = 0;i<rowNum;i++)
        {
            for (int j = 0; j < columNum; j+=2)
            {
                int temp = Random.Range(0, tiles.Length);
                temp_map[j, i] = temp;//ͬʱ��������һ�����ƣ�ȷ�������ֵ�������
                temp_map[j + 1, i] = temp;
            }
        }
        ChangeMap();
        for (int i = 0; i < rowNum+2; i++)
        {
            for (int j = 0; j < columNum+2; j++)
            {
                if (i == 0 || j == 0 || i == rowNum + 1 || j == columNum + 1) 
                    test_map[j, i] = 0;
                else
                    test_map[j, i] = temp_map[j-1, i-1];
            }
        }
        BuildMap();
    }

    public void ChangeMap()//���洢ID���������
    {
        for (int i = 0; i < rowNum; i++)
        {
            for (int j = 0; j < columNum; j++)
            {
                int temp = temp_map[j, i];

                int randomRow = Random.Range(0, rowNum);
                int randomColum = Random.Range(0, columNum);

                temp_map[j, i] = temp_map[randomColum, randomRow];
                temp_map[randomColum, randomRow] = temp;
            }
        }
    }

    public void BuildMap()
    {
        int i = 0;//�����е���
        int j = 0;//�����е���
        GameObject g;
        for(int y = 0;y<rowNum+2;y++)//x,y��ʾʵ������
        {
            for(int x = 0;x<columNum+2;x++)
            {
                g = Instantiate(tile) as GameObject;
                g.transform.position = new Vector3(x * xMove, y * yMove, 0);
                Sprite icon = tiles[test_map[j, i]];
                g.GetComponent<SpriteRenderer>().sprite = icon;

                g.GetComponent<Tile>().x = x;//�洢�Ƶ�����
                g.GetComponent<Tile>().y = y;
                g.GetComponent<Tile>().value = test_map[j, i];

                if (x == 0 || y == 0 || x == columNum + 1 || y == rowNum + 1 )
                    g.GetComponentInChildren<SpriteRenderer>().enabled = false;

                j++;
            }
            i++;
            j = 0;
        }
    }
}
