using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f; //move speed

    private PlayerControls controls; //controls
    private Vector2 movement; //moving or not
    private Rigidbody2D rb; //rigidbody
    private Animator myAnimator; //animator for the playuer moving
    private SpriteRenderer mySpriteRenderer; //sprite update

    public Vector2 playerPos => rb.position;

    private void Awake() { //first line ran
        controls = new PlayerControls(); //get playercontroles
        rb = GetComponent<Rigidbody2D>(); //get the rigidbody
        myAnimator = GetComponent<Animator>(); //get idle animation
        mySpriteRenderer = GetComponent<SpriteRenderer>(); //get sprite renderer for that
    }

    private void OnEnable() {
        controls.Enable(); //enable controls
    }

    private void OnDisable() {
        controls.Disable();
    }

    private void PlayerInput() {
        movement = controls.Player.Move.ReadValue<Vector2>(); //reads the movement
        myAnimator.SetFloat("moveX", movement.x); //gets the params for the animation
        myAnimator.SetFloat("moveY", movement.y); //gets the params for the animation
    }

    private void Update() {
        PlayerInput(); //updates the movement
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); //moves character
    }

    private void FixedUpdate() { //called 50 times a second
        AdjustPlayerFacingDirection(); //changes direction for sprite
        Move(); 
    }

    private void AdjustPlayerFacingDirection() {
        mySpriteRenderer.flipX = movement.x < 0;
    }

   public int getPlayerXDirection() {
        
        if (movement.x > 0) {
            return 1; //right
        } 
        
        else if (movement.x < 0) {
            return -1; //left
        } 
        
        else {
            return 0; //no left/right
        }
    }

    public int getPlayerYDirection() {
        
        if (movement.y > 0) {
            return 1; //up
        } 
        
        else if (movement.y < 0) {
            return -1; //down
        } 
        
        else {
            return 0; //no up or down
        }
    }
}
