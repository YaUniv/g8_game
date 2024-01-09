using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderScript : MonoBehaviour
{
    PlayerMoveScript playerMoveScript;  //動きのスクリプト

    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = this.transform.parent.GetComponent<PlayerMoveScript>();   //動作スクリプトの取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCollider")
        {
            float enemyColliderScaleY = collision.transform.localScale.y;
            if (transform.position.y > collision.gameObject.transform.position.y + enemyColliderScaleY/2 - 0.1f)
            {
                playerMoveScript.EnemyStep();
                collision.gameObject.GetComponent<EnemyColliderScript>().EnemyDestroy();
            }
            else
            {
                playerMoveScript.PlayerDestroy();
            }
        }

        if (collision.gameObject.tag == "EnemyMissile")
        {
            playerMoveScript.PlayerDestroy();
            collision.gameObject.GetComponent<MissileScript>().MissileDestroy();
        }
    }
}
