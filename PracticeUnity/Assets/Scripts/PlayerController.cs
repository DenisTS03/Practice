using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    float horizontal = 0f;
    
    private bool isGroud = false;
    private bool isJump = false;
    private bool isFacingRight = true;
    private bool isFinish = false;
    private bool isLeverArm = false;

    private Finish finish;
    private Rigidbody2D rb;
    private LeverArm leverArm;

    const float speedXMultiplier = 50f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        leverArm = FindObjectOfType<LeverArm>();
    }

    void Update() {
        horizontal = Input.GetAxis("Horizontal"); // -1; 1
        animator.SetFloat("speedX", Mathf.Abs(horizontal));
        if (Input.GetKeyDown(KeyCode.W) && isGroud) {
            isJump = true;
            jumpSound.Play();
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            if (isFinish) {
                finish.FinishLevel();
            }
            if (isLeverArm) {
                leverArm.ActivateLeverArm();
            }
        }
    }

    void FixedUpdate() { // rigitBody используется только здесь
        rb.velocity = new Vector2(horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, rb.velocity.y);
        if (isJump) {
            rb.AddForce(new Vector2(0f, 500f));
            isGroud = false;
            isJump = false;
        }

        if (horizontal > 0f && !isFacingRight) {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight){
            Flip();
        }
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGroud = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish")) {
            isFinish = true;
        }
        if (leverArmTemp != null) {
            isLeverArm = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish")) {
            isFinish = false;
        }
         if (leverArmTemp != null) {
            isLeverArm = false;
        }
    }
}
