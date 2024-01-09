using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderScript : MonoBehaviour
{
    EnemyScript enemyScript;  //動きのスクリプト

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = this.transform.parent.GetComponent<EnemyScript>();   //動作スクリプトの取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyDestroy()
    {
        enemyScript.EnemyDestroy();
    }

    public void Damage(float damage)
    {
        enemyScript.Damage(damage);
    }
}
