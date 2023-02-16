using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cosmetics : MonoBehaviour
{
    public GameObject blood;


    public GameObject sombreroObjectToActivate;
    public GameObject[] sombreroObjectsToDeactivate;
    public GameObject pickupParticles;

    private AudioManager am;

    private void Start() {
        am = FindObjectOfType<AudioManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Data.enemiesKilled > 5){
            blood.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Sombrero"){
            EquipCosmetic("Sombrero", sombreroObjectToActivate, sombreroObjectsToDeactivate, other.gameObject);
        }
    }

    void EquipCosmetic(string sound, GameObject objToActivate, GameObject[] objDeactivate, GameObject other){
        am.Play(sound);
        Instantiate(pickupParticles, transform.position, Quaternion.identity);
        if(objToActivate != null){
            objToActivate.SetActive(true);
        }
        if(objDeactivate.Length > 0){
            foreach (var obj in objDeactivate){
                obj.SetActive(false);
            }
        }
        Destroy(other.gameObject);
    }
}
