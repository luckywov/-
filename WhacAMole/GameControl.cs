using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    //地鼠对象
    public GameObject Gophers;

    //用于记录地鼠的x，y坐标
    public int PosX, PosY;

    public TextMesh timeLable;
    public float time = 30.0f;
    public int score = 0;

    /// <summary>
    /// 设定一个地洞类，存储地洞的坐标以及是否出现的布尔值
    /// </summary>
    public class Hole
    {
        public bool isAppear;
        public int HoleX;
        public int HoleY;
    }

    public Hole[] holes;

    /// <summary>
    /// Awake函数实际上比Start函数调用得更早
    /// 在场景初始化的时候，将每个洞口的坐标值存入一维数组中，并将每个洞口的isAppear设为false
    /// (-2, 0)(0, 0)(2, 0)
    /// (-2.-1)(0,-1)(2,-1)
    /// (-2,-2)(0,-2)(2,-2)
    /// </summary>

    private void Awake()
    {
        PosX = -2;
        PosY = -2;
        holes = new Hole[9];
        for(int i = 0;i<3;++i)
        {
            for(int j = 0;j<3;++j)
            {

                holes[i * 3 + j] = new Hole
                {
                    HoleX = PosX,
                    HoleY = PosY,
                    isAppear = false
                };
                PosY++;
            }
            PosY = -2;
            PosX += 2;
        }
    }
    //Use this for initialization
    void Start()
    {
        ////在(0,0+0.4f)上生成地鼠，0.4f为地鼠的高度
        //Instantiate(Gophers, new Vector3(0, 0 + 0.4f, -0.1f), Quaternion.identity);
        ////Quaternion.identity == Quaternion(0,0,0,0);

        ////从第0秒开始调用，每秒调用一次
        //InvokeRepeating("Appear", 0, 1);

        //在游戏场景开始后延时调用CanAppear函数，从第0秒开始，每隔10秒调用一次
        InvokeRepeating("CanAppear", 0, 10);
    }
    ///<summary>
    ///从第0秒开始调用函数，每隔1秒调用一次
    /// </summary>
    public void CanAppear()
    {
        InvokeRepeating("Appear", 0, 1);
    }

   ///<summary>
   ///地鼠生成函数
   /// </summary>
   public void Appear()
    {
        //当前地洞可以生成地鼠的条件：isAppear==false
        //随机生成i值选择洞口
        int i = Random.Range(0, 9);
        while(holes[i].isAppear == true)
        {
            i = Random.Range(0, 9);
        }
        //debug只是用来打印当前坐标，便于观察，并不会影响游戏运行（懂）
        Debug.Log(holes[i].HoleX + "," + holes[i].HoleY);

        //选定洞口以后，在洞口的坐标上生成地鼠,传递洞口id，将当前洞口的isAppear改为true
        Instantiate(Gophers, new Vector3(holes[i].HoleX, holes[i].HoleY + 0.4f, -0.1f), Quaternion.identity);
        Gophers.GetComponent<Gophers>().id = i;
        holes[i].isAppear = true;
    }

    private void Update()
    {
        //时间以秒的速度减少，并在timeLabel里显示当前剩余时间（一位小数）
        time -= Time.deltaTime;
        timeLable.text = "Time: " + time.ToString("F1");

        //当时间耗尽，调用GameOverhanshu
        if (time < 0)
        {
            GameOver();
        }

        ///<summary>
        /// 游戏结束函数
        /// </summary>
        void GameOver()
        {
            time = 0;
            timeLable.text = "Time:0";

            //将所有延时调用函数全部取消
            CancelInvoke();
        }
    }
}
