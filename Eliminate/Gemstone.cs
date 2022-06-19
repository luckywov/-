using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : MonoBehaviour
{
    public float xOffset = -5.5f;//��ʯ��x����ʼλ��
    public float yOffset = -2.0f;//��ʯ��y����ʼλ��
    public int rowIndex = 0;
    public int columIndex = 0;
    public GameObject[] gemstoneBgs;//��ʯ����
    public int gemstoneType;//��ʯ����
    private GameObject gemstoneBg;
    private SpriteRenderer spriteRenderer;

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        spriteRenderer = gemstoneBg.GetComponent<SpriteRenderer>();
    }

    public bool isSelected
        {
            set
            {
                 if (value) spriteRenderer.color = Color.red;
                 else spriteRenderer.color = Color.white;
            }
        }

    public void OnMouseDown()
    {
        gameController.Select(this);
    }

    public void TweenToPostion(int _rowIndex,int _colomIndex)
    {
        rowIndex = _rowIndex;
        columIndex = _colomIndex;
        iTween.MoveTo(this.gameObject, 
            iTween.Hash("x", columIndex + xOffset, 
                        "y", rowIndex + yOffset,
                        "time", 0.5f));
    }
    private void Update()
    {
        
    }

    public void RandomCreateGemstoneBg()
    {
        if (gemstoneBg != null) return;
        gemstoneType = Random.Range(0, gemstoneBgs.Length);//�ӱ�ʯ���������ѡ��һ�ֱ�ʯ
        gemstoneBg = Instantiate(gemstoneBgs[gemstoneType]) as GameObject;//ʵ���������ʯ
        gemstoneBg.transform.parent = this.transform;
    }

    public void UpdatePosition(int _rowIndex,int _columIndex)
    {
        rowIndex = _rowIndex;
        columIndex = _columIndex;
        //�������ɱ�ʯ��λ��
        this.transform.position = new Vector3(columIndex + xOffset, rowIndex + yOffset, 0);
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
        Destroy(gemstoneBg.gameObject);
        gameController = null;
    }
}
