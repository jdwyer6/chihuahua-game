using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float lifeTime;
    float timer;

    public GameObject destructionParticles;


    private void Start() {
        timer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());

        if(timer < 0){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Wall"){
            Instantiate(destructionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
