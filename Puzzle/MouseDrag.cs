using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private Vector2 _vec3Offset;        //�������Ƭ��ƫ����
    public Vector2 _ini_pos;            //��ʼλ��
    private Transform targetTransform;  //Ŀ������
    public static int width = 3;
    public static int height = 3;

    private bool isMouseDown = false;   //����Ƿ���
    private Vector3 lastMousePosition = Vector3.zero;

    float chipWidth = 1;                //��Ƭ���
    float chipHeight = 1;               //��ƴ�߶�

    public float threshold = 0.5f;      //�ٽ�ֵ

    private float timeOffset;

    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            timeOffset = Time.realtimeSinceStartup;
            //Debug.Log(timeOffset);
            for(int i = 0;i<width;i++)
            {
                for(int j = 0;j<height;j++)
                {
                    float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                    float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

                    float picX = CreatePic.pic[i, j].transform.position.x;
                    float picY = CreatePic.pic[i, j].transform.position.y;

                    if (Mathf.Abs(mouseX - picX) < chipWidth  / 2 &&
                        Mathf.Abs(mouseY - picY) < chipHeight / 2) 
                    {
                        targetTransform = CreatePic.pic[i, j].transform;
                        //��¼��Ƭ��ʼλ��
                        _ini_pos = new Vector2(targetTransform.position.x, targetTransform.position.y);
                        break;
                    }
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            if(Time.realtimeSinceStartup-timeOffset<0.2f)
            {
                Debug.Log("OneClickRotate");
                OneClickRotate();
            }
            lastMousePosition = Vector3.zero;
            OnMyMouseUp();
            GameOver.Judge();
        }
        if(isMouseDown)
        {
            if(lastMousePosition!=Vector3.zero)
            {
                //��Ŀ�������������ϲ�
                targetTransform.GetComponent<SpriteRenderer>().sortingOrder = 100;
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;//���ƫ����
                //��Ƭ��ǰλ�� = ��Ƭ��һ֡��λ�� + ���ƫ����
                targetTransform.position += offset;
            }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void OneClickRotate()
    {
        targetTransform.Rotate(0, 0, -90, Space.Self);
        //Debug.Log($"({q.x},{q.y},{q.z})");
    }

    void OnMyMouseUp()
    {
       
        bool flag = false;
        for(int j = 0;j<width;j++)
        {
            for(int i = 0;i<height;i++)
            {
                //Debug.Log("acd1:("+targetTransform.position.x+","+j+")");
                //Debug.Log("acd2:("+targetTransform.position.y+","+i+")");
                var acd1 = Mathf.Abs(targetTransform.position.x - j) < threshold;
                var acd2 = Mathf.Abs(targetTransform.position.y - i) < threshold;
                //Debug.Log(acd1 + "&&" + acd2);
                //Debug.Log("�뿪������߳ǣ�����");
                if (acd1 &&acd2)
                {
                    Debug.Log("OnMyMouseUp");
                    targetTransform.position = new Vector2(j, i);//ʵ������Ч��
                    
                    targetTransform.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    return;
                }
            }
        }
        if(!flag)
        {
            targetTransform.position = _ini_pos;
        }
    }
}
