using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public static int width = 10; //��Ϸ�����Ŀ��
    public static int height = 20;//��Ϸ�����ĸ߶�
    //���ڴ�ŷ���Ķ�ά����
    public static Transform[,] grid = new Transform[width, height];

    public static int score = 0;//����

    ///<summary>
    ///���������ȡ��
    /// </summary>
     public static Vector2 RoundVec2(Vector2 v)
    {
        //Mathf.Round:Returns f rounded to the nearest interger
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    ///<summary>
    ///�������Ƿ��ڱ߽���
    /// </summary>
    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    ///<summary>
    ///�ж�һ���Ƿ�����
    /// </summary>
    public static bool IsRowFull(int y)
    {
        for(int x=0;x<width;++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    ///<summary>
    ///ɾ����
    /// </summary>
    public static void DeleteRow(int y)
    {
        for(int x=0;x<width;++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    ///<summary>
    ///ɾ���к�����һ���½�
    /// </summary>
    public static void DecreaseRow(int y)
    {
        for(int x=0;x<width;++x)
        {
            if(grid[x,y]!=null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);//��������
            }
        }
    }

    ///<summary>
    ///�������������½�
    /// </summary>
    public static void DecreaseRowAbove(int y)
    {
        for( int i =y;i<height;i++)
        {
            DecreaseRow(i);
        }
    }

    ///<summary>
    ///ɾ������������
    ///1�����ж�һ���Ƿ���������������ɾ��
    ///2��ɾ������������������
    ///3����������
    /// </summary>
    public static void DeleteFullRows()
    {
        for(int y=0;y<height;++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                score += 10;
                SetScore(score);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }

    ///<summary>
    ///���÷���
    /// </summary>
    public static void SetScore(int s)
    {
        GameObject.Find("Score").GetComponent<Text>().text = "" + s;
    }
}
