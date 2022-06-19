using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    #region ����
    //������Ϸʤ������
    public GameObject Win;
    //��������Ϊ�������У�ÿ�ſ�Ƭ����֮��ļ����С
    public const int gridRows = 3;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;
    //��ʼ��λ(-3,0)
    public const float originalX = -3;
    public const float originalY = 0;
    ////����һ����Ϸ����Ϊԭʼ��Ƭ
    //public GameObject originalCard;
    public MemoryCard originalCard;
    //����ͼƬ����
    public Sprite[] images;

    //����������Ƭ�����ڵ���ж�ʱʹ��
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    //��ӷ����Ͳ�������
    public Text ScoreLabel;
    public Text StepLabel;
    public int _step = 0;
    public int _score = 0;


    #endregion
    #region ����
    private void Start()
    {
        //һ��ʼWin��ϢͼƬ����ʾ
        Win.SetActive(false);
        //���ò����ң�����Ԫ�ص�ֵ��Ϊ������±꣬���ڽ�ͼƬ���Ƹ���Ƭ
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers);
        //�����������еĿ�Ƭ
        for(int i =0;i<gridCols;i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                #region ���ɿ�Ƭ
                //Instantiate(originalCard, new Vector2(offsetX * i + originalX,
                //    offsetY * j + originalY), Quaternion.identity);
                //originalCard.GetComponent<SpriteRenderer>().sprite =
                //  images[numbers[j * gridCols + i]];
                #endregion
                card = Instantiate(originalCard) as MemoryCard;

                //��˳����ƶ�������λ���±꣬����id����ʾͼƬ
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);
                //�����¿�Ƭ��λ��
                float posX = (offsetX * i) + originalX;
                float posY = (offsetY * j) + originalY;
                card.transform.position = new Vector3(posX, posY, 1);
            }
        }
    }

    /// <summary>
    /// ���Ե��״̬���жϵڶ��ſ�Ƭ���״̬�Ƿ񱻸ı�
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
            //���ε���ɹ�֮��������
            _step++;
            StepLabel.text = "Step:" + _step;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        //������ſ�Ƭ��id��ͬ���������ӣ�����������ӵ�һ��ֵ���ж�ʤ��
        //�������ͬ���ȴ�1s����Ƭ��ת
        //������������״̬
         if(_firstRevealed.id==_secondRevealed.id)
        {
            //��Գɹ����������
            _score++;
            ScoreLabel.text = "Score:" + _score;
            //��Ϊ���俨Ƭ��������Եģ����Ե��������� ���п�Ƭ������һ��ʱ����Ϸ��������ʾ��Ϸʤ������
            if (_score == ((gridRows * gridCols) / 2)) Win.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            //�����ſ��ı��涼��ʾ����
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //�����ε���Ŀ�Ƭ�����
        _firstRevealed = null;
        _secondRevealed = null;
    }

    /// <summary>
    /// �������麯����ϴ�ƣ�
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    private int[] ShuffleArray(int[] numbers)
    {
        //��������
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length;i++)
        {
            //������������
            int tmp = newArray[i];
            int r = Random.Range(i,newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    #endregion
}
