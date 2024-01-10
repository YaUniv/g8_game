using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandScript : MonoBehaviour
{
    Vector2 pos;
    //float time;

    public bool down;
    float y;
    float speed = 4;

    public BossMoveScript bossMoveScript;

    // Start is called before the first frame update
    void Start()
    {
        //time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if (down)
        {
            pos.y -= speed *Time.deltaTime;
        }
        else
        {
            pos.y += speed * Time.deltaTime;
        }

        pos.y = Mathf.Clamp(pos.y, 0, 7);
        transform.position = pos;
    }

    public void Damage(float damage)
    {
        bossMoveScript.Damage(damage);
    }
}
