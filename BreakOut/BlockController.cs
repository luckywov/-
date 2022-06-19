using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject upgradePrefab;
    public GameObject racket;
    void Start()
    {
        racket = GameObject.Find("racket");
        string spriteFileName = "Sprites/block_" + GetComponent<Block>().color;//��ȡ��ɫ����
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.Sprite>(spriteFileName);//��ͼ
    }
    /// <summary>
    /// ����ש�����ײ���
    /// </summary>
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject go = GameObject.Find("Main Camera");
        LevelLoader levelLoader = go.GetComponent<LevelLoader>();
        gameObject.GetComponent<Block>().hits-=racket.GetComponent<Racket>().damage;
        if(gameObject.GetComponent<Block>().hits <= 0)
        {
            Destroy(gameObject);
            levelLoader.block_count--;
            if(Random.value<0.10)//�������ɵĸ���
            {
                Instantiate(upgradePrefab,
                    new Vector3(
                        col.gameObject.transform.position.x,
                        col.gameObject.transform.position.y,
                        0),
                        Quaternion.identity);
            }
        }
    }
   
}
