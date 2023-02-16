using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public List <Transform> adjacentLocations;

    public bool roomActive = false;
    private GameObject cam;

    private GameObject gm;
    public GameObject barrier;

    string[] levelTypes = new string[] {"boss", "enemy", "item"}; 
    public string levelType;
    public bool isHome = false;

    public GameObject fow;
    GameObject newFow;

    // private GameObject spawnedEnemies;

    void Awake(){
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gm = GameObject.FindGameObjectWithTag("GM");
        SelectRoomType();

    }

    void Start(){
        if(GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated < GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().maxRooms){
            SpawnRoom();
        }

        CloseExits();
        if(levelType == "item" && isHome == false){
            SpawnCollectibles();
        }

        newFow = Instantiate(fow, transform.localPosition, Quaternion.identity, transform);
    }

    private void Update() {
        if(roomActive == true){
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

        if(roomActive){
            newFow.SetActive(false);
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
                if(location.tag == "T" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(location.position.x, location.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().tops[Random.Range(0, gm.GetComponent<LevelGeneration>().tops.Length)], location.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(location.position.x, location.position.y));
                }
                if(location.tag == "B" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(location.position.x, location.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().bottoms[Random.Range(0, gm.GetComponent<LevelGeneration>().bottoms.Length)], location.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(location.position.x, location.position.y));
                }
                if(location.tag == "L" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(location.position.x, location.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().lefts[Random.Range(0, gm.GetComponent<LevelGeneration>().lefts.Length)], location.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(location.position.x, location.position.y));
                }
                if(location.tag == "R" && !gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(new Vector2(location.position.x, location.position.y))){
                    Instantiate(gm.GetComponent<LevelGeneration>().rights[Random.Range(0, gm.GetComponent<LevelGeneration>().rights.Length)], location.transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
                    gm.GetComponent<LevelGeneration>().usedSpawnLocations.Add(new Vector2(location.position.x, location.position.y));
                }

        }
    }

    void CloseExits(){
        foreach (var location in adjacentLocations){
            if(!gm.GetComponent<LevelGeneration>().usedSpawnLocations.Contains(location.position)){
                Instantiate(barrier, location.position, Quaternion.identity);
            }
        }
    }

    void SpawnCollectibles(){
        var newCollectible = Instantiate(gm.GetComponent<LevelGeneration>().collectibles[Random.Range(0, gm.GetComponent<LevelGeneration>().collectibles.Length)], transform.position, Quaternion.identity);
        newCollectible.transform.SetParent(transform, true);
    }

    void SelectRoomType(){
        if(gm.GetComponent<LevelGeneration>().bossSpawned == false){
            levelType = levelTypes[Random.Range(0, levelTypes.Length)];
        }else{
            levelType = levelTypes[Random.Range(1, levelTypes.Length)];
        }
        if(levelType == "boss"){
            gm.GetComponent<LevelGeneration>().bossSpawned = true;
        }
    }

}
