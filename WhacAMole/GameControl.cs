using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    //�������
    public GameObject Gophers;

    //���ڼ�¼�����x��y����
    public int PosX, PosY;

    public TextMesh timeLable;
    public float time = 30.0f;
    public int score = 0;

    /// <summary>
    /// �趨һ���ض��࣬�洢�ض��������Լ��Ƿ���ֵĲ���ֵ
    /// </summary>
    public class Hole
    {
        public bool isAppear;
        public int HoleX;
        public int HoleY;
    }

    public Hole[] holes;

    /// <summary>
    /// Awake����ʵ���ϱ�Start�������õø���
    /// �ڳ�����ʼ����ʱ�򣬽�ÿ�����ڵ�����ֵ����һά�����У�����ÿ�����ڵ�isAppear��Ϊfalse
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
        ////��(0,0+0.4f)�����ɵ���0.4fΪ����ĸ߶�
        //Instantiate(Gophers, new Vector3(0, 0 + 0.4f, -0.1f), Quaternion.identity);
        ////Quaternion.identity == Quaternion(0,0,0,0);

        ////�ӵ�0�뿪ʼ���ã�ÿ�����һ��
        //InvokeRepeating("Appear", 0, 1);

        //����Ϸ������ʼ����ʱ����CanAppear�������ӵ�0�뿪ʼ��ÿ��10�����һ��
        InvokeRepeating("CanAppear", 0, 10);
    }
    ///<summary>
    ///�ӵ�0�뿪ʼ���ú�����ÿ��1�����һ��
    /// </summary>
    public void CanAppear()
    {
        InvokeRepeating("Appear", 0, 1);
    }

   ///<summary>
   ///�������ɺ���
   /// </summary>
   public void Appear()
    {
        //��ǰ�ض��������ɵ����������isAppear==false
        //�������iֵѡ�񶴿�
        int i = Random.Range(0, 9);
        while(holes[i].isAppear == true)
        {
            i = Random.Range(0, 9);
        }
        //debugֻ��������ӡ��ǰ���꣬���ڹ۲죬������Ӱ����Ϸ���У�����
        Debug.Log(holes[i].HoleX + "," + holes[i].HoleY);

        //ѡ�������Ժ��ڶ��ڵ����������ɵ���,���ݶ���id������ǰ���ڵ�isAppear��Ϊtrue
        Instantiate(Gophers, new Vector3(holes[i].HoleX, holes[i].HoleY + 0.4f, -0.1f), Quaternion.identity);
        Gophers.GetComponent<Gophers>().id = i;
        holes[i].isAppear = true;
    }

    private void Update()
    {
        //ʱ��������ٶȼ��٣�����timeLabel����ʾ��ǰʣ��ʱ�䣨һλС����
        time -= Time.deltaTime;
        timeLable.text = "Time: " + time.ToString("F1");

        //��ʱ��ľ�������GameOverhanshu
        if (time < 0)
        {
            GameOver();
        }

        ///<summary>
        /// ��Ϸ��������
        /// </summary>
        void GameOver()
        {
            time = 0;
            timeLable.text = "Time:0";

            //��������ʱ���ú���ȫ��ȡ��
            CancelInvoke();
        }
    }
}
