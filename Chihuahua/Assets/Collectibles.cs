using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{

    private AudioManager am;
    public TextMeshProUGUI coinsText;
    public GameObject coinParticles;

    private void Start() {
        am = FindObjectOfType<AudioManager>();
    }

    private void Update() {
        coinsText.text = Data.coins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Coin"){
            am.Play("Coin");
            Data.coins += 1;
            Instantiate(coinParticles, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
