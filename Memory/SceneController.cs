using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    #region 变量
    //定义游戏胜利物体
    public GameObject Win;
    //设置行数为三行四列，每张卡片中心之间的间隔大小
    public const int gridRows = 3;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;
    //初始定位(-3,0)
    public const float originalX = -3;
    public const float originalY = 0;
    ////设置一个游戏物体为原始卡片
    //public GameObject originalCard;
    public MemoryCard originalCard;
    //建立图片数组
    public Sprite[] images;

    //建立两个卡片对象，在点击判断时使用
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    //添加分数和步数变量
    public Text ScoreLabel;
    public Text StepLabel;
    public int _step = 0;
    public int _score = 0;


    #endregion
    #region 方法
    private void Start()
    {
        //一开始Win信息图片不显示
        Win.SetActive(false);
        //设置并打乱，数组元素的值作为数组的下标，用于将图片复制给卡片
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers);
        //生成三行四列的卡片
        for(int i =0;i<gridCols;i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                #region 生成卡片
                //Instantiate(originalCard, new Vector2(offsetX * i + originalX,
                //    offsetY * j + originalY), Quaternion.identity);
                //originalCard.GetComponent<SpriteRenderer>().sprite =
                //  images[numbers[j * gridCols + i]];
                #endregion
                card = Instantiate(originalCard) as MemoryCard;

                //按顺序给牌定义数字位置下标，赋予id，显示图片
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);
                //设置新卡片的位置
                float posX = (offsetX * i) + originalX;
                float posY = (offsetY * j) + originalY;
                card.transform.position = new Vector3(posX, posY, 1);
            }
        }
    }

    /// <summary>
    /// 可以点击状态，判断第二张卡片点击状态是否被改变
    /// </summary>
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if(_firstRevealed == null) { _firstRevealed = card; }
        else
        {
            _secondRevealed = card;
            //两次点击成功之后步数增加
            _step++;
            StepLabel.text = "Step:" + _step;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        //如果两张卡片的id相同，分数增加，如果分数增加到一定值，判断胜利
        //如果不相同，等待1s将卡片翻转
        //清空两个点击的状态
         if(_firstRevealed.id==_secondRevealed.id)
        {
            //配对成功后分数增加
            _score++;
            ScoreLabel.text = "Score:" + _score;
            //因为记忆卡片是两两配对的，所以当分数到达 所有卡片数量的一半时，游戏结束，显示游戏胜利画面
            if (_score == ((gridRows * gridCols) / 2)) Win.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            //将两张卡的背面都显示出来
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //将两次点击的卡片都清空
        _firstRevealed = null;
        _secondRevealed = null;
    }

    /// <summary>
    /// 打乱数组函数（洗牌）
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    private int[] ShuffleArray(int[] numbers)
    {
        //复制数组
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length;i++)
        {
            //打乱数组内容
            int tmp = newArray[i];
            int r = Random.Range(i,newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    #endregion
}
