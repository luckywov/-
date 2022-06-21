using UnityEngine;

public class CreatePic : MonoBehaviour
{
    public string sprite_Path = "Sprites/Pictures";
    public Sprite[] sp_S;//���е�ͼƬ
    public int textureNum = -1;//ͼƬ���
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
                //������һ����ͼ
                pic[i, j].AddComponent<SpriteRenderer>().sprite = sp_S[textureNum];
                //����Ƭ���õ����λ��
                pic[i, j].GetComponent<Transform>().position = new
                    Vector2(Random.Range(3.0f, 5.5f), Random.Range(0.0f, 2.5f));
                //����Ƭ�����ת90��
                pic[i, j].GetComponent<Transform>().Rotate(0,0, Random.Range(0, 4) * 90,Space.Self);
                Debug.Log(pic[i, j].GetComponent<Transform>().eulerAngles);
            }
    }
}
