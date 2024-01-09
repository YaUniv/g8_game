using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderScript : MonoBehaviour
{
    EnemyScript enemyScript;  //�����̃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = this.transform.parent.GetComponent<EnemyScript>();   //����X�N���v�g�̎擾
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
