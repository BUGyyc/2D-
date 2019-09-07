using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallFindWay : MonoBehaviour {
    public Transform muzzle;
    public float boreSpeed = 0.2f;
    private void OnTriggerStay2D (Collider2D other) {
        Rigidbody2D r2d = other.GetComponent<Rigidbody2D> ();
        switch (name) {
            case "LeftDown":
                r2d.AddForce (-transform.right * 0.002f);
                break;
            case "RightDown":
                r2d.AddForce (transform.right * 0.003f);
                break;
            case "Left":
            case "Right":
                r2d.AddForce (transform.up * 0.002f);
                break;
            case "Up":
                //启动协程寻路（上膛）   
                StartCoroutine (MoveToMuzzle (other.transform, muzzle));
                //打开小球触发器,使小球能越过枪口阀门
                other.GetComponent<CircleCollider2D> ().isTrigger = true;
                break;
        }
    }

    public IEnumerator MoveToMuzzle (Transform ball, Transform muzzle) {
        ball.GetComponent<BallMove> ().state = BallState.BORE;
        while (ball.GetComponent<BallMove> ().state == BallState.BORE) {
            ball.position = Vector3.MoveTowards (ball.position, muzzle.position, boreSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate ();
            if ((ball.position - muzzle.position).sqrMagnitude <= 0.001f) {
                ball.GetComponent<BallMove> ().state = BallState.READY;
                ball.position = muzzle.position;
            }
        }
    }
}