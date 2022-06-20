using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer line1, line2, line3;
    public void CreateLine()
    {
        GameObject Line = new GameObject("line1");
        line1 = Line.AddComponent<LineRenderer>();
        line1.SetWidth(0.1F, 0.1F);
        line1.SetVertexCount(2);//������

        Line = new GameObject("line2");
        line2 = Line.AddComponent<LineRenderer>();
        line2.SetWidth(0.1F, 0.1F);
        line2.SetVertexCount(2);//������

        Line = new GameObject("line3");
        line3 = Line.AddComponent<LineRenderer>();
        line3.SetWidth(0.1F, 0.1F);
        line3.SetVertexCount(2);//������
    }

    public void DrawLinkLine(GameObject g1, GameObject g2, 
        int linkType, Vector3 z1, Vector3 z2)
    {

        if (linkType == 0)
        {
            line1.SetPosition(0, g1.transform.position + new Vector3(0, 0, -1));
            line1.SetPosition(1, g2.transform.position + new Vector3(0, 0, -1));
        }
        if (linkType == 1)
        {
            line1.SetPosition(0, g1.transform.position);
            line1.SetPosition(1, z1);

            line2.SetPosition(0, z1);
            line2.SetPosition(1, g2.transform.position);
        }
        if (linkType == 2)
        {
            line1.SetPosition(0, g1.transform.position);
            line1.SetPosition(1, z2);

            line2.SetPosition(0, z2);
            line2.SetPosition(1, z1);

            line3.SetPosition(0, z1);
            line3.SetPosition(1, g2.transform.position);
        }

        StartCoroutine(DestoryLine());
    }
    IEnumerator DestoryLine()
    {
        yield return new WaitForSeconds(0.2f);
        line1.SetPosition(0, Vector3.zero);
        line1.SetPosition(1, Vector3.zero);

        line2.SetPosition(0, Vector3.zero);
        line2.SetPosition(1, Vector3.zero);

        line3.SetPosition(0, Vector3.zero);
        line3.SetPosition(1, Vector3.zero);
    }
}
