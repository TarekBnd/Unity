using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 5f;
    public float Verticalrange = 5f;
    public float bigDamage = 2f;
    public float smallDamage = 1f;


    public float fireRate;
    private float nextTimeFire;

    private BoxCollider gunTrigger;


    public LayerMask raycastLayerMask;
    public EnemyManager enemyManager;


    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, Verticalrange, range);
        gunTrigger.center = new Vector3(0,0, range * 0.5f);  

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        foreach(var enemy in enemyManager.enemiesInTrigger)
        {
            var direction = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, range * 1.5f, raycastLayerMask))
            {


                if(hit.transform == enemy.transform)
                {
                    float distance = Vector3.Distance(enemy.transform.position, transform.position);

                    if (distance > range * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage);
                    } else
                    {
                        enemy.TakeDamage(bigDamage);

                    }

                }
            }
        }
        
        nextTimeFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        {
            if(enemy)
            {
                enemyManager.AddEnemy(enemy);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        {
            if(enemy)
            {
                enemyManager.RemoveEnemy(enemy);
            }
        }
    }
}
