using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject collisionParticles;
    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(player.GetComponent<Player_Health>().TakeDamage());
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
