using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float score;

    float scoreView;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
        score = 0;
        scoreView = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreView < score)
        {
            scoreView += 1000 * Time.deltaTime;
            if (scoreView > score) { scoreView = score; }
        }

        scoreText.text = scoreView.ToString("F0");
    }
}
