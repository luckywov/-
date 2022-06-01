using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
    ///<summary>
    ///按钮被点击以后，重新调用游戏场景
    /// </summary>
    public void OnMouseDown()
    {
        Debug.Log("restart");
        EditorSceneManager.LoadScene("Game");
    }
}
