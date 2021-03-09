using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Animator animator;

    public void Die() {
        animator.SetTrigger("die");
        gameOverCanvas.SetActive(true);
    }
}
