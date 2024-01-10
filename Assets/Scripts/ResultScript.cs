using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public static float score;
    public static float goalTime;
    public static int tryNum;
    public static bool allItem;
    public static bool allEnemy;


    int resultStep;
    float time;
    float moveTime = 0.5f;

    public Image blackIm;
    public Image blackIm2;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI tryText;
    public GameObject noMissObj;
    public GameObject allItemObj;
    public GameObject allEnemyObj;

    public RectTransform resultTrf;

    // Start is called before the first frame update
    void Start()
    {
        resultStep = 0;
        time = 0;

        scoreText.text = score.ToString("f0");
        timeText.text = goalTime.ToString("f2");
        tryText.text = tryNum.ToString();
        if (tryNum == 1) noMissObj.SetActive(true);
        if (allItem) allItemObj.SetActive(true);
        if (allEnemy) allEnemyObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (resultStep == 0)
        {
            time += Time.deltaTime;

            float r = time / moveTime;
            r = Mathf.Clamp01(r);
            blackIm.color = new Color(0, 0, 0, 1 - r);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                resultStep++;
                time = 0;
            }
        }
        else if (resultStep == 1)
        {
            time += Time.deltaTime;

            float r = time / moveTime;
            r = Mathf.Clamp01(r);
            blackIm2.color = new Color(0, 0, 0, (1 - r) * 0.5f);
            resultTrf.localPosition = new Vector3(r * 4000, 0, 0);

            if (time >= moveTime)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
