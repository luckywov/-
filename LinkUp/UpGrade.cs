using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade : MonoBehaviour
{
    
    public Sprite[] upgradeSprites;
    public string upgradeName = "";

    private void Awake()
    {
        //�����ȡ��ͼ
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];
        upgradeName = icon.ToString();//�������ƵĴ洢
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;//��ͼ
    }
    private void Update()
    {
        Destroy(this.gameObject, 0.8f);//���ٵ���
    }
}
