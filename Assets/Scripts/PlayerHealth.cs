using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int blinkNumber;
    public float blinkTime;

    private Animator playerAnimator;
    private Renderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamagePlayer(int damage)
    {
        health -= damage;
        //playerAnimator.SetTrigger("Hurt");
        if (health <= 0)
        {
            playerAnimator.SetTrigger("Die");
            Destroy(gameObject,1f);
        }
        BlinkPlayer(blinkNumber, blinkTime);
    }

    void BlinkPlayer(int numberBlink,float second)
    {
        StartCoroutine(DoBlink(numberBlink, second));
    }

    IEnumerator DoBlink(int numberBlink,float second)
    {
        for (int i = 0; i < numberBlink*2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(second);
        }
        myRenderer.enabled = true;
    }
}
