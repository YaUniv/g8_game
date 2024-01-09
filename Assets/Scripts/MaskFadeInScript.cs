using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFadeInScript : MonoBehaviour
{
    float time;

    public GameObject blackObj;
    public Transform maskTrf;

    float maskSize;
    float maskMaxSize = 17;
    float fadeTime = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        maskSize = 0;

        blackObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float r = time / fadeTime;
        r = Mathf.Clamp01(r);
        maskSize = r * maskMaxSize;
        maskTrf.localScale = Vector2.one * maskSize;

        if (time > fadeTime)
        {
            Destroy(gameObject);
        }
    }
}
