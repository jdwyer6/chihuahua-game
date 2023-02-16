using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool open;
    public GameObject[] openDoors;
    public GameObject[] closedDoors;
    private AudioManager am;

    bool openSoundPlayed = false;
    bool closeSoundPlayed = false;

    private void Start() {
        am = FindObjectOfType<AudioManager>();
        open = true;
    }

    private void Update() {

    }

    public void OpenDoors(){
        if(openSoundPlayed == false){
            am.Play("DoorOpen");
            openSoundPlayed = true;
            closeSoundPlayed = false;
        }
        
        foreach (var door in openDoors){
            door.SetActive(true);
        }
        foreach (var door in closedDoors){
            door.SetActive(false);
        }
    }

    public void CloseDoors(){
        if(closeSoundPlayed == false){
            am.Play("DoorClose");
            closeSoundPlayed = true;
            openSoundPlayed = false;
        }
        foreach (var door in openDoors){
            door.SetActive(false);
        }
        foreach (var door in closedDoors){
            door.SetActive(true);
        }
    }


}
