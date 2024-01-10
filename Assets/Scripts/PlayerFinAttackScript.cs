using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinAttackScript : MonoBehaviour
{
    public Transform playerObj;
    public float finAttackTime;

    Vector2 pos;
    float time;

    public float power;

    public int lr;      //¶‰E

    public Transform finTrf;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        finTrf.localScale = new Vector2(0, 0);

        AudioSource.PlayClipAtPoint(SEManager.sounds[2], Vector3.zero, SEManager.volume[2]);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.position = playerObj.position;
        pos = transform.position;

        time += Time.deltaTime;
        if (time > finAttackTime)
        {
            Destroy(this.gameObject);
        }

        float r = time / finAttackTime; r = Mathf.Clamp01(r);
        float r1 = Mathf.Pow(Mathf.Sin(r * Mathf.PI/2),2);
        float r2 = Mathf.Pow(Mathf.Sin(Mathf.Clamp(r,0,0.9999f) * Mathf.PI), 0.25f);
        finTrf.localEulerAngles = new Vector3(0, 0, -r1 * 720 * lr);
        finTrf.localScale = new Vector2(lr, 1) * r2 * 1.5f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyCollider")
        {
            collision.gameObject.GetComponent<EnemyColliderScript>().Damage(power * Time.deltaTime);
        }
        if (collision.gameObject.tag == "EnemyMissile")
        {
            collision.gameObject.GetComponent<MissileScript>().MissileDestroy();
        }
        if (collision.transform.tag == "BossHand")
        {
            collision.gameObject.GetComponent<BossHandScript>().Damage(power * Time.deltaTime);
        }
    }
}
