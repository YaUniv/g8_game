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
    float maskMaxSize = 17;
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

        float r = time / fadeTime;
        r = Mathf.Clamp01(r);
        maskSize = Mathf.Pow(1 - r, 2) * maskMaxSize;
        maskTrf.localScale = Vector2.one * maskSize;
    }

    public void MaskFatdeStart()
    {
        start = true;
        blackObj.SetActive(true);
    }
}
