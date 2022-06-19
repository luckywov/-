using UnityEngine;
using UnityEditor.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.Log("restart");
        EditorSceneManager.LoadScene("Game");
    }
}
