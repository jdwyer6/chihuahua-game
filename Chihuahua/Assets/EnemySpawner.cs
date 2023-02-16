using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject gm;
    private Vector3 randomPos;
    private int maxNumOfEnemies = 5;
    public GameObject spawnedEnemies;

    private void Awake() {
        gm = GameObject.FindGameObjectWithTag("GM");
    }

    void Start(){
        if(GetComponent<RoomSpawner>().levelType == "enemy" && GetComponent<RoomSpawner>().isHome == false){
            SpawnEnemies();
        }
    }

    void Update(){
        if(GetComponent<RoomSpawner>().roomActive == true){
            spawnedEnemies.SetActive(true);
        }else{
            spawnedEnemies.SetActive(false);
        }
    }

    void SpawnEnemies(){
        int numOfEnemies = Random.Range(1, maxNumOfEnemies);
        for (int i = 0; i < numOfEnemies; i++){
            randomPos = new Vector3(Random.Range(-5, 5), Random.Range(-2, 2), 0) + transform.position;
            GameObject newEnemy = Instantiate(gm.GetComponent<LevelGeneration>().enemies[Random.Range(0, gm.GetComponent<LevelGeneration>().enemies.Length)], randomPos, Quaternion.identity, spawnedEnemies.transform);
        }

    }
}
