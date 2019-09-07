using UnityEngine;
using UnityEngine.UI;
public class BallMove : MonoBehaviour {
    float timer;
    public BallState state = BallState.READY;

    private void OnCollisionEnter2D (Collision2D other) {
        if (state == BallState.BATTLE) {
            GetComponent<Rigidbody2D> ().gravityScale = 1;
            if (other.gameObject.tag == "Enemy") {
                //获取敌人数字
                Text enemyNumber = other.transform.GetChild (0).GetComponent<Text> ();
                //获取当前分数
                Text Score = GameObject.Find ("ScoreText").GetComponent<Text> ();
                if (tag == "BigBall") //如果自己是大球
                {
                    //敌人数字-2
                    enemyNumber.text = ((System.Convert.ToInt32 (enemyNumber.text)) - 2).ToString ();
                    //当前分数+2
                    Score.text = ((System.Convert.ToInt32 (Score.text)) + 2).ToString ();
                } else //如果自己是小球
                {
                    //敌人数字-1
                    enemyNumber.text = ((System.Convert.ToInt32 (enemyNumber.text)) - 1).ToString ();
                    //当前分数+1
                    Score.text = ((System.Convert.ToInt32 (Score.text)) + 1).ToString ();
                }
            }
        }
    }

    private void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            timer += Time.deltaTime;
            if (timer > 1) {
                switch (Random.Range (0, 4)) //随机方向弹开
                {
                    case 0:
                        GetComponent<Rigidbody2D> ().AddForce (transform.up * 0.01f);
                        break;
                    case 1:
                        GetComponent<Rigidbody2D> ().AddForce (-transform.up * 0.01f);
                        break;
                    case 2:
                        GetComponent<Rigidbody2D> ().AddForce (transform.right * 0.01f);
                        break;
                    case 3:
                        GetComponent<Rigidbody2D> ().AddForce (-transform.up * 0.01f);
                        break;
                }
            }
        }
    }

    private void OnCollisionExit2D (Collision2D other) {
        timer = 0;
    }

    private void Update () {
        transform.Rotate (0, 0, 0.0001f);
        switch (state) {
            case BallState.BORE:
                GetComponent<Rigidbody2D> ().gravityScale = 0;
                break;
            case BallState.READY:
                GetComponent<CircleCollider2D> ().isTrigger = false;
                GetComponent<Rigidbody2D> ().Sleep ();
                break;
        }
    }
}