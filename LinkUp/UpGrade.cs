using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade : MonoBehaviour
{
    
    public Sprite[] upgradeSprites;
    public string upgradeName = "";

    private void Awake()
    {
        //随机获取贴图
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];
        upgradeName = icon.ToString();//道具名称的存储
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;//贴图
    }
    private void Update()
    {
        Destroy(this.gameObject, 0.8f);//销毁道具
    }
}
