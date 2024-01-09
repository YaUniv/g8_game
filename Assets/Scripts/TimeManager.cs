using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float time;

    public float timeLim;
    float timeView;

    public TextMeshProUGUI timeText;

    private void Awake()
    {
        instance = this;
        time = 0;
        timeView = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        timeView = timeLim - time;
        timeView = Mathf.Ceil(timeView);

        timeText.text = timeView.ToString("F0");
    }
}
