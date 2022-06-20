using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public Camera gameCamera;
    public static GameObject g1, g2;
    public int x1, x2, y1, y2, value1, value2;
    public bool select = false;
    public Vector3 z1, z2;
    public int linkType;

    public void isSelect()
    {
        //鼠标位置作为射线方向
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();//生成射线
        if(Physics.Raycast(ray,out hit))
        {
            if(select == false)
            {
                g1 = hit.transform.gameObject;//第一个点选的物体为g1
                //将g1的颜色改为红色
                g1.GetComponent<SpriteRenderer>().color = Color.red;
                x1 = g1.GetComponent<Tile>().x;//获取g1在数组中的位置及贴图编号
                y1 = g1.GetComponent<Tile>().y;
                value1 = g1.GetComponent<Tile>().value;
                select = true;
            }
            else
            {
                g2 = hit.transform.gameObject;
                g1.GetComponent<SpriteRenderer>().color = Color.white;
                x2 = g2.GetComponent<Tile>().x;
                y2 = g2.GetComponent<Tile>().y;
                value2 = g2.GetComponent<Tile>().value;
                select = false;
                isSame();
            }
        }
    }
    public void isSame()
    {
        if ((value1 == value2) && (g1.transform.position != g2.transform.position))
        { 
            Debug.Log("same");
            isLink(x1, y1, x2, y2);
        }
        else
        {
            x1 = x2 = y1 = y2 = value1 = value2 = 0;
        }
    }
    IEnumerator destroy(int x1,int y1,int x2,int y2)
    {
        //生成道具
        if (Random.value < 0.10)
        {
            GameObject g;
            g = Instantiate(upgradePrefab, new Vector3(8, 7, -1), Quaternion.identity);
            string name = g.GetComponent<UpGrade>().upgradeName;
            performUpgrade(name);
        }
        FindObjectOfType<DrawLine>().DrawLinkLine(g1, g2, linkType, z1, z2);
        yield return new WaitForSeconds(0.2f);//0.2s后进行删除
        Destroy(g1);
        Destroy(g2);
        MapController.test_map[x1, y1] = 0;//刷新数组中g1的位置信息
        MapController.test_map[x2, y2] = 0;//刷新数组中g2的位置信息
        x1 = x2 = y1 = y2 = value1 = value2 = 0;
    }
    //三种连接方式
    #region 直连
    //同Y
    bool X_Link(int x1, int x2, int y2)
    {
        if (x1 > x2)
        {
            int n = x1;
            x1 = x2;
            x2 = n;
        }
        for (int i = x1 + 1; i <= x2; i++)
        {
            if (i == x2) { return true; }//相邻
            //两连接点之间不为空，则跳出循环
            if (MapController.test_map[i, y2] != 0) { break; }
        }
        return false;
    }
    //同X
    bool Y_Link(int x1, int y1, int y2)
    {
        if (y1 > y2)
        {
            int n = y1;
            y1 = y2;
            y2 = n;
        }
        for (int i = y1 + 1; i <= y2; i++)
        {
            if (i == y2) { return true; }//相邻
            //两连接点之间不为空，则跳出循环
            if (MapController.test_map[x1, i] != 0) { break; }
        }
        return false;
    }
    #endregion

    #region 一折
    bool oneCornerLink(int x1,int y1,int x2,int y2)
    {
        if(MapController.test_map[x1,y2]==0)
        {
            if (X_Link(x1, x2, y2) && Y_Link(x1, y1, y2))
            {
                z1 = new Vector3(
                     x1 * MapController.xMove,
                     y2 * MapController.yMove,
                    -1);
                return true; 
            }
        }
        if (MapController.test_map[x2, y1] == 0)
        {
            if (X_Link(x1, x2, y1) && Y_Link(x2, y1, y2))
            {
                z1 = new Vector3(
                    x2 * MapController.xMove,
                    y1 * MapController.yMove,
                   -1);
                return true;
            }
        }
        return false;
    }
    #endregion

    #region 二折
    bool twoCornerLink(int x1, int y1, int x2, int y2)
    {
        #region 上探
        for(int i = y1 -1;i>-1;i--)
        {
            if (MapController.test_map[x1, i] == 0)
            {
                if(oneCornerLink(x1,i,x2,y2))
                {
                    z2 = new Vector3(
                     x1 * MapController.xMove,
                     i * MapController.yMove,
                    -1);
                    return true;
                }
            }
            if (MapController.test_map[x1, i] != 0)
                break;
        }
        #endregion
        #region 下探
        for (int i = y1 + 1; i < MapController.rowNum+2; i++)
        {
            if (MapController.test_map[x1, i] == 0)
            {
                if (oneCornerLink(x1, i, x2, y2))
                {
                    z2 = new Vector3(
                     x1 * MapController.xMove,
                    i * MapController.yMove,
                    -1);
                    return true;
                }
            }
            if (MapController.test_map[x1, i] != 0)
                break;
        }
        #endregion
        #region 左探
        for (int i = x1 - 1; i > -1; i--)
        {
            if (MapController.test_map[i, y1] == 0)
            {
                if (oneCornerLink(i, y1, x2, y2))
                {
                    z2 = new Vector3(
                     i * MapController.xMove,
                    y1 * MapController.yMove,
                    -1);
                    return true;
                }
            }
            if (MapController.test_map[i, y1] != 0)
                break;
        }
        #endregion
        #region 右探
        for (int i = x1 + 1; i < MapController.columNum + 2; i++)
        {
            if (MapController.test_map[i, y1] == 0)
            {
                if (oneCornerLink(i, y1, x2, y2))
                {
                    z2 = new Vector3(
                     i * MapController.xMove,
                    y1 * MapController.yMove,
                    -1);
                    return true;
                }
            }
            if (MapController.test_map[i, y1] != 0)
                break;
        }
        #endregion
        return false;
    }
    #endregion

    //判断是否能连接
    bool isLink(int x1,int y1,int x2,int y2)
    {
        if (x1 == x2)
        {
            if(Y_Link(x1,y1,y2))
            {
                linkType = 0;
                StartCoroutine(destroy(x1, y1, x2, y2));
                return true;
            }
        }
        else if(y1==y2)
        {
            if (X_Link(x1, x2, y1))
            {
                linkType = 0;
                StartCoroutine(destroy(x1, y1, x2, y2));
                return true;
            }
        }
        if(oneCornerLink(x1,y1,x2,y2))
        {
            linkType = 1;
            StartCoroutine(destroy(x1, y1, x2, y2));
            return true;
        }
        if (twoCornerLink(x1, y1, x2, y2))
        {
            linkType = 2;
            StartCoroutine(destroy(x1, y1, x2, y2));
            return true;
        }
        return false;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1")&&(isStoped==true))
        {
            isSelect();
        }
    }

    bool isStoped = true;
    public GameObject upgradePrefab;

    /// <summary>
    /// 道具功能实现
    /// </summary>
    /// <param name="name"></param>
    void performUpgrade(string name)
    {
        name = name.Remove(name.Length - 21);
        switch (name)
        {
            case "plus": break;
            case "stop": IsStoped(); break;
            case "clock": break;
        }
    }
    void IsStoped()
    {
        isStoped = false;
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10.0f);
        isStoped = true;
    }
}
