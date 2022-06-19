using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade : MonoBehaviour
{
    public Sprite[] upgradeSprites;
    public string upgradeName = "";

    private void Start()
    {
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];//���ѡ��ͼƬ
        upgradeName = icon.ToString();//��ͼƬ��Ӧ�ĵ�����
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;//��ͼ
    }

    private void Update()
    {
        //����λ��ˢ��
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
        //�������λ�õ��ں�壬������
        if (gameObject.transform.position.y <= -8.0f)
            Destroy(gameObject);
    }

}
