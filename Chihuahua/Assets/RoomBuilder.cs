using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{

    public GameObject[] floors;
    public GameObject[] cracks;
    public GameObject[] walls;
    public GameObject door;
    public Transform nextRoomPos;
    public GameObject room;
    public List<Transform> doors;
    public List<int> doorIdx;
    public GameObject gm;

    int randomIdx;

    private void Awake() {
        gm = GameObject.FindGameObjectWithTag("GM");
        nextRoomPos = door.transform.Find("NextRoomPos");
        Generate(floors);
        Generate(cracks);
        Generate(walls);
        // GenerateDoors();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate(GameObject[] obj){
        randomIdx = Random.Range(0, obj.Length);
        Instantiate(obj[randomIdx], transform.position, Quaternion.identity);
    }

    void GenerateDoors(){
        int randomQuantity = Random.Range(1, 4);
        for (int i = 0; i < randomQuantity; i++){
            randomIdx = Random.Range(0, doors.Count);
            var newDoor = Instantiate(door, doors[randomIdx].position, doors[randomIdx].rotation);
            // if(gm.GetComponent<LevelGeneration>().roomsGenerated <= gm.GetComponent<LevelGeneration>().maxRooms){
            //     Instantiate(room, newDoor.transform.position, Quaternion.identity);
            //     gm.GetComponent<LevelGeneration>().roomsGenerated += 1;
            // }

            doors.Remove(doors[randomIdx]);
        }

    }

}
