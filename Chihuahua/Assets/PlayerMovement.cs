using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    bool facingRight = true;
    public float moveX;
    public float moveY;

    public Animator anim;

    private void Update() {
        ProcessInputs();
    }

    void FixedUpdate(){
       Move();
    }

    void ProcessInputs(){
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        if(moveX != 0 || moveY != 0){
            anim.SetBool("Walk", true);
        }else{
            anim.SetBool("Walk", false);

        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            anim.SetBool("ThrowUp", true);
            anim.SetLayerWeight(1, 1);
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            anim.SetLayerWeight(1, 0);
            anim.SetBool("ThrowUp", false);
        }
        
        // if(Input.GetKey(KeyCode.DownArrow)){
        //     anim.SetBool("ThrowDown", true);
        //     anim.SetLayerWeight(1, 1);
        // }else if(Input.GetKey(KeyCode.LeftArrow)){
        //     if(moveX > 0){
        //         anim.SetBool("ThrowBack", true);
        //     }
        //     if(moveX < 0){
        //         anim.SetBool("ThrowForward", true);
        //     }
        //     anim.SetLayerWeight(1, 1);
        // }else if(Input.GetKey(KeyCode.RightArrow)){
        //     if(moveX > 0){
        //         anim.SetBool("ThrowForward", true);
        //     }
        //     if(moveX < 0){
        //         anim.SetBool("ThrowBack", true);
        //     }
        //     anim.SetLayerWeight(1, 1);
        // }
        // }else{
        //     
        //     anim.SetBool("ThrowDown", false);
        //     anim.SetBool("ThrowBack", false);
        //     anim.SetBool("ThrowForward", false);
        //     
        // }


        moveDirection = new Vector2(moveX, moveY).normalized;

        if(moveX > 0 && facingRight){
            Flip();
        }else if(moveX < 0 && !facingRight){
            Flip();
        }
    }

    void Move(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }   

    void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
