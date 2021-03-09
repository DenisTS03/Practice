using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    float _horizontal = 0f;
    
    private bool _isGroud = false;
    private bool _isWall = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;

    private Rigidbody2D rb;
    private PlayerHealth playerHealth;

    const float speedXMultiplier = 50f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update() {
        _horizontal = Input.GetAxis("Horizontal"); // -1; 1
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.W) && _isGroud) {
            _isJump = true;
            jumpSound.Play();
        }
    }

    void FixedUpdate() { // rigitBody используется только здесь
        if (!_isWall || _isGroud) {
            rb.velocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, rb.velocity.y);
        }
        if (_isJump) {
            rb.AddForce(new Vector2(0f, 350f));         
            _isGroud = false;
            _isJump = false;
        }

        if (_horizontal > 0f && !_isFacingRight) {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight){
            Flip();
        }
    }

    void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _isGroud = true;
        }
        if (other.gameObject.CompareTag("Wall")) {
            _isWall = true;
        }
        if (other.gameObject.CompareTag("Magma")) {
            playerHealth.Die();
        }
    }

    void OnCollisionExit2D (Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            _isWall = false;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        Finish finish = other.GetComponent<Finish>();
        if (other.CompareTag("Finish")) {
            finish.FinishLevel();
        }
        if (leverArmTemp != null) {
            leverArmTemp.ActivateLeverArm();
        }
    }
}
