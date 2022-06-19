using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Gemstone gemstone;
    public int rowNum = 7;
    public int columNum = 10;
    public ArrayList gemstoneList;//定义列表
    private ArrayList matchesGemstone;

    private Gemstone currentGemStone;
    /// <summary>
    /// 鼠标点选判定
    /// </summary>
    /// <param name="g"></param>
    public void Select(Gemstone g)
    {
        if(currentGemStone == null)
        {
            currentGemStone = g;
            currentGemStone.isSelected = true;
            return;
        }
        else
        {
            //判断是否选择相邻宝石
            if (Mathf.Abs(currentGemStone.rowIndex - g.rowIndex) +
                Mathf.Abs(currentGemStone.columIndex - g.columIndex) == 1)
                //由于之后要消除宝石，所以先将交换函数的调用写在协程中，方便之后的消除及延迟调用
                StartCoroutine(ExchangeAndMatches(currentGemStone, g));
            currentGemStone.isSelected = false;
            currentGemStone = null;
        }
    }

    /// <summary>
    /// 实现宝石交换并且检测匹配消除
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <returns></returns>
    IEnumerator ExchangeAndMatches(Gemstone g1,Gemstone g2)
    {
        Exchange(g1, g2);
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches() || CheckVerticalMatches())
            RemoveMatches();
        else
            Exchange(g1, g2);//因为这里还不能消除，所以0.5s后再次交换宝石以进行测试
    }

    /// <summary>
    /// 生成所对应行号和列号的宝石
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="columIndex"></param>
    /// <param name="g"></param>
    public void SetGemstone(int rowIndex,int columIndex,Gemstone g)
    {
        ArrayList temp = gemstoneList[rowIndex] as ArrayList;
        temp[columIndex] = g;
    }

    /// <summary>
    /// 交换宝石数据
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    public void Exchange(Gemstone g1,Gemstone g2)
    {
        SetGemstone(g1.rowIndex, g1.columIndex, g2);
        SetGemstone(g2.rowIndex, g2.columIndex, g1);

        //交换g1,g2的行号
        int tempRowIndex;
        tempRowIndex = g1.rowIndex;
        g1.rowIndex = g2.rowIndex;
        g2.rowIndex = tempRowIndex;

        //交换g1,g2的列号
        int tempColumIndex;
        tempColumIndex = g1.columIndex;
        g1.columIndex = g2.columIndex;
        g2.columIndex = tempColumIndex;

        g1.TweenToPostion(g1.rowIndex, g1.columIndex);
        g2.TweenToPostion(g2.rowIndex, g2.columIndex);

    }

    public Gemstone AddGemstone(int rowIndex, int columIndex)
    {
        Gemstone g = Instantiate(gemstone) as Gemstone;
        g.transform.parent = this.transform;//生成宝石为GameController子物体
        g.GetComponent<Gemstone>().RandomCreateGemstoneBg();
        g.GetComponent<Gemstone>().UpdatePosition(rowIndex, columIndex);//传递宝石位置 
        return g;
    }

    /// <summary>
    /// 通过行号和列号，获取对应位置的宝石
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="columIndex"></param>
    /// <returns></returns>
    public Gemstone GetGemstone(int rowIndex, int columIndex)
    {
        ArrayList temp = gemstoneList[rowIndex] as ArrayList;
        Gemstone g = temp[columIndex] as Gemstone;
        return g;
    }

    /// <summary>
    /// 实现检测水平方向的匹配
    /// </summary>
    /// <returns></returns>
    bool CheckHorizontalMatches()
    {
        bool isMathces = false;
        for(int rowIndex = 0;rowIndex<rowNum;rowIndex++)
        {
            for(int columIndex = 0;columIndex<columNum - 2;columIndex++)
            {
                if((GetGemstone(rowIndex,columIndex).gemstoneType==
                    GetGemstone(rowIndex,columIndex+1).gemstoneType&&
                    GetGemstone(rowIndex, columIndex).gemstoneType==
                     GetGemstone(rowIndex, columIndex + 2).gemstoneType))
                {
                    //Debug.Log("发现行相同宝石");
                    AddMatches(GetGemstone(rowIndex, columIndex));
                    AddMatches(GetGemstone(rowIndex, columIndex+1));
                    AddMatches(GetGemstone(rowIndex, columIndex+2));
                    isMathces = true;

                }
            }
        }
        return isMathces;
    }

    /// <summary>
    /// 实现检测垂直方向的匹配
    /// </summary>
    /// <returns></returns>
    bool CheckVerticalMatches()
    {
        bool isMathces = false;
        for (int columIndex = 0; columIndex < columNum; columIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex++)
            {
                if ((GetGemstone(rowIndex, columIndex).gemstoneType ==
                    GetGemstone(rowIndex + 1, columIndex ).gemstoneType &&
                    GetGemstone(rowIndex, columIndex).gemstoneType ==
                     GetGemstone(rowIndex + 2, columIndex ).gemstoneType))
                {
                    //Debug.Log("发现列相同宝石");
                    AddMatches(GetGemstone(rowIndex, columIndex));
                    AddMatches(GetGemstone(rowIndex + 1, columIndex ));
                    AddMatches(GetGemstone(rowIndex + 2, columIndex ));
                    isMathces = true;

                }
            }
        }
        return isMathces;
    }

    /// <summary>
    /// 储存符合消除条件的数组
    /// </summary>
    /// <param name="g"></param>
    void AddMatches(Gemstone g)
    {
         if(matchesGemstone==null)
            matchesGemstone = new ArrayList();
        int index = matchesGemstone.IndexOf(g);//检测宝石是否已在数组当中
        if (index == -1)
            matchesGemstone.Add(g);
    }

    /// <summary>
    /// 删除/生成宝石
    /// </summary>
    /// <param name="g"></param>
    void RemoveGemstone(Gemstone g)
    {
        //Debug.Log("删除宝石");
        g.Dispose();
        //删除宝石生成新的宝石
        for(int i = g.rowIndex+1;i<rowNum;i++)
        {
            Gemstone temGemstone = GetGemstone(i, g.columIndex);
            temGemstone.rowIndex--;
            SetGemstone(temGemstone.rowIndex, temGemstone.columIndex, temGemstone);

            temGemstone.TweenToPostion(temGemstone.rowIndex, temGemstone.columIndex);
        }

        Gemstone newGemstone = AddGemstone(rowNum, g.columIndex);
        newGemstone.rowIndex--;
        SetGemstone(newGemstone.rowIndex, newGemstone.columIndex, newGemstone);

        newGemstone.TweenToPostion(newGemstone.rowIndex, newGemstone.columIndex);
    }

    /// <summary>
    /// 删除匹配的宝石
    /// </summary>
    void RemoveMatches()
    {
        for(int i =0;i<matchesGemstone.Count;i++)
        {
            Gemstone g = matchesGemstone[i] as Gemstone;
            RemoveGemstone(g);
        }
        matchesGemstone = new ArrayList();
        StartCoroutine(WaitForCheckMatchesAgain());
    }

    /// <summary>
    /// 连续检测匹配消除
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForCheckMatchesAgain()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches() || CheckVerticalMatches())
        {
            RemoveMatches();
            GameObject.Find("Text").GetComponent<Text>().text = "连击";
            yield return new WaitForSeconds(2f);
            GameObject.Find("Text").GetComponent<Text>().text = "";
        }
    }

    void Start()
    {
        gemstoneList = new ArrayList();//新建列表
        matchesGemstone = new ArrayList();
        for(int rowIndex = 0;rowIndex<rowNum;rowIndex++)
        {
            ArrayList temp = new ArrayList();
            for(int columIndex = 0;columIndex<columNum;columIndex++)
            {
                Gemstone g = AddGemstone(rowIndex, columIndex);
                temp.Add(g);
            }
            gemstoneList.Add(temp);
        }

        //为防止首次刷新地图存在消除
        if (CheckHorizontalMatches() || CheckVerticalMatches())
            RemoveMatches();
    }


    
    
}
