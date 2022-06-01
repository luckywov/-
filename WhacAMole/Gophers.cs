using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地鼠类
/// </summary>
public class Gophers : MonoBehaviour
{
    //定义一个新的游戏对象beaten
    public GameObject beaten;
    public int id;

    /// <summary>
    /// 地鼠出现后，如果未被击中，则三秒后自动销毁
    /// </summary>
    void Update()
    {
        Destroy(gameObject, 3.0f);
        //将对应洞口的isAppear改为false
        FindObjectOfType<GameControl>().holes[id].isAppear = false;
    }

    /// <summary>
    /// 鼠标点击函数
    /// </summary>
    private void OnMouseDown()
    {
        //在相同的位置生成一个被击打图像的地鼠
        GameObject g;
        g = Instantiate(beaten, gameObject.transform.position, Quaternion.identity);
        //在0.1s后摧毁当前生成的地鼠
        Destroy(gameObject);
        //将当前洞口id传递给beaten
        g.GetComponent<Beaten>().id = id;

        //增加分数
        FindObjectOfType<GameControl>().score += 1;
        int scores = FindObjectOfType<GameControl>().score;
        GameObject.Find("Score").gameObject.GetComponent<TextMesh>().text = "Score:" + scores.ToString();
    }

}
