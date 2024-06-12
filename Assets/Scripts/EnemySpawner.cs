using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    public GameObject spawnParticle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner(){
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawn){
            yield return wait;
            GameObject enemyToSpawn = ChooseEnemy();
            
            Instantiate(spawnParticle, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject ChooseEnemy(){
        int rand = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[rand];
    }
}
