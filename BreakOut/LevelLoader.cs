using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Block block;
    public int block_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        string level = getRandomLevelName();
        Debug.Log(level);
        LoadLevel(level);
    }

    public string getRandomLevelName()//随机获取地图名称
    {
        int level = Random.Range(1, 5);
        //通过地图名称读取文件夹中的txt
        return "Assets/_Complete-Game/Levels/level_" + level + ".txt";
    }

    /// <summary>
    /// 载入地图
    /// </summary>
    public void LoadLevel(string levelName)
    {
        try
        {
            string line;
            StreamReader reader = new StreamReader(levelName, Encoding.Default);
            using (reader)
            {
                float pos_x = -5f;//初始克隆方块位置
                float pos_y = 5.8f;
                line = reader.ReadLine();
                while(line!=null)
                {
                    char[] characters = line.ToCharArray();
                    foreach (char character in characters)
                    {
                        if(character == 'X')
                        {
                            pos_x += 0.87f;
                            continue;
                        }
                        Vector2 b_pos = new Vector2(pos_x, pos_y);
                        Block b = Instantiate(block, b_pos, Quaternion.identity);
                        b.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.4f);//方块大小
                        switch (character)
                        {
                            case 'B':
                                b.GetComponent<Block>().color = "blue";
                                b.GetComponent<Block>().hits = 3;
                                block_count++;
                                break;
                            case 'G':
                                b.GetComponent<Block>().color = "green";
                                b.GetComponent<Block>().hits = 2;
                                block_count++;
                                break;
                            case 'P':
                                b.GetComponent<Block>().color = "pink";
                                b.GetComponent<Block>().hits = 1;
                                block_count++;
                                break;
                            case 'R':
                                b.GetComponent<Block>().color = "red";
                                b.GetComponent<Block>().hits = 5;
                                block_count++;
                                break;
                            case 'Y':
                                b.GetComponent<Block>().color = "yellow";
                                b.GetComponent<Block>().hits = 4;
                                block_count++;
                                break;
                            default:
                                Destroy(b);
                                break;
                        }
                        pos_x += 0.87f;//每块克隆方块的间隔
                    }
                    pos_x = -5.5f;
                    pos_y -= 0.45f;
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }
        catch(IOException e)
        {
            Debug.Log(e.Message);
        }
    }
  
}
