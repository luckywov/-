using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������
/// </summary>
public class Gophers : MonoBehaviour
{
    //����һ���µ���Ϸ����beaten
    public GameObject beaten;
    public int id;

    /// <summary>
    /// ������ֺ����δ�����У���������Զ�����
    /// </summary>
    void Update()
    {
        Destroy(gameObject, 3.0f);
        //����Ӧ���ڵ�isAppear��Ϊfalse
        FindObjectOfType<GameControl>().holes[id].isAppear = false;
    }

    /// <summary>
    /// ���������
    /// </summary>
    private void OnMouseDown()
    {
        //����ͬ��λ������һ��������ͼ��ĵ���
        GameObject g;
        g = Instantiate(beaten, gameObject.transform.position, Quaternion.identity);
        //��0.1s��ݻٵ�ǰ���ɵĵ���
        Destroy(gameObject);
        //����ǰ����id���ݸ�beaten
        g.GetComponent<Beaten>().id = id;

        //���ӷ���
        FindObjectOfType<GameControl>().score += 1;
        int scores = FindObjectOfType<GameControl>().score;
        GameObject.Find("Score").gameObject.GetComponent<TextMesh>().text = "Score:" + scores.ToString();
    }

}
