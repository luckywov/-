using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Sprite[] MapSprites;   //��ͼ����

    public static int[] temp_map; //��ֵ����
    public int[] final_map;       //�����յ���

    public bool playerDestory = false;//ͬisChange��Ҳ�����ڿ��ƽű��е�����ƶ��������д�����
    public bool boxDestory = false;

    public GameObject[] destroyObj;

    //Ԥ����
    public GameObject mapPrefab;   //��ͼ
    public GameObject playerPrefab;//��ɫ
    public GameObject boxPrefab;   //����
    public GameObject finalBoxPrefab;   //���յ�������
    public GameObject finalPrefab; //��Ϸ��ʤ��ˢ��һ��ͼƬ��������ʾ��Ϸ��ʤ
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
        //�趨final_map��temp��ͬ�Ա��յ���
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
        destroyObj = new GameObject[9 * 9];//��������GameObject

        int i = 0;
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                switch (temp_map[i])
                {

                    case 1://����ǽ��
                    case 0://��ͼ
                        g = Instantiate(mapPrefab) as GameObject;
                        g.transform.position = new Vector3(x, -y, 0);
                        g.name = x.ToString() + y;

                        destroyObj[y * 9 + x] = g;

                        Sprite icon = MapSprites[temp_map[i]];//ʹ��ͼ���ͼ�����Ǻ�
                        g.GetComponent<SpriteRenderer>().sprite = icon;
                        break;
                    case 2://��������
                        g = Instantiate(playerPrefab) as GameObject;
                        g.transform.position = new Vector3(x, -y, 0);
                        g.name = "Player";
                        destroyObj[y * 9 + x] = g;

                        break;

                    case 3://��������
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
    /// ��ɫ�ƶ�
    /// </summary>
    void PlayerMove()
    {
        //��ȡ������ֵ
        int x, y;
        int x_num = 0;
        int y_num = 0;

        //��ȡ��ɫ����
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        x = (int)playerPos.position.x;
        y = (int)playerPos.position.y;

        //��ȡ�����ƶ�������ֵ
        x_num = FindObjectOfType<GameController>().x_num;
        y_num = FindObjectOfType<GameController>().y_num;

        //��ȡ����һ��Ҫ��������Playerǰ 
        string animator_name = FindObjectOfType<GameController>().animator_name;

        //����ԭ��Player
        Destroy(destroyObj[-y * 9 + x]);

        g = Instantiate(playerPrefab) as GameObject;

        g.GetComponent<Animator>().Play(animator_name);
        g.transform.position = new Vector3(x + x_num, y + y_num, 0);
        g.name = "Player";

        //���µ�Player����������ֵ
        destroyObj[-((y + y_num) * 9) + x + x_num] = g;
        playerDestory = false;

    }

    /// <summary>
    /// �����ƶ�
    /// </summary>
    void BoxMove()
    {
        //��ȡ������ֵ
        int x, y;
        int x_num = 0;
        int y_num = 0;

        //��ȡ�����ƶ�������ֵ��Box������
        x_num = FindObjectOfType<GameController>().x_num;
        y_num = FindObjectOfType<GameController>().y_num;

        //��ȡ��ӦBox����
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();

        //Player��X����+x_numΪ��һ��Ŀ�������꣬��Ϊ��ӦBox��X���꣬Yͬ��
        x = (int)playerPos.position.x + x_num;
        y = (int)playerPos.position.y + y_num;

        //����ԭ��Box
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

        //���µ�Box������������
        destroyObj[-((y + y_num) * 9) + x + x_num] = g;
        playerDestory = false;
        boxDestory = false;
    }

    private void Update()
    {
        if (playerDestory)//���player�ƶ�����ԭ��player��Ҫ����
        {
            if (boxDestory)//���Box�ƶ�����ԭ��Box��Ҫ����
            {
                BoxMove();
            }
            PlayerMove();
        }
        //��Ϸ�����ж�
        if(GameObject.Find("Box")==null&&GameObject.Find("Final")==null)
        {
            g = Instantiate(finalPrefab) as GameObject;
            g.transform.position = new Vector3(4, -4, 0);
            g.name = "Final";
        }
    }
}
