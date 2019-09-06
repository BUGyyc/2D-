using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {
    public Text scoreText;
    public Transform[] enemys;
    public Transform[] tools;

    public Transform OnCreateEnemy () {
        int index = Random.Range (0, enemys.Length);
        Transform enemy = Instantiate (enemys[index]);
        enemy.GetComponent<Renderer> ().material.color = new Color (Random.value, Random.value, Random.value);
        enemy.rotation = Quaternion.Euler (0, 0, Random.Range (0, 90));
        Transform textf = enemy.GetComponent<Text> ().transform;
        textf.rotation = Quaternion.Euler (0, 0, 0);
        int score = System.Convert.ToInt32 (scoreText.text);

        if (score < 100) {
            enemy.GetComponent<Text> ().text = Random.Range (1, 10).ToString ();
        } else {
            enemy.GetComponent<Text> ().text = Random.Range (1, score / 10).ToString ();
        }
        return enemy;
    }

    public Transform ItemFactory () {
        int chance = Random.Range (0, 4);
        if (chance < 3) {
            return null;
        } else {
            OnCreateItem ();
        }
    }

    Transform OnCreateItem () {
        int chance = Random.Range (0, 5);
        if (chance < 4) {
            OnCreateEnemy ();
        } else {
            OnCreateTool ();
        }
    }

    Transform OnCreateTool () {
        int index = Random.Range (0, tools.Length);
        return tools[index];
    }
}