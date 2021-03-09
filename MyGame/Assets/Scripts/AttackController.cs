﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource attackSound;
    private bool _isAttack;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) { // 0 - ЛКМ, 1 - ПКМ
            _isAttack = true;
            attackSound.Play();
            animator.SetTrigger("attack");
        }
    }

    public void FinishAttack() {
        _isAttack = false;
        attackSound.Play();
    }
    public bool IsAttack {
        get => _isAttack;
    }
}
