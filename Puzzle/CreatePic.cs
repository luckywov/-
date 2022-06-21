using UnityEngine;

public class CreatePic : MonoBehaviour
{
    public string sprite_Path = "Sprites/Pictures";
    public Sprite[] sp_S;//所有的图片
    public int textureNum = -1;//图片序号
    public static GameObject[,] pic = new GameObject[3, 3];
    public static bool isSetTruePosition = false;

    private void Start()
    {
        sp_S = Resources.LoadAll<Sprite>(sprite_Path);

        for(int i = 0; i<3;i++)
            for(int j = 0;j<3;j++)
            {
                textureNum++;
                pic[i, j] = new GameObject("picture" + i + j);
                //给物体一个贴图
                pic[i, j].AddComponent<SpriteRenderer>().sprite = sp_S[textureNum];
                //将碎片放置到随机位置
                pic[i, j].GetComponent<Transform>().position = new
                    Vector2(Random.Range(3.0f, 5.5f), Random.Range(0.0f, 2.5f));
                //将碎片随机旋转90°
                pic[i, j].GetComponent<Transform>().Rotate(0,0, Random.Range(0, 4) * 90,Space.Self);
                Debug.Log(pic[i, j].GetComponent<Transform>().eulerAngles);
            }
    }
}
