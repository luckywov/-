using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public float lastFall = 0;//��������һ�������ʱ��
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
        //�����ƶ�
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);//����һ����λ
            if (IsValidGridPos()) UpdateGrid(); //λ����Ч����������
            else transform.position += new Vector3(1, 0, 0);//����������һ����λ
        }
        //�����ƶ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPos()) UpdateGrid(); //λ����Ч����������
            else transform.position += new Vector3(-1, 0, 0);//����������һ����λ
        }
    
        //��ת
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {   
            transform.Rotate(0,0,-90);//��ʱ����ת90��
            if (IsValidGridPos()) UpdateGrid(); //λ����Ч����������
            else transform.Rotate(0, 0, 90);    //����ת��ȥ
        }
        //ֱ������
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            while(IsValidGridPos())
            transform.position += new Vector3(0, -1, 0);
            transform.position += new Vector3(0, 1, 0);
            UpdateGrid();
            Grid.DeleteFullRows();//ɾ��������������
            FindObjectOfType<Spawner>().SpawnerNext();//������һ������
            enabled = false;//�������鵽�����·��󣬽��÷���Ĵ˽ű�
            lastFall = Time.time;
            return;
        }
        if(Time.time-lastFall>fallframe)//���������һ��׹��ʱ���
        {
            transform.position +=new Vector3(0, -1, 0);//����ģ����׹
            if (IsValidGridPos()) UpdateGrid();//λ�úϷ������
            else
            {
                transform.position += new Vector3(0, 1, 0);//���Ϸ���rollback
                Grid.DeleteFullRows();//ɾ��������������
                FindObjectOfType<Spawner>().SpawnerNext();//������һ������
                enabled = false;//�÷���Ĵ˽ű�������
                return;
            }
            //��¼���������ʱ��
            lastFall = Time.time;
        }
       
    }

    ///<summary>
    ///��������״̬
    /// </summary>
    void UpdateGrid()
    {
        for(int y=0;y<Grid.height;y++)
        {
            for(int x=0;x<Grid.width;x++)
            {
                if(Grid.grid[x,y]!=null)
                {
                    //���ĳһ�����Ƿ�Ϊ�÷����һ����
                    if(Grid.grid[x,y].parent==transform)
                    {
                        //�Ƴ��ɵ��ӷ���
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }
        //���������еķ��� �������������ÿ���ӷ��飬��ÿ���ӷ�����ӵ�grid������
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }

    }

    //����׹���ٶ�
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
    ///λ���Ƿ����
    /// </summary>
    bool IsValidGridPos()
    {
        //������������ÿһ���ӷ���
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            //����ӷ����λ�ó����߽��򷵻�false
            if (!Grid.InsideBorder(v))
                return false;
            //��ⷽ����Ҫ�ƶ���λ���Ƿ��������������
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
}

    
