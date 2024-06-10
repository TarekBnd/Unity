using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 5f;
    public float Verticalrange = 5f;

    private BoxCollider gunTrigger;

    public EnemyManager EnemyManager;
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, Verticalrange, range);
        gunTrigger.center = new Vector3(0,0, range * 0.5f);  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        {
            if(enemy)
            {
                EnemyManager.AddEnemy(enemy);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        {
            if(enemy)
            {
                EnemyManager.RemoveEnemy(enemy);
            }
        }
    }
}
