using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveScript : MonoBehaviour
{
    Vector3 pos;
    Rigidbody2D rb;
    Vector2 vel;

    public Transform playerTrf;
    Vector2 playerPos;

    public Vector2 limMax;
    public Vector2 limMin;

    public RawImage backImage;
    public RawImage backImage2;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        vel = rb.velocity;

        if (playerTrf != null)
        {
            playerPos = playerTrf.position;
        }

        if (GameManager.instance.boss || GameManager.instance.bossCatCome || GameManager.instance.bossClear)
        {
            transform.position = new Vector3(limMax.x, limMin.y, pos.z);

            return;
        }
        

        vel = playerPos - (Vector2)pos;

        vel *= 5;

        if (pos.x < limMin.x)
        {
            pos.x = limMin.x;
            transform.position = pos;
            vel.x = 0;
        }
        if (pos.x > limMax.x)
        {
            pos.x = limMax.x;
            transform.position = pos;
            vel.x = 0;
        }
        if (pos.y < limMin.y)
        {
            pos.y = limMin.y;
            transform.position = pos;
            vel.y = 0;
        }
        if (pos.y > limMax.y)
        {
            pos.y = limMax.y;
            transform.position = pos;
            vel.y = 0;
        }

        rb.velocity = vel;

        backImage.uvRect = new Rect(pos.x / 47, pos.y / 20, 1, 1);
        backImage2.uvRect = new Rect(pos.x / 47, pos.y / 20, 1, 1);

    }
}
