using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public float waitTime;
    public int health;
    public int damage;
    public float animationTime_Hit;
    public GameObject BloodEffect;

    private Animator animator;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDame(int damage)
    {
        health -= damage;
        animator.SetBool("Hit", true);
        Invoke("ResetAnimator", animationTime_Hit);
        Instantiate(BloodEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
    }
    public void ResetAnimator()
    {
        animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}
