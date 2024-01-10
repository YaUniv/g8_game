using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerObj;
    PlayerMoveScript playerMoveScript;

    public bool gameOver;
    public bool goal;

    public float time;
    public float timeLim;
    float timeView;
    bool timer;

    float goalTime;

    public bool maskFade;
    float time2;

    public TextMeshProUGUI timeText;

    public MaskFadeScript maskFadeScript;

    public static int tryNum;


    private void Awake()
    {
        instance = this;

        gameOver = false;
        goal = false;

        time = 0;
        timeView = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = playerObj.GetComponent<PlayerMoveScript>();
        timer = true;
        maskFade = false;

        MusicManager.MusicPlay(0);

        tryNum++;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;

            timeView = timeLim - time;
            timeView = Mathf.Ceil(timeView);
            timeView = Mathf.Max(timeView, 0);

            timeText.text = timeView.ToString("F0");

            if (!gameOver && time >= timeLim)
            {
                playerMoveScript.PlayerDestroy();
            }
        }

        if (maskFade)
        {
            time2 += Time.deltaTime;
            if (time2 >= 4)
            {
                Result();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(Retry());
        MusicManager.MusicStop();
    }

    public void Goal()
    {
        goal = true;
        goalTime = time;
        timer = false;
        MusicManager.MusicPlay(1);

        ResultScript.score = ScoreManager.instance.score;
        ResultScript.goalTime = goalTime;
        ResultScript.tryNum = tryNum;
        if (GameObject.Find("Items").transform.childCount == 0) ResultScript.allItem = true;
        else ResultScript.allItem = false;
        if (GameObject.Find("Enemys").transform.childCount == 0) ResultScript.allEnemy = true;
        else ResultScript.allEnemy = false;

    }

    // コルーチン本体
    private IEnumerator Retry()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MaskFade()
    {
        maskFade = true;
        time2 = 0;
        maskFadeScript.MaskFatdeStart();
    }

    void Result()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
