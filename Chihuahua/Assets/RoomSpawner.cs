using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public List <Transform> adjacentLocations;

    public bool roomActive = false;
    private Camera cam;

    private GameObject gm;

    void Awake(){
        cam = FindObjectOfType<Camera>();
        gm = GameObject.FindGameObjectWithTag("GM");
    }

    void Start(){
        if(GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated < GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().maxRooms){
            SpawnRoom();
        }
    }

    private void Update() {
        if(roomActive == true){
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            roomActive = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            roomActive = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            roomActive = true;
        }
    }

    void SpawnRoom(){
        
        foreach (var location in adjacentLocations){
                int randomNum = Random.Range(0, adjacentLocations.Count);
                Transform randomAdjLocation = adjacentLocations[randomNum];
                if(randomAdjLocation.tag == "T" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().tops[Random.Range(0, gm.GetComponent<LevelGeneration>().tops.Length)], randomAdjLocation.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y));
                }
                if(randomAdjLocation.tag == "B" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().bottoms[Random.Range(0, gm.GetComponent<LevelGeneration>().bottoms.Length)], randomAdjLocation.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y));
                }
                if(randomAdjLocation.tag == "L" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().lefts[Random.Range(0, gm.GetComponent<LevelGeneration>().lefts.Length)], randomAdjLocation.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y));
                }
                if(randomAdjLocation.tag == "R" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().rights[Random.Range(0, gm.GetComponent<LevelGeneration>().rights.Length)], randomAdjLocation.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(randomAdjLocation.position.x, randomAdjLocation.position.y));
                }

        }
    }


}
