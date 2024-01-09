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


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        transform.localScale = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            Destroy(this.gameObject);
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
        transform.localEulerAngles = new Vector3(0, 0, -r1 * 720 * lr);
        transform.localScale = new Vector2(lr, 1) * r2;
    }
}
