using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationNEW : MonoBehaviour
{

    public int xScaleMultiplier = 20;
    public int yScaleMultiplier = 10;
    public int maxNumOfRooms = 10;
    public GameObject room;
    public GameObject door;
    public List<Room> rooms = new List<Room>(); 
    public List<Vector2> takenPositions = new List<Vector2>(); 
    public Vector2 startingPos = new Vector2(200, 100);
    public GameObject player;
    public Camera cam;

    private void Awake() {
        CreateRoomLocations();
        CreateRooms();
        PlacePlayer();
    }

    // Start is called before the first frame update
    void Start(){

    }


    void CreateRoomLocations(){
        
        int count = 0;
        int j = 0;
        Vector2 spawnPos;
        Vector2 posToTry;
        spawnPos = startingPos;


        //The problem is here. It is not keeping the same vector2 value when looking for a new position when one is taken
        while(count <= maxNumOfRooms){
            posToTry = NewCoordinates(spawnPos);

            if(!takenPositions.Contains(posToTry)){
                spawnPos = posToTry;
                takenPositions.Add(spawnPos);
                count++;
            }

            j++;
            if(j > 5000){
                Debug.Log("infinte loop");
                break;
            }

        }

    }

    void CreateRooms(){

        for (int i = 0; i < takenPositions.Count; i++){
            Vector2 current = takenPositions[i];
            Vector2 next;
            if(i < (takenPositions.Count - 1)){
                next = takenPositions[i+1];
            }else{
                next = new Vector2(0, 0);
            }
             
            
            GameObject newRoom = Instantiate(room, takenPositions[i], Quaternion.identity);
            Debug.Log(takenPositions[i]);

            if(i < takenPositions.Count){
                if(next.x > current.x && next.y == current.y){
                    GameObject newDoor = Instantiate(door, new Vector2(current.x + 11, current.y), Quaternion.identity);
                    newDoor.transform.Rotate(0, 0, -90);
                    GameObject newDoor2 = Instantiate(door, new Vector2(next.x - 11, next.y), Quaternion.identity);
                    newDoor2.transform.Rotate(0, 0, 90);
                }
                if(next.y > current.y && next.x == current.x){
                    GameObject newDoor = Instantiate(door, new Vector2(current.x, current.y + 4.5f), Quaternion.identity);
                    newDoor.transform.Rotate(0, 0, 180);
                    GameObject newDoor2 = Instantiate(door, new Vector2(next.x, next.y - 4.5f), Quaternion.identity);
                    newDoor2.transform.Rotate(0, 0, 0);
                }
                if(next.x < current.x && next.y == current.y){
                    GameObject newDoor = Instantiate(door, new Vector2(current.x - 11, current.y), Quaternion.identity);
                    newDoor.transform.Rotate(0, 0, 90);
                    GameObject newDoor2 = Instantiate(door, new Vector2(next.x + 11, next.y), Quaternion.identity);
                    newDoor2.transform.Rotate(0, 0, -90);
                }
                if(next.y < current.y && next.x == current.x){
                    GameObject newDoor = Instantiate(door, new Vector2(current.x, current.y - 4.5f), Quaternion.identity);
                    GameObject newDoor2 = Instantiate(door, new Vector2(next.x, next.y + 4.5f), Quaternion.identity);
                    newDoor2.transform.Rotate(0, 0, 180);
                }
            }


        }
    }

    Vector2 NewCoordinates(Vector2 pos){
        int randomInt = Random.Range(0, 3);
        if(randomInt == 0){
            pos.x += xScaleMultiplier;
        }else if(randomInt == 1){
            pos.x -= xScaleMultiplier;
        }else if(randomInt == 2){
            pos.y += yScaleMultiplier;
        }else{
            pos.y -= yScaleMultiplier;
        }

        return pos;
    }

    void PlacePlayer(){
        player.transform.position = takenPositions[1];
        cam.transform.position = new Vector3(takenPositions[1].x, takenPositions[1].y, -1);
    }
}
