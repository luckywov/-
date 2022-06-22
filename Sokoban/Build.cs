using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Sprite[] MapSprites;   //贴图数组

    public static int[] temp_map; //传值数组
    public int[] final_map;       //用于终点检测

    public bool playerDestory = false;//同isChange，也是用于控制脚本中的玩家移动函数运行次数的
    public bool boxDestory = false;

    public GameObject[] destroyObj;

    //预制体
    public GameObject mapPrefab;   //地图
    public GameObject playerPrefab;//角色
    public GameObject boxPrefab;   //箱子
    public GameObject finalBoxPrefab;   //到终点后的箱子
    public GameObject finalPrefab; //游戏获胜后刷新一张图片，用于提示游戏获胜
    GameObject g;



    private void Awake()
    {
        final_map = new int[9 * 9];
        temp_map = new int[]
        {
                1, 1, 1, 1, 1, 0, 0, 0, 0,
                1, 2, 0, 0, 1, 0, 0, 0, 0,
                1, 0, 3, 0, 1, 0, 1, 1, 1,
                1, 0, 3, 0, 1, 0, 1, 9, 1,
                1, 1, 1, 3, 1, 1, 1, 9, 1,
                0, 1, 1, 0, 0, 0, 0, 9, 1,
                0, 1, 0, 0, 0, 1, 0, 0, 1,
                0, 1, 0, 0, 0, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 0, 0, 0
        };
        //设定final_map与temp形同以便终点检测
        for(int i = 0;i<9;i++)
        {
            for (int j = 0; j < 9; j++)
                final_map[j * 9 + i] = temp_map[j * 9 + i];
        }
    }
    private void Start()
    {
        BuildMap();
    }
    void BuildMap()
    {
        destroyObj = new GameObject[9 * 9];//用于销毁GameObject

        int i = 0;
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                switch (temp_map[i])
                {

                    case 1://生成墙壁
                    case 0://无图
                        g = Instantiate(mapPrefab) as GameObject;
                        g.transform.position = new Vector3(x, -y, 0);
                        g.name = x.ToString() + y;

                        destroyObj[y * 9 + x] = g;

                        Sprite icon = MapSprites[temp_map[i]];//使贴图与地图数组吻合
                        g.GetComponent<SpriteRenderer>().sprite = icon;
                        break;
                    case 2://生成人物
                        g = Instantiate(playerPrefab) as GameObject;
                        g.transform.position = new Vector3(x, -y, 0);
                        g.name = "Player";
                        destroyObj[y * 9 + x] = g;

                        break;

                    case 3://生成箱子
                        g = Instantiate(boxPrefab) as GameObject;
                        g.transform.position = new Vector3(x, -y, 0);
                        g.name = "Box";
                        destroyObj[y * 9 + x] = g;

                        break;
                }
                if (i < 80)
                {
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// 角色移动
    /// </summary>
    void PlayerMove()
    {
        //获取各项数值
        int x, y;
        int x_num = 0;
        int y_num = 0;

        //获取角色坐标
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        x = (int)playerPos.position.x;
        y = (int)playerPos.position.y;

        //获取界面移动坐标数值
        x_num = FindObjectOfType<GameController>().x_num;
        y_num = FindObjectOfType<GameController>().y_num;

        //获取动画一定要放在销毁Player前 
        string animator_name = FindObjectOfType<GameController>().animator_name;

        //销毁原有Player
        Destroy(destroyObj[-y * 9 + x]);

        g = Instantiate(playerPrefab) as GameObject;

        g.GetComponent<Animator>().Play(animator_name);
        g.transform.position = new Vector3(x + x_num, y + y_num, 0);
        g.name = "Player";

        //将新的Player存入销毁数值
        destroyObj[-((y + y_num) * 9) + x + x_num] = g;
        playerDestory = false;

    }

    /// <summary>
    /// 箱子移动
    /// </summary>
    void BoxMove()
    {
        //获取各项数值
        int x, y;
        int x_num = 0;
        int y_num = 0;

        //获取界面移动坐标数值及Box界面编号
        x_num = FindObjectOfType<GameController>().x_num;
        y_num = FindObjectOfType<GameController>().y_num;

        //获取相应Box坐标
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();

        //Player的X坐标+x_num为下一个目标点的坐标，即为相应Box的X坐标，Y同理
        x = (int)playerPos.position.x + x_num;
        y = (int)playerPos.position.y + y_num;

        //销毁原有Box
        Destroy(destroyObj[-y * 9 + x]);
        if(final_map[-((y+y_num)*9)+x+x_num]==0|| final_map[-((y + y_num) * 9) + x + x_num] == 3)
        {
            g = Instantiate(boxPrefab) as GameObject;
            g.transform.position = new Vector3(x + x_num, y + y_num, 0);
            g.name = "Box";
        }
        else if (final_map[-((y + y_num) * 9) ] == 9)
        {
            g = Instantiate(boxPrefab) as GameObject;
            g.transform.position = new Vector3(x + x_num, y + y_num, 0);
            g.name = "FinalBox";
        }

        //将新的Box存入销毁数组
        destroyObj[-((y + y_num) * 9) + x + x_num] = g;
        playerDestory = false;
        boxDestory = false;
    }

    private void Update()
    {
        if (playerDestory)//如果player移动并且原有player需要销毁
        {
            if (boxDestory)//如果Box移动并且原有Box需要销毁
            {
                BoxMove();
            }
            PlayerMove();
        }
        //游戏结束判定
        if(GameObject.Find("Box")==null&&GameObject.Find("Final")==null)
        {
            g = Instantiate(finalPrefab) as GameObject;
            g.transform.position = new Vector3(4, -4, 0);
            g.name = "Final";
        }
    }
}
