using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private float startingSpeed;
    float distance;

    AudioManager am;
    public Animator anim;
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    public float damageTimeStart = .3f;
    public bool canMove = true;
    public int health;

    public GameObject enemyHitParticles;
    public GameObject boneParticles;
    public GameObject bloodSplat;

    public bool fullSpeed = true;
    public bool facingRight = true;
    bool movingRight = true;

    float timer;
    public float maxTimeToAttack;
    public GameObject enemyProjectile;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        am = FindObjectOfType<AudioManager>();
        startingSpeed = speed;
        timer = Random.Range(0, maxTimeToAttack);
    }

    void Update()
    {
        
        if(health <= 0){
            am.Play("Enemy_Death_0");
            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            Instantiate(enemyHitParticles, transform.position, Quaternion.identity);
            Data.enemiesKilled += 1;
            Destroy(gameObject);
        }

        if(canMove == false){
            speed = 0;
        }else{
            speed = startingSpeed;
        }
        
        timer -= Time.deltaTime;

        if(movingRight && !facingRight){
            Flip();
        }else if(!movingRight && facingRight){
            Flip();
        }

        StartCoroutine(CheckDirection());

    }

    private void FixedUpdate() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        
        if(timer <= 0){
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bone"){
            health -= 1;
            am.Play("Hit");
            StartCoroutine(DamagePause(Color.red));
            Instantiate(boneParticles, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Player"){
            Vector2 dir = transform.position - other.gameObject.transform.position;
            rb.AddForce(dir * 1, ForceMode2D.Impulse);
            StartCoroutine(DamagePause(Color.gray));
        }

    }

    IEnumerator DamagePause(Color colorToChange){
        canMove = false;
        spriteRenderer.color = colorToChange;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;
        canMove = true;
    }

    void Attack(){
        timer = Random.Range(0, maxTimeToAttack);
        anim.SetTrigger("Attack");
        GameObject newProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        // Vector2 dir = player.transform.position;
        newProjectile.GetComponent<Rigidbody2D>().velocity = (player.transform.position - newProjectile.transform.position).normalized * projectileSpeed;
        am.Play("EnemyProjectile");
    }

    void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator CheckDirection(){
        float tempDirection = transform.position.x;
        yield return new WaitForSeconds(.1f);
        if(tempDirection < transform.position.x){
            movingRight = true;
        }else{
            movingRight = false;
        }

    }
}
