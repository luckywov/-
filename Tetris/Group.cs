using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public float lastFall = 0;//方块组上一次下落的时间
    public float fallframe = 1;
    private float scoregoal = 50;

    private void Start()
    {
        if(!IsValidGridPos())
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        UpdateFrame();
        //向左移动
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);//左移一个单位
            if (IsValidGridPos()) UpdateGrid(); //位置有效，更新数组
            else transform.position += new Vector3(1, 0, 0);//否则向右移一个单位
        }
        //向右移动
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPos()) UpdateGrid(); //位置有效，更新数组
            else transform.position += new Vector3(-1, 0, 0);//否则向左移一个单位
        }
    
        //旋转
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {   
            transform.Rotate(0,0,-90);//逆时针旋转90°
            if (IsValidGridPos()) UpdateGrid(); //位置有效，更新数组
            else transform.Rotate(0, 0, 90);    //否则转回去
        }
        //直接下落
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            while(IsValidGridPos())
            transform.position += new Vector3(0, -1, 0);
            transform.position += new Vector3(0, 1, 0);
            UpdateGrid();
            Grid.DeleteFullRows();//删除所有填满的行
            FindObjectOfType<Spawner>().SpawnerNext();//生成下一个方块
            enabled = false;//当方块组到达最下方后，禁用方块的此脚本
            lastFall = Time.time;
            return;
        }
        if(Time.time-lastFall>fallframe)//如果经过了一个坠落时间差
        {
            transform.position +=new Vector3(0, -1, 0);//进行模拟下坠
            if (IsValidGridPos()) UpdateGrid();//位置合法则更新
            else
            {
                transform.position += new Vector3(0, 1, 0);//不合法则rollback
                Grid.DeleteFullRows();//删除所有填满的行
                FindObjectOfType<Spawner>().SpawnerNext();//生成下一个方块
                enabled = false;//该方块的此脚本不可用
                return;
            }
            //记录方块下落的时间
            lastFall = Time.time;
        }
       
    }

    ///<summary>
    ///更新网格状态
    /// </summary>
    void UpdateGrid()
    {
        for(int y=0;y<Grid.height;y++)
        {
            for(int x=0;x<Grid.width;x++)
            {
                if(Grid.grid[x,y]!=null)
                {
                    //检测某一方块是否为该方块的一部分
                    if(Grid.grid[x,y].parent==transform)
                    {
                        //移除旧的子方块
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }
        //更新数组中的方块 。遍历方块组的每个子方块，将每个子方块添加到grid数组中
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }

    }

    //更新坠落速度
    void UpdateFrame()
    {
        if (Grid.score < scoregoal) return;
        else
        {
            scoregoal *= 2;
            fallframe -= 0.1f;
        }
    }

    ///<summary>
    ///位置是否合理
    /// </summary>
    bool IsValidGridPos()
    {
        //遍历方块组中每一个子方块
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            //如果子方块的位置超出边界则返回false
            if (!Grid.InsideBorder(v))
                return false;
            //检测方块组要移动的位置是否存在其它方块组
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
}

    
