using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private Transform enemyModelTransform;

    private Rigidbody2D _rb;
    private Vector2 _nextPoint;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;

    private bool _isFacingRight = true;
    private bool _isWait = false;

    private float _waitTime;
    private float _walkSpeed;
    
    public bool IsFacingRight {
        get => _isFacingRight;
    }

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _waitTime = timeToWait;
        _walkSpeed = patrolSpeed;
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance; // Vector2.right == new Vector2(1, 0)
    }

    private void Update() {
        if (_isWait) { 
            StartWaitTimer();
        }
       
        if (ShouldWait()) {
            _isWait = true;
        }
    }

    void FixedUpdate() { // rigitBody используется только здесь
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (!_isWait) {
            Patrol();
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
    
    private void Patrol() {
        if (!_isFacingRight) {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

     void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransform.localScale;
        playerScale.x *= -1;
        enemyModelTransform.localScale = playerScale;
    }
}