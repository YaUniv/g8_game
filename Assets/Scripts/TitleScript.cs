using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    float time;
    float fadeTime = 2;

    bool fade;

    public Image blackSr;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        fade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fade = true;
        }

        if (fade)
        {
            time += Time.deltaTime;

            float r = time / fadeTime;
            r = Mathf.Clamp01(r);
            blackSr.color = new Color(0, 0, 0, r);

            if (time >= fadeTime)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
