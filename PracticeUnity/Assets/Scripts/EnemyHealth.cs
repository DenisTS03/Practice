using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;
    private Animator _animator;

    private float _health;

    private void Start() {
        _animator = GetComponent<Animator>();
        _health = totalHealth;
        UpdateHealthSlider();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        UpdateHealthSlider();
        _animator.SetTrigger("takeDamage");
        if (_health <= 0) {
            Die();
        }
    }

    private void UpdateHealthSlider() {
        healthSlider.value = _health / totalHealth;
    }

    private void Die() {
        gameObject.SetActive(false);
    }
}
