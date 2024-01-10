using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossMoveScript : MonoBehaviour
{
    public RectTransform bossTrf;

    float time;

    bool bossCome;

    bool bossStart;

    bool bossEnd;

    public GameObject[] armLObjs;
    public GameObject[] armRObjs;
    public GameObject handLObj;
    public GameObject handRObj;
    Transform handLTrf;
    Transform handRTrf;
    BossHandScript handLScr;
    BossHandScript handRScr;

    bool canAttack;

    private Coroutine coroutine;

    public float life;
    public float maxLife;

    public Slider slider;
    public GameObject slideObj;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        handLObj.SetActive(false);
        handRObj.SetActive(false);
        armLObjs[0].SetActive(true);
        armLObjs[1].SetActive(false);
        armRObjs[0].SetActive(true);
        armRObjs[1].SetActive(false);

        handLTrf = handLObj.transform;
        handRTrf = handRObj.transform;
        handLScr = handLObj.GetComponent<BossHandScript>();
        handRScr = handRObj.GetComponent<BossHandScript>();

        canAttack = true;

        life = maxLife;
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossCome)
        {
            time += Time.deltaTime;

            float r = time / 5;
            r = Mathf.Clamp01(r);
            bossTrf.localPosition = new Vector2(0, (1-r) * -1080);
        }

        if (bossStart)
        {
            time += Time.deltaTime;

            if (canAttack)
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    coroutine = StartCoroutine(Attack(true, false));
                }
                else if (rand == 1)
                {
                    coroutine = StartCoroutine(Attack(false, true));
                }
                else if (rand == 2)
                {
                    coroutine = StartCoroutine(Attack(true, true));
                }
            }

            //if (time > 4) GameManager.instance.BossClear();
        }

        if (bossEnd)
        {
            time += Time.deltaTime;

            float r = time / 5;
            r = Mathf.Clamp01(r);
            bossTrf.localPosition = new Vector2(0, r * -1080);
        }
    }

    public void BossCome()
    {
        bossCome = true;
        time = 0;

    }

    public void BossStart()
    {
        bossStart = true;
        bossCome = false;
        time = 0;
        bossTrf.localPosition = new Vector2(0, 0);
        handLObj.SetActive(true);
        handRObj.SetActive(true);
        slider.value = 1;
        slideObj.SetActive(true);
    }

    public void BossEnd()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        bossEnd = true;
        bossStart = false;
        time = 0;
        handLObj.SetActive(false);
        handRObj.SetActive(false);
        armLObjs[0].SetActive(true);
        armLObjs[1].SetActive(false);
        armRObjs[0].SetActive(true);
        armRObjs[1].SetActive(false);
        slideObj.SetActive(false);
    }

    private IEnumerator Attack(bool l, bool r)
    {
        canAttack = false;

        yield return new WaitForSeconds(2);

        if (l)
        {
            armLObjs[0].SetActive(false);
            armLObjs[1].SetActive(true);
            Vector2 pos = handLTrf.position;
            pos.x = Random.Range(99.0f, 102.0f);
            handLTrf.position = pos;
            handLScr.down = true;
        }
        if (r)
        {
            armRObjs[0].SetActive(false);
            armRObjs[1].SetActive(true);
            Vector2 pos = handRTrf.position;
            pos.x = Random.Range(99.0f, 102.0f);
            handRTrf.position = pos;
            handRScr.down = true;
        }

        yield return new WaitForSeconds(5);

        if (l)
        {
            handLScr.down = false;
        }
        if (r)
        {
            handRScr.down = false;
        }

        yield return new WaitForSeconds(2);

        if (l)
        {
            armLObjs[0].SetActive(true);
            armLObjs[1].SetActive(false);
        }
        if (r)
        {
            armRObjs[0].SetActive(true);
            armRObjs[1].SetActive(false);
        }

        canAttack = true;
    }

    public void Damage(float damage)
    {
        life -= damage;
        slider.value = life / maxLife;
        if (life <= 0)
        {
            GameManager.instance.BossClear();
        }
    }
}
