using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float slowSpeed;
    float distance;

    AudioManager am;

    float timer;
    public float stamina;
    public int health;

    public GameObject enemyHitParticles;
    public GameObject bloodSplat;

    public bool fullSpeed = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = stamina;
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            timer = stamina;
            fullSpeed = !fullSpeed;
        }

        if(health <= 0){
            am.Play("Enemy_Death_0");
            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void FixedUpdate() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bone"){
            health -= 1;
            Instantiate(enemyHitParticles, other.transform.position, Quaternion.identity);
            am.Play("Hit");
            Destroy(other.gameObject);
        }
    }
}
