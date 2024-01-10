using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFadeScript : MonoBehaviour
{
    bool start;

    float time;

    public GameObject blackObj;
    public Transform maskTrf;

    public Transform playerTrf;

    float maskSize;
    float maskMaxSize = 27;
    float fadeTime = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        start = false;

        time = 0;

        maskSize = maskMaxSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start) return;

        time += Time.deltaTime;

        maskTrf.position = playerTrf.position;


        if (GameManager.instance.bossCatCome)
        {
            float r = 2 / fadeTime - Mathf.Max(0, (time - 3)*50);
            r = Mathf.Clamp01(r);
            maskSize = Mathf.Pow(1 - r, 3) * maskMaxSize;
            maskTrf.localScale = Vector2.one * maskSize;

            if (r == 0)
            {
                start = false;
                time = 0;
                maskSize = maskMaxSize;
                blackObj.SetActive(false);
            }
        }
        else
        {
            float r = time / fadeTime;
            r = Mathf.Clamp01(r);
            maskSize = Mathf.Pow(1 - r, 3) * maskMaxSize;
            maskTrf.localScale = Vector2.one * maskSize;
        }
    }

    public void MaskFatdeStart()
    {
        start = true;
        blackObj.SetActive(true);
        time = 0;
        maskSize = maskMaxSize;
    }
}
