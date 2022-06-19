using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade : MonoBehaviour
{
    public Sprite[] upgradeSprites;
    public string upgradeName = "";

    private void Start()
    {
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];//随机选择图片
        upgradeName = icon.ToString();//与图片对应的道具名
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;//贴图
    }

    private void Update()
    {
        //道具位置刷新
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
        //如果道具位置低于横板，则销毁
        if (gameObject.transform.position.y <= -8.0f)
            Destroy(gameObject);
    }

}
