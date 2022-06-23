using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ��ͼ������������ǽ������ɺ����٣����ߵķ��õ�
/// </summary>
public class BoardManager : MonoBehaviour
{


    /// <summary>
    /// ���ڹ����ը��ǽ������
    /// </summary>
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    //�������ֵ�ͼ�ڵ�Ԥ����
    public GameObject metalTile;
    public GameObject wallTile;

    //����Ԥ����
    public GameObject wormPrefab;

    //������������ͼ���������Լ�ǽ���������Χ
    public int columns { get; private set; }
    public int rows { get; private set; }
    public Count wallCount = new Count(30, 40);
    public Count wormCount = new Count(3, 5);   //���˵�����

    //�����ò���
    public List<Metal> metalList = new List<Metal>();           // Metal�༯��
    public List<Wall> wallList = new List<Wall>();              // Wall�༯��
    private List<Vector3> gridPositions = new List<Vector3>();  // ���õĸ��ӵļ���(���ڷ��ó�����enemy)
    private Transform boardHolder;          //ǽ��ĸ�transform

    void InitialList()
    {
        gridPositions.Clear();
        for(int x = 0;x<columns;x++)
        {
            for(int y = 0;y<rows;y++)
            {
                //Ϊ������һ������λ��
                //����������
                //����������
                //����������
                if ((x == 0 & y == rows - 1) | (x == 0 & y == rows - 2) | (x == 1 & y == rows - 1)) 
                {   
                    continue; 
                }

                //�ҳ����п��õĸ��Ӵ���gridPositions
                //��ը������Ϸ�У�ż���к�ż���еĸ����ǿ������� ��ը��ǽ������ɺ͹����Լ��˵����ߵġ�
                //����ǽ���������
                if(x%2==0||y%2==0)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }
    }

    /// <summary>
    /// ����Ϸ����Metal��ǽ(�߽�)����ǽ
    /// </summary>
    /// ��ը������Ϸ�У���������ͼ����ǽ��Χ���⣬��ͼ��x��y��Ϊ�����ĸ���Ҳ�����ɲ���ը�ٵ�ǽ��
    void  BoardSetUp()
    {
        columns = 18;
        rows = 7;

        metalList.Clear();
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = null;
                if((x%2==1&y%2==1)|x==-1|x==columns|y==-1|y==rows)
                {
                    toInstantiate = metalTile;
                }
                if(toInstantiate)
                {
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                    metalList.Add(instance.GetComponent<Metal>());
                }
            }
        }
        
    }

    /// <summary>
    /// ��girdPositions�����з���һ�����λ��
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomPosition()
    {
        if(gridPositions.Count == 0)
        {
            Debug.Log("���ø�����Ϊ0");
        }
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    /// <summary>
    /// ����minimum��maximum����ը��ǽ��
    /// </summary>
    /// wall�����ɷ�Χ��GridPositions�б���
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void LayoutWallAtRandom(int minimum,int maximum)
    {
        wallList.Clear();

        int objectCount = Random.Range(minimum, maximum + 1);
        Debug.Log(objectCount);

        for(int i = 0;i<objectCount;i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject obj = Instantiate(wallTile, randomPosition, Quaternion.identity);

            obj.transform.SetParent(boardHolder);

            wallList.Add(obj.GetComponent<Wall>());
        }
    }

    /// <summary>
    /// ����miminum��maximum������
    /// </summary>
    /// ���˿�����λ����GridPositions��
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void LayoutWormAtRandom(int minimum,int maximum)
    {
        wallList.Clear();

        int objectCount = Random.Range(minimum, maximum + 1);

        Debug.Log(objectCount);

        for(int i = 0;i<objectCount;i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject obj = Instantiate(wormPrefab, randomPosition, Quaternion.identity);
        }
    }

    private void Start()
    {
        BoardSetUp();
        InitialList();
        LayoutWallAtRandom(wallCount.minimum, wallCount.maximum);

        //��������
        LayoutWormAtRandom(wormCount.minimum, wormCount.maximum);
    }
}
