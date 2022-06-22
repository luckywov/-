using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Direction { Up = -9,Down = 9,Left = -1,Right = 1}
    Direction dir;

    public int x_num = 0;
    public int y_num = 0;
    public int box_num;
    public string animator_name;

    //isChange��һ����������������updateÿ֡��ˢ�£�������Ҫһ������isMove �������д����ı��������ٳ���������
    bool isChange;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) dir = Direction.Up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) dir = Direction.Down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) dir = Direction.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) dir = Direction.Right;

        switch(dir)
        {
            case Direction.Up:
                isChange = true;
                x_num = 0;
                y_num = 1;
                animator_name = "Up";
                break;
            case Direction.Down:
                isChange = true;
                x_num = 0;
                y_num = -1;
                animator_name = "Down";
                break;
            case Direction.Left:
                isChange = true;
                x_num = -1;
                y_num = 0;
                animator_name = "Left";
                break;
            case Direction.Right:
                isChange = true;
                x_num = 1;
                y_num = 0;
                animator_name = "Right";
                break;
        }
        IsMove();
    }

    public bool IsMove()
    {
        int x, y;

        //��ȡPlayer����
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        x = (int)playerPos.position.x;
        y = (int)playerPos.position.y;

        //���������һ���˶�Ŀ���Ϊ��
        if ((Build.temp_map[-y * 9 + x + (int)dir] == 0|| Build.temp_map[-y * 9 + x + (int)dir] == 9) 
            && isChange == true)
        //temp_map[-y*9+x+dir]:��һ���˶�Ŀ��������λ��
        {
            //�ı�����������λ��
            Build.temp_map[-y * 9 + x] = 0;
            Build.temp_map[-y * 9 + x + (int)dir] = 2;

            isChange = false;
            FindObjectOfType<Build>().playerDestory = true;//��������
            return true;
        }
        //���������һ���˶�Ŀ���Ϊǽ��
        else if (Build.temp_map[-y * 9 + x + (int)dir] == 1 && isChange == true)
        {
            isChange = false;
            return false;
        }
        //���������һ���˶�Ŀ���Ϊ����
        else if (Build.temp_map[-y * 9 + x + (int)dir] == 3      &&
                (Build.temp_map[-y * 9 + x + (int)dir * 2] == 0 ||
                 Build.temp_map[-y * 9 + x + (int)dir * 2] == 9) &&
                 isChange)
        {
            //�ı� ���������Ｐ����λ��
            Build.temp_map[-y * 9 + x] = 0;
            Build.temp_map[-y * 9 + x+(int)dir] = 2;
            Build.temp_map[-y * 9 + x+(int)dir*2] = 3;

            isChange = false;
            FindObjectOfType<Build>().playerDestory = true;
            FindObjectOfType<Build>().boxDestory = true;
            return true;
        }
        return false;
    }
}
