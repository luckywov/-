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
        string spriteFileName = "Sprites/block_" + GetComponent<Block>().color;//获取颜色名称
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.Sprite>(spriteFileName);//贴图
    }
    /// <summary>
    /// 球与砖块的碰撞检测
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
            if(Random.value<0.10)//道具生成的概率
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
