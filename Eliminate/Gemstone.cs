using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : MonoBehaviour
{
    public float xOffset = -5.5f;//宝石的x轴起始位置
    public float yOffset = -2.0f;//宝石的y轴起始位置
    public int rowIndex = 0;
    public int columIndex = 0;
    public GameObject[] gemstoneBgs;//宝石数组
    public int gemstoneType;//宝石类型
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
        gemstoneType = Random.Range(0, gemstoneBgs.Length);//从宝石数组中随机选择一种宝石
        gemstoneBg = Instantiate(gemstoneBgs[gemstoneType]) as GameObject;//实例化随机宝石
        gemstoneBg.transform.parent = this.transform;
    }

    public void UpdatePosition(int _rowIndex,int _columIndex)
    {
        rowIndex = _rowIndex;
        columIndex = _columIndex;
        //控制生成宝石的位置
        this.transform.position = new Vector3(columIndex + xOffset, rowIndex + yOffset, 0);
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
        Destroy(gemstoneBg.gameObject);
        gameController = null;
    }
}
