using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject room;
    public Transform[] adjacentLocations;
    private int roomsGenerated;
    private int maxRooms;

    public bool roomActive = false;
    public Camera cam;

    void Awake(){
        roomsGenerated = GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated;
        maxRooms = GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().maxRooms;    
    }

    void Start(){
        if(roomsGenerated < maxRooms){
            Instantiate(room, adjacentLocations[Random.Range(0, adjacentLocations.Length)].position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("GM").GetComponent<LevelGeneration>().roomsGenerated += 1;
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

}
