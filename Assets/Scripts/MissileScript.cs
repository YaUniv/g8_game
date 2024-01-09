using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject ExplodeObj;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            MissileDestroy();
        }
    }

    public void MissileDestroy()
    {
        Destroy(gameObject);
        GameObject explode = Instantiate(ExplodeObj, transform.position, Quaternion.identity);
        explode.transform.localScale *= 0.5f;
    }
}
