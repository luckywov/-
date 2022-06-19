using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    //���Ԥ��������������ͼƬ
    public GameObject cardBack;
    private int _id;
    public int id
    {
        get { return _id; }
    }
    //��ȡ�����䵽��id��ͼ��
    public void SetCard(int id,Sprite image)
    {
        _id = id;
        // GetComponentInChildren<SpriteRenderer>.sprite = image;
        //GetComponentInChildren<SpriteRenderer>().sprite = image;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    /// <summary>
    /// ������¼�
    /// </summary>
    public void OnMouseDown()
    {
        //�����Ƭ�ı�������ʾ״̬������SceneController�ű��м�¼�ڶ��εı���δ����ֵ��
        //  ����Ե��
        if(cardBack.activeSelf&&
            FindObjectOfType<SceneController>().canReveal==true)
        {
            //����Ժ󱳾�ͼƬ����ʾ״̬Ϊfalse
            cardBack.SetActive(false);
            //������Ŀ�Ƭ����CardRevealed�����У�ʹ�䱻����
            FindObjectOfType<SceneController>().CardRevealed(this);
        }
      
    }
    /// <summary>
    /// δ�����״̬����ʾ����
    /// </summary>
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
