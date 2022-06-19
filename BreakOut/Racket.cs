using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Racket : MonoBehaviour
{
    public float speed =20.0f;//横板移动速度
    public Ball ball;
    public int ballCount = 0;
    public int damage = 1;
    public int speedUp = 1;
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //当横板未超过屏幕左侧时移动横板，否则不能移动
            if (transform.position.x > -5.2)  transform.Translate(Vector3.left * Time.deltaTime * speed);
            else return;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 5.2) transform.Translate(Vector3.right * Time.deltaTime * speed);
            else return;
        }
        else if(Input.GetKey(KeyCode.UpArrow)&&ballCount>0)
        {
            var g= Instantiate(ball, gameObject.transform.position, Quaternion.identity);
            ballCount--;
        }
        else if(Input.GetKey(KeyCode.Space ))
        {
            SceneManager.LoadScene("Game");
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            GameObject[] AllBalls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var item in AllBalls)
            {
                item.GetComponent<Ball>().AllBack();
            }
        }
        else if(Input.GetKey(KeyCode.LeftShift)&&speedUp==1)
        {
            GameObject[] AllBalls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var item in AllBalls)
            {
                item.GetComponent<Ball>().BallSpeed+=20;
            }
            speedUp = 0;
            Debug.Log("SpeedUp!!!!!!!");
        }
    }

    ///<summary>
    ///道具生效
    /// </summary>
    void performUpgrade(string name)
    {
        //removing unity-attached suffixed data to get original sprite name
        name = name.Remove(name.Length - 21);
        float x;
        Ball ballController = GameObject.Find("ball").GetComponent<Ball>();
        Debug.Log($"Got UpGrade {name}");
        switch(name)
        {
            case "ball_speed_up":
                if(ballController.BallSpeed<27)
                {
                    ballController.BallSpeed += 3;
                    ballCount++;
                }
                break;
            case "ball_speed_down":
                if (ballController.BallSpeed >13 )
                {
                    ballController.BallSpeed -= 3;
                    ballCount++;
                    damage++;
                }
                break;
            case "paddle_size_up":
                x = gameObject.transform.localScale.x;
                ballCount++;
                if (x < 8.0f)
                    gameObject.transform.localScale = 
                        new Vector3(x += 0.25f, gameObject.transform.localScale.y, 1.0f);
                break;
            case "paddle_size_down":
                x = gameObject.transform.localScale.x;
                ballCount++;
                if (x > 3.0f)
                    gameObject.transform.localScale =
                        new Vector3(x -= 0.25f, gameObject.transform.localScale.y, 1.0f);
                break;
            case "paddle_speed_up":
                speed += 3;
                ballCount++;
                break;
            case "paddle_speed_down":
                if (speed > 7) { speed -= 3; ballCount++; }
                    break;
            default:
                break;
        }
    }

    ///<summary>
    ///道具与板接触的触发器
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="upgrade")
        {
            string name = col.gameObject.GetComponent<UpGrade>().upgradeName;
            performUpgrade(name);
            Destroy(col.gameObject);
        }
    }
}
