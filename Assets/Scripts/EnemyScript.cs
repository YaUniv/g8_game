using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject ExplodeObj;
    float life;
    public float maxLife;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyDestroy()
    {
        Destroy(gameObject);
        Instantiate(ExplodeObj, transform.position, Quaternion.identity);
        ScoreManager.instance.score += 100;
    }

    public void Damage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            EnemyDestroy();
        }
    }
}
