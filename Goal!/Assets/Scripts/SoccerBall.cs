using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoccerBall : MonoBehaviour {

    private Rigidbody2D ballrb;
    public PlayerControls controls; //the controls
    private Animator myAnimator;
    private PlayerController playerController;
    public bool dribbling = false;

    public Vector2 ballPos => ballrb.position;


    private void Awake() { //first function called
        
        controls = new PlayerControls(); //get player controls
        ballrb = GetComponent<Rigidbody2D>();
        ballrb.gravityScale = 0; // Set gravity scale to 0 to prevent falling
        myAnimator = GetComponent<Animator>();
        controls.Player.Kick.performed += ctx => Kick(); //not using ctx (context) (just adding shoot method to this list)
        controls.Player.Dribble.performed += ctx => toggleDribble();
    }

    private void Start() {
        playerController = FindObjectOfType<PlayerController>(); //select the playercontoller
    }

    private void Update() { // Update is called once per frame
        Vector2 velocity = ballrb.velocity;
        myAnimator.SetFloat("velocityX", velocity.x);
        myAnimator.SetFloat("velocityY", velocity.y);
        PlayerProximity(); // Check player proximity
       
    }

    private void FixedUpdate() { //called 50 times a second
        if (dribbling == true) {
            Dribble();
        }
    }

    private float PlayerProximity() {
        float distance = Vector2.Distance(playerController.playerPos, ballPos); //distance between ball and player
        return distance;
    }

    private void Kick() {
        if (PlayerProximity() < 1 || dribbling == true) {
            Vector2 kickDirection = new Vector2(playerController.getPlayerXDirection(), playerController.getPlayerYDirection());
            ballrb.AddForce(kickDirection * 5f, ForceMode2D.Impulse); }
        dribbling = false;
    }

    private void Dribble() {
        
        if (PlayerProximity() < 2) {
            
            float dribbleX = 0;
            float dribbleY = -1f;
            
            if (playerController.getPlayerXDirection() == -1) {
                dribbleX = -0.53f;
                dribbleY = -0.6f;
            }
            if (playerController.getPlayerXDirection() == 1) {
                dribbleX = 0.53f;
                dribbleY = -0.6f;
            }

            if ((playerController.getPlayerYDirection() == -1 || playerController.getPlayerYDirection() == 1) && playerController.getPlayerXDirection() == 0) {
                dribbleY = 0.9f * playerController.getPlayerYDirection();
            }

            Vector2 dribbleDirection = new Vector2(dribbleX, dribbleY);
            Vector2 newBallPosition = playerController.playerPos + dribbleDirection;
            ballrb.position = newBallPosition; // Move the ball smoothly
        }
    }


    private void toggleDribble() {
        dribbling = !dribbling; // Toggle the dribbling state
    }

    private void OnEnable() {
        controls.Enable(); //enable controls
    }

    private void OnDisable() {
        controls.Disable();
    }
}
