using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    Vector2 input;
    private Rigidbody2D player; //Players collider object

    Animator anim;
	AudioSource moveAudio;

    public float moveSpeed = 5;
    public float speed = 0;
    public float diagSpeed = 0.7f;
	public bool isMoving = false;
    

    public bool isAllowedToMove = false;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAllowedToMove = true;
		moveAudio = GetComponent<AudioSource> ();
    }

    void Update(){

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

		if(isMoving && !moveAudio.isPlaying)	{
			moveAudio.Play ();
		}
		else if(!isMoving){
			moveAudio.Stop ();
		} 


        if (new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")) != Vector2.zero)
        {
			isMoving = true;
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", input.x);
            anim.SetFloat("input_y", input.y);
        }
        else {
			isMoving = false;
            anim.SetBool("isWalking", false);
        }

        if (input.x != 0 && input.y != 0)
        {
            speed = diagSpeed * moveSpeed;
        }
        else {
            speed = moveSpeed;
        }

        player.velocity = input * speed;
    }


}