using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Gemstone gemstone;
    public int rowNum = 7;
    public int columNum = 10;
    public ArrayList gemstoneList;//�����б�
    private ArrayList matchesGemstone;

    private Gemstone currentGemStone;
    /// <summary>
    /// ����ѡ�ж�
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
            //�ж��Ƿ�ѡ�����ڱ�ʯ
            if (Mathf.Abs(currentGemStone.rowIndex - g.rowIndex) +
                Mathf.Abs(currentGemStone.columIndex - g.columIndex) == 1)
                //����֮��Ҫ������ʯ�������Ƚ����������ĵ���д��Э���У�����֮����������ӳٵ���
                StartCoroutine(ExchangeAndMatches(currentGemStone, g));
            currentGemStone.isSelected = false;
            currentGemStone = null;
        }
    }

    /// <summary>
    /// ʵ�ֱ�ʯ�������Ҽ��ƥ������
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
            Exchange(g1, g2);//��Ϊ���ﻹ��������������0.5s���ٴν�����ʯ�Խ��в���
    }

    /// <summary>
    /// ��������Ӧ�кź��кŵı�ʯ
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
    /// ������ʯ����
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    public void Exchange(Gemstone g1,Gemstone g2)
    {
        SetGemstone(g1.rowIndex, g1.columIndex, g2);
        SetGemstone(g2.rowIndex, g2.columIndex, g1);

        //����g1,g2���к�
        int tempRowIndex;
        tempRowIndex = g1.rowIndex;
        g1.rowIndex = g2.rowIndex;
        g2.rowIndex = tempRowIndex;

        //����g1,g2���к�
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
        g.transform.parent = this.transform;//���ɱ�ʯΪGameController������
        g.GetComponent<Gemstone>().RandomCreateGemstoneBg();
        g.GetComponent<Gemstone>().UpdatePosition(rowIndex, columIndex);//���ݱ�ʯλ�� 
        return g;
    }

    /// <summary>
    /// ͨ���кź��кţ���ȡ��Ӧλ�õı�ʯ
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
    /// ʵ�ּ��ˮƽ�����ƥ��
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
                    //Debug.Log("��������ͬ��ʯ");
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
    /// ʵ�ּ�ⴹֱ�����ƥ��
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
                    //Debug.Log("��������ͬ��ʯ");
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
    /// ���������������������
    /// </summary>
    /// <param name="g"></param>
    void AddMatches(Gemstone g)
    {
         if(matchesGemstone==null)
            matchesGemstone = new ArrayList();
        int index = matchesGemstone.IndexOf(g);//��ⱦʯ�Ƿ��������鵱��
        if (index == -1)
            matchesGemstone.Add(g);
    }

    /// <summary>
    /// ɾ��/���ɱ�ʯ
    /// </summary>
    /// <param name="g"></param>
    void RemoveGemstone(Gemstone g)
    {
        //Debug.Log("ɾ����ʯ");
        g.Dispose();
        //ɾ����ʯ�����µı�ʯ
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
    /// ɾ��ƥ��ı�ʯ
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
    /// �������ƥ������
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForCheckMatchesAgain()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches() || CheckVerticalMatches())
        {
            RemoveMatches();
            GameObject.Find("Text").GetComponent<Text>().text = "����";
            yield return new WaitForSeconds(2f);
            GameObject.Find("Text").GetComponent<Text>().text = "";
        }
    }

    void Start()
    {
        gemstoneList = new ArrayList();//�½��б�
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

        //Ϊ��ֹ�״�ˢ�µ�ͼ��������
        if (CheckHorizontalMatches() || CheckVerticalMatches())
            RemoveMatches();
    }


    
    
}
