using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    //添加预制体用来管理背景图片
    public GameObject cardBack;
    private int _id;
    public int id
    {
        get { return _id; }
    }
    //获取被分配到的id和图案
    public void SetCard(int id,Sprite image)
    {
        _id = id;
        // GetComponentInChildren<SpriteRenderer>.sprite = image;
        //GetComponentInChildren<SpriteRenderer>().sprite = image;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    public void OnMouseDown()
    {
        //如果卡片的背面是显示状态，并且SceneController脚本中记录第二次的变量未被赋值，
        //  则可以点击
        if(cardBack.activeSelf&&
            FindObjectOfType<SceneController>().canReveal==true)
        {
            //点击以后背景图片的显示状态为false
            cardBack.SetActive(false);
            //将点击的卡片传入CardRevealed函数中，使其被复制
            FindObjectOfType<SceneController>().CardRevealed(this);
        }
      
    }
    /// <summary>
    /// 未被点击状态，显示背面
    /// </summary>
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
