using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public int numOfHearts;
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    public float rechargeTimer;
    bool canTakeDamage = true;



    // Update is called once per frame
    void Update(){
        if(health > numOfHearts){
            health = numOfHearts;
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
        yield return new WaitForSeconds(rechargeTimer);
        canTakeDamage = true;
    }
}
