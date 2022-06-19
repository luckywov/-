using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生成装快并让其有序排列思路如下
//1、外置五个编译好的txt文档，里面保存了地图数据，其中x表示空，b表示蓝色，r表示红色，g表示绿色，y表示黄色
//2、使用随机函数，生成（1，5）中随机数表示载入的地图
//3、使用Block中定义的block类，在txt文档长度内遍历，每一个字符进行一次计算
//1)若是x，则x坐标增加一个空位距离
//2)若是r、b、g、y，则生成block，并在block类中存入相应颜色以及生命值
//4、遍历完后结束
public class Block : MonoBehaviour
{
    public string color;
    public int hits;
}
