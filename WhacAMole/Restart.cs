using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
    ///<summary>
    ///��ť������Ժ����µ�����Ϸ����
    /// </summary>
    public void OnMouseDown()
    {
        Debug.Log("restart");
        EditorSceneManager.LoadScene("Game");
    }
}
