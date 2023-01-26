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

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetBool("ThrowForward", true);
            anim.SetLayerWeight(1, 1);
        }else{
            anim.SetBool("ThrowForward", false);
            anim.SetLayerWeight(1, 0);
        }


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
