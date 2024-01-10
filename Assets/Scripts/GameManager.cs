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

    public float bossTime;
    public float bossTimeLim;
    float bossTimeView;

    float goalTime;
    float bossClearTime;

    public bool maskFade;
    public float time2;
    float time3;

    public TextMeshProUGUI timeText;

    public MaskFadeScript maskFadeScript;

    public static int tryNum;

    public bool bossCatFrag;
    public bool bossCatCome;
    public bool boss;
    public bool bossClear;

    public GameObject bossWall;

    public RectTransform goalTrf;
    public RectTransform clearTrf;

    public BossMoveScript bossMoveScript;

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


        float rand = Random.Range(0, 100);
        if (rand < 33)
        {
            bossCatFrag = true;
        }
        else
        {
            bossCatFrag = false;
        }
        bossTime = 0;
        bossTimeView = 0;
        bossCatCome = false;
        boss = false;
        bossClear = false;

        bossWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            if (!gameOver)
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
            if (!bossCatFrag && time2 >= 4)
            {
                Result();
            }

            if (bossCatFrag && time2 >= 2)
            {
                bossCatCome = true;
                maskFade = false;
                goal = false;
                time2 = 0;
                StartCoroutine(BossCome());
            }
        }

        if (bossCatCome)
        {
            time2 += Time.deltaTime;
            timeText.text = bossTimeLim.ToString("F0");
            goalTrf.localPosition = new Vector2(goalTrf.localPosition.x, goalTrf.localPosition.y + Mathf.Max((time2-1)*0.5f, 0));

            if (time2 >= 6)
            {
                bossCatCome = false;
                boss = true;
                playerMoveScript.ReMove();
                bossWall.SetActive(true);
                bossMoveScript.BossStart();
            }
        }

        if (boss)
        {
            if (!gameOver)
                bossTime += Time.deltaTime;

            bossTimeView = bossTimeLim - bossTime;
            bossTimeView = Mathf.Ceil(bossTimeView);
            bossTimeView = Mathf.Max(bossTimeView, 0);

            timeText.text = bossTimeView.ToString("F0");

            if (!gameOver && bossTime >= bossTimeLim)
            {
                playerMoveScript.PlayerDestroy();
            }
        }

        if (bossClear)
        {
            time3 += Time.deltaTime;
            clearTrf.localPosition = new Vector2(clearTrf.localPosition.x, 1 + Mathf.Max((6 - time3) * 1.2f, 0));
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
        ResultScript.bossClear = false;

    }

    public void BossClear()
    {
        bossClear = true;
        boss = false;
        bossCatFrag = false;
        bossClearTime = bossTime;
        time3 = 0;
        MusicManager.MusicPlay(3);
        bossMoveScript.BossEnd();

        ResultScript.bossClear = true;
        ResultScript.bossTime = bossClearTime;

        StartCoroutine(BossEnd());
    }

    private IEnumerator BossEnd()
    {
        yield return new WaitForSeconds(9);
        maskFadeScript.MaskFatdeStart();
        yield return new WaitForSeconds(4);
        Result();
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

    private IEnumerator BossCome()
    {
        yield return new WaitForSeconds(1);

        MusicManager.MusicPlay(2);
        bossMoveScript.BossCome();
    }
}
