using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeamScript : MonoBehaviour
{
    public Transform playerObj;
    public float beamTime;

    Vector2 pos;
    float time;

    LineRenderer line;
    float width = 0.5f;
    BoxCollider2D boxCollider;

    public float power;

    public int lr;      //ç∂âE

    public Transform beamTrf;
    public SpriteRenderer beamInSpr;
    public SpriteRenderer beamSpr;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        //line = GetComponent<LineRenderer>();

        //line.material = new Material(Shader.Find("Sprites/Default"));

        //line.startColor = Color.black;
        //line.endColor = Color.black;

        //line.startWidth = width;
        //line.endWidth = width;

        boxCollider = GetComponent<BoxCollider2D>();

        beamTrf.localPosition = new Vector2(0.5f * lr, 0);
        if (lr < 0) beamInSpr.flipX = true;
        if (lr < 0) beamSpr.flipX = true;

        boxCollider.size = new Vector2(0, width);
        beamInSpr.size = new Vector2(0, 0.5f);
        beamSpr.size = new Vector2(0, 0.5f);
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
        if (time > beamTime)
        {
            Destroy(this.gameObject);
        }


        Vector2 lineEndPos;

        Ray2D ray = new Ray2D(pos, transform.right*lr);
        RaycastHit2D hit;

        int layerMask = 1 << 6;
        float maxDistance = Camera.main.ViewportToWorldPoint(new Vector3(1*(lr+1)/2, 0, 0)).x - pos.x;
        maxDistance = Mathf.Abs(maxDistance);

        hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, layerMask);

        if (hit.collider)
        {
            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.green);

            lineEndPos = hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);

            lineEndPos = pos + Vector2.right * maxDistance;
        }

        //line.SetPosition(0, pos);
        //line.SetPosition(1, lineEndPos);


        boxCollider.offset = new Vector2((lineEndPos.x - pos.x) / 2, 0);
        boxCollider.size = new Vector2(Mathf.Abs(lineEndPos.x - pos.x), width);

        float beamSizeX = Mathf.Abs(lineEndPos.x - pos.x);
        beamInSpr.size = new Vector2(Mathf.Min(beamSizeX, 0.5f), 0.5f);
        beamSpr.size = new Vector2(Mathf.Max(beamSizeX, 0.5f) - 0.5f, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.transform.tag == "Enemy")
        //{
        //    collision.gameObject.GetComponent<EnemyLifeScript>().damage(power * Time.deltaTime);
        //}
    }
}
