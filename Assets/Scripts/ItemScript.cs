using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerCollider")
        {
            ScoreManager.instance.score += 100;
            Destroy(this.gameObject);

            AudioSource.PlayClipAtPoint(SEManager.sounds[4], Vector3.zero, SEManager.volume[4]);
        }
    }
}
