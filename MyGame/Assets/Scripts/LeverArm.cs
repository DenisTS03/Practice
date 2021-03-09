using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    [SerializeField] private int number;

    private Animator _animator;
    private Finish _finish;

    void Start() {
        _animator = GetComponent<Animator>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }
    public void ActivateLeverArm() {
        _animator.SetTrigger("activate");
        _finish.Activate(number);
    }
}
