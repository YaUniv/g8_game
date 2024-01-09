using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemyScript : MonoBehaviour
{
    Vector2 pos;    //�ʒu
    Rigidbody2D rb; //RigidBody2D
    Vector2 vel;    //���x
    float time;     //����

    bool canMove;   //�����邩
    bool run;

    public float accel;
    public float maxMoveSpeed;

    Transform playerTrf;

    public Sprite standSpr;
    public Sprite runSpr;

    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        canMove = false;
        run = false;

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = standSpr;

        playerTrf = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        vel = rb.velocity;
        time += Time.deltaTime;

        if (playerTrf == null) { return; }
        if (Mathf.Abs(pos.x - playerTrf.position.x) < 6f)
        {
            canMove = true;
        }
        if (!canMove) { return; }
        if (Mathf.Abs(pos.y - playerTrf.position.y)  < 0.5f)
        {
            run = true;
            sr.sprite = runSpr;
        }
        if (!run) { return; }


        //����
        vel.x -= accel * Time.deltaTime;
        //�ō����x�ȉ���
        if (vel.x < -maxMoveSpeed) { vel.x = -maxMoveSpeed; }

        //���x�K��
        rb.velocity = vel;
    }
}
