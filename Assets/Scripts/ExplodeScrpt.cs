using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScrpt : MonoBehaviour
{
    float time;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 1)
        {
            float r = Mathf.Pow(time / 1, 2);
            sr.color = new Color(1, 1, 1, 1 - r);
        }
        else Destroy(gameObject);
    }
}
