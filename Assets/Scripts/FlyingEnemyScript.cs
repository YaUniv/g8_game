using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    Vector2 pos;    //�ʒu
    Rigidbody2D rb; //RigidBody2D
    Vector2 vel;    //���x
    float time;     //����

    bool canMove;   //�����邩

    public float accel;
    public float maxMoveSpeed;

    Transform playerTrf;

    public Sprite[] fly;

    SpriteRenderer sr;

    public SpriteRenderer eyeSr;
    float eyeTime;
    bool missileAttack;
    public GameObject missileObj;
    public Transform muzzleTrf;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        canMove = false;

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = fly[0];

        missileAttack = false;

        eyeSr.color = new Color(1,1,1,0);

        playerTrf = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        vel = rb.velocity;
        time += Time.deltaTime;

        sr.sprite = fly[(int) (time * 2) % 2];

        if (playerTrf == null) { return; }
        if (Mathf.Abs(pos.x - playerTrf.position.x) < 8f)
        {
            canMove = true;
        }
        if (!canMove) { return; }

        //����
        vel.x -= accel * Time.deltaTime;
        //�ō����x�ȉ���
        if (vel.x < -maxMoveSpeed) { vel.x = -maxMoveSpeed; }

        //���x�K��
        rb.velocity = vel;

        if (!missileAttack && Mathf.Abs(pos.x - playerTrf.position.x) < 0.5f && pos.y - playerTrf.position.y > 0f)
        {
            missileAttack = true;
            Instantiate(missileObj, muzzleTrf.position, Quaternion.identity);
            eyeTime = time;
        }

        if (missileAttack)
        {
            float r = Mathf.Pow((time -eyeTime) / 1, 2);
            r = Mathf.Clamp01(r);
            eyeSr.color = new Color(1, 1, 1, 1 - r);
        }
    }
}
