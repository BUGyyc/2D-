using UnityEngine;

public class LevelMoveStep : MonoBehaviour {
    public GameObject DiePanel;
    public LevelState levelState = LevelState.ACTIVE;

    List<Transform> lineList = new List<Transform> ();
    LevelManager levelManager;
    private void Start () {
        levelManager = GetComponent<LevelManager> ();
        lineList = GetAllChild (transform);

    }

    List<Transform> GetAllChild (Transform obj) {
        List<Transform> list = new List<Transform> ();
        int number = obj.childCount;
        for (int i = 0; i < number; i++) {
            list.Add (obj.GetChild (i));
        }
        return list;
    }

    void createLevel () {
        Transform last = lineList[lineList.Count - 1];
        List<Transform> list = GetAllChild (last);
        Transform enemy = levelManager.OnCreateEnemy ();
        int index = Random.Range (0, last.childCount);
        enemy.position = last.GetChild (index).position;
        enemy.parent = last.GetChild (index);

        for (int i = 0; i < list.Count; i++) {
            if (i != index) {
                Transform obj = levelManager.ItemFactory ();
                if (obj != null) {
                    obj.position = list[i].position;
                    obj.parent = list[i];
                }
            }
        }
    }

    public void LevelGetUp () {
        Vector3 tempPos = lineList[lineList.Count - 1].position;
        for (int i = lineList.Count - 1; i >= 0; i--) {
            if (i == 0) {
                lineList[i].position = tempPos;
            } else {
                lineList[i].position = lineList[i - 1].position;
            }
        }
        destoryTool ();
        lineList.Add (lineList[0]);
        lineList.RemoveAt (0);
        createLevel ();
        if (death ()) {
            levelState = LevelState.OVER;
            DiePanel.SetActive (true);
        }
    }

    void destoryTool () {
        Transform[] lineSon = lineList[0].GetComponentsInChildren<Transform> ();
        for (int i = 0; i < lineSon.Length; i++) {
            if (lineSon[i].tag == "Tool") {
                Destroy (lineSon[i].gameObject);
            }
        }
    }

    bool death () {
        Transform[] list = lineList[0].GetComponent<Transform> ();
        for (int i = 0; i < list.Length; i++) {
            if (list[i].tag == "Enemy") {
                return true;
            }
        }
        return false;
    }

    private void Update () {
        if (levelState == LevelState.ACTIVE && Input.GetKeyDown (KeyCode.Escape)) {
            levelState = LevelState.PAUSE;
            DiePanel.SetActive (true);
            Time.timeScale = 0;
        } else if (levelState == LevelState.PAUSE && Input.GetKeyDown (KeyCode.Escape)) {
            levelState = LevelState.ACTIVE;
            DiePanel.SetActive (false);
            Time.timeScale = 1;
        }
    }
}