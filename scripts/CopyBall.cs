using UnityEngine;

public class CopyBall : MonoBehaviour {
    // public Transform ball;
    private void OnCollisionEnter2D (Collision2D collision) {
        Transform tf = collision.transform;
        //在小球位置加载一个新小球
        GameObject newBall = Instantiate (Resources.Load ("Prefab/Ball"), tf.position, tf.rotation) as GameObject;
        //新小球认小球的父物体“Balls”为自己的父物体
        newBall.transform.parent = tf.parent;
        //新小球往右跳     
        newBall.GetComponent<Rigidbody2D> ().AddForce (transform.right * 0.02f);
        //旧小球往左跳
        tf.GetComponent<Rigidbody2D> ().AddForce (-transform.right * 0.02f);
        //销毁复制道具
        Destroy (gameObject);
    }
}