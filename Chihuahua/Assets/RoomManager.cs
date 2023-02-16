using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Doors doors;
    public bool roomComplete = false;
    public bool playerHasEnteredBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        doors = gameObject.transform.Find("Doors").GetComponent<Doors>();

    }

    // Update is called once per frame
    void Update()
    {
           if(roomComplete == true){
                doors.OpenDoors();
           }

           

        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            if(playerHasEnteredBefore == false && roomComplete == false && (GetComponent<RoomSpawner>().levelType == "enemy" || GetComponent<RoomSpawner>().levelType == "boss") && GetComponent<RoomSpawner>().isHome == false){
                StartCoroutine(DelayedClose());
                playerHasEnteredBefore = true;
           }
        }
    }


    IEnumerator DelayedClose(){
        yield return new WaitForSeconds(.5f);
        doors.CloseDoors();
    }
}
