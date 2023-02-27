using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float linearDrag;
    private float horizontalDirection;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallMultiplier;
    private Vector2 vecGravity;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airLinearDrag;
    public bool isJumping;
    public bool isRunning;
    public bool isSliding;
    public bool isFalling;
    private float horizontalAxis;
    private bool facingRight = true;
    private float fJumpPressedRemember;
    private float fJumpPressedRememberTime = 0.25f;
    private float fGroundedRemember;
    private float fGroundedRememberTime = 0.1f;
    private float jumpCounter;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    private void Update() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if(rb.velocity.y < 0){
            isFalling = true;
        }

        if(isGrounded()){
            isFalling = false;
        }

        if(horizontalAxis > 0 || horizontalAxis < 0){
            isRunning = true;
        }else{
            isRunning = false;
        }
        
        if(horizontalAxis < 0 && facingRight){
            flip();
        }else if(horizontalAxis > 0 && !facingRight){
            flip();
        }

        fGroundedRemember  -= Time.deltaTime;
        if(isGrounded()){
            fGroundedRemember = fGroundedRememberTime;
            ApplyLinearDrag();
        }else{
            ApplyAirLinearDrag();
        }

        fJumpPressedRemember -= Time.deltaTime;
        if(Input.GetButtonDown("Jump")){
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if((fJumpPressedRemember > 0) && (fGroundedRemember > 0)) {
            isJumping = true;
            jumpCounter = 0;
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            Jump();
        }

        if(rb.velocity.y >  0 && isJumping){
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime){
                isJumping = false;
            }
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if(t > 0.5f){
                currentJumpM = jumpMultiplier * (1 - t);
            }
            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if(Input.GetButtonUp("Jump")){
            isJumping = false;
            jumpCounter = 0;
            if(rb.velocity.y > 0){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.6f);
            }
        }

        if(rb.velocity.y < 0){
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        horizontalDirection = GetDirectionInput().x;
        if(isRunning){
            MoveCharacter();
        }
        ApplyLinearDrag();
    }

    private Vector2 GetDirectionInput(){
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter(){
        rb.AddForce(new Vector2(horizontalDirection, 0) * movementAcceleration);
        if(Mathf.Abs(rb.velocity.x) > maxMovementSpeed){
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMovementSpeed, rb.velocity.y);
        }

    }

    private void ApplyLinearDrag(){
        if((Mathf.Abs(horizontalDirection) < 0.4) || changingDirection){
            rb.drag = linearDrag;
        }else{
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag(){
        rb.drag = airLinearDrag;
    }

    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        if(isRunning){
            rb.AddForce(Vector2.up * jumpForce * 0.4f, ForceMode2D.Impulse);
        }else{
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        AudioManager.Instance.PlaySFX("Jump");
    }

    public bool isGrounded(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.5f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
