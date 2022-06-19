using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public static int width = 10; //游戏场景的宽度
    public static int height = 20;//游戏场景的高度
    //用于存放方块的二维数组
    public static Transform[,] grid = new Transform[width, height];

    public static int score = 0;//分数

    ///<summary>
    ///对坐标进行取整
    /// </summary>
     public static Vector2 RoundVec2(Vector2 v)
    {
        //Mathf.Round:Returns f rounded to the nearest interger
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    ///<summary>
    ///方块组是否在边界内
    /// </summary>
    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    ///<summary>
    ///判断一行是否被填满
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
    ///删除行
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
    ///删除行后将上面一行下降
    /// </summary>
    public static void DecreaseRow(int y)
    {
        for(int x=0;x<width;++x)
        {
            if(grid[x,y]!=null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);//坐标下移
            }
        }
    }

    ///<summary>
    ///将上面所有行下降
    /// </summary>
    public static void DecreaseRowAbove(int y)
    {
        for( int i =y;i<height;i++)
        {
            DecreaseRow(i);
        }
    }

    ///<summary>
    ///删除所有填满行
    ///1、先判断一行是否被填满，若填满就删除
    ///2、删除上面所有填满的行
    ///3、分数增加
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
    ///设置分数
    /// </summary>
    public static void SetScore(int s)
    {
        GameObject.Find("Score").GetComponent<Text>().text = "" + s;
    }
}
