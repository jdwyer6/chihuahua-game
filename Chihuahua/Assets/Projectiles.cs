using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject projectile;
    public float distance;
    public float velocity;
    public float timeBetweenShots;
    float timer;
    bool canShoot = true;
    AudioManager am;

    string[] throwSounds = new string[] {"Throw0", "Throw1", "Throw2"};

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenShots;
        am = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update(){

        timer -= Time.deltaTime;

        if(Input.GetKey(KeyCode.UpArrow) && canShoot == true){
            Throw(new Vector2(0, velocity));
        }else if(Input.GetKey(KeyCode.DownArrow) && canShoot == true){
            Throw(new Vector2(0, -velocity));
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && canShoot == true){
            Throw(new Vector2(-velocity, 0));
        }
        else if(Input.GetKey(KeyCode.RightArrow) && canShoot == true){
            Throw(new Vector2(velocity, 0));
        }

        if(timer > 0){
            canShoot = false;
        }
        if(timer <= 0){
            canShoot = true;
        }
    }

    void Throw(Vector2 dir){
        timer = timeBetweenShots;  
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = dir;
        var sound = throwSounds[Random.Range(0, throwSounds.Length)];
        am.Play(sound);
    }

}
