using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public int numOfHearts;
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator anim;
    private AudioManager am;

    public SpriteRenderer spriteRenderer;


    public float rechargeTimer;
    bool canTakeDamage = true;

    private void Start() {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update(){
        if(health > numOfHearts){
            health = numOfHearts;
        }

        if(health <= 0){
            StartCoroutine(Die());
        }
        
        for (int i = 0; i < hearts.Length; i++){
            if(i<health){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }
        }

        for (int i = 0; i < hearts.Length; i++){
            if(i< numOfHearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            if(canTakeDamage == true){
                StartCoroutine(TakeDamage());
            }
            
        }
    }


    IEnumerator TakeDamage(){
        canTakeDamage = false;
        health -= 1;
        anim.SetTrigger("Hurt");
        am.Play("Hurt");
        am.Play("PlayerIsHit");
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(rechargeTimer);
        spriteRenderer.color = Color.white;
        canTakeDamage = true;
    }

    IEnumerator Die(){

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
