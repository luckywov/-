using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Direction { Up = -9,Down = 9,Left = -1,Right = 1}
    Direction dir;

    public int x_num = 0;
    public int y_num = 0;
    public int box_num;
    public string animator_name;

    //isChange是一个布尔变量，由于update每帧都刷新，我们需要一个控制isMove 函数运行次数的变量，减少程序运算量
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

        //获取Player坐标
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        x = (int)playerPos.position.x;
        y = (int)playerPos.position.y;

        //如果人物下一个运动目标点为空
        if ((Build.temp_map[-y * 9 + x + (int)dir] == 0|| Build.temp_map[-y * 9 + x + (int)dir] == 9) 
            && isChange == true)
        //temp_map[-y*9+x+dir]:下一个运动目标点的数组位置
        {
            //改变数组内人物位置
            Build.temp_map[-y * 9 + x] = 0;
            Build.temp_map[-y * 9 + x + (int)dir] = 2;

            isChange = false;
            FindObjectOfType<Build>().playerDestory = true;//可以销毁
            return true;
        }
        //如果人物下一个运动目标点为墙壁
        else if (Build.temp_map[-y * 9 + x + (int)dir] == 1 && isChange == true)
        {
            isChange = false;
            return false;
        }
        //如果人物下一个运动目标点为箱子
        else if (Build.temp_map[-y * 9 + x + (int)dir] == 3      &&
                (Build.temp_map[-y * 9 + x + (int)dir * 2] == 0 ||
                 Build.temp_map[-y * 9 + x + (int)dir * 2] == 9) &&
                 isChange)
        {
            //改变 数组内人物及箱子位置
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
