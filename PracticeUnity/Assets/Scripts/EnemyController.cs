using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private Transform enemyModelTransform;

    private Rigidbody2D _rb;
    private Vector2 _nextPoint;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Transform _playerTransform;

    private bool _isChasingPlayer;
    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _colliderWithPlayer;

    private float _chaseTime;
    private float _waitTime;
    private float _walkSpeed;
    
    public bool IsFacingRight {
        get => _isFacingRight;
    }

    public void StartChasingPlayer() {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }
    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance; // Vector2.right == new Vector2(1, 0)
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }

    private void Update() {
        if (_isChasingPlayer) {
            StartChasingTimer();
        }

        if (_isWait && !_isChasingPlayer) { 
            StartWaitTimer();
        }
       
        if (ShouldWait()) {
            _isWait = true;
        }
    }

    private bool ShouldWait() {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;
        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }

    private void StartWaitTimer() {
        _waitTime -= Time.deltaTime;
        if (_waitTime <= 0f) {
             _isWait = false;
             _waitTime = timeToWait;
              Flip();
        }
    }

   void FixedUpdate() { // rigitBody используется только здесь
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;
        if (_isChasingPlayer && _colliderWithPlayer) {
            return;
        }
        if (_isChasingPlayer) {
            ChasePlayer();
        }

        if (!_isWait && !_isChasingPlayer) {
            Patrol();
        }   
    }
    private void Patrol() {
        if (!_isFacingRight) {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private float DistanceToPlayer() {
        return _playerTransform.position.x - transform.position.x;
    }
    private void StartChasingTimer() {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime <= 0f) {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
        }
    }
    private void ChasePlayer() {
        float distance = DistanceToPlayer();
        Debug.Log(distance);
        if (distance < 0) {
            _nextPoint.x *= -1;
        }
        if (distance > 0.2f && !_isFacingRight) {
            Flip();
        }
        else if (distance < 0.2f && _isFacingRight) {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

     void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransform.localScale;
        playerScale.x *= -1;
        enemyModelTransform.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null) {
            _colliderWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null) {
            _colliderWithPlayer = false;
        }
    }
}