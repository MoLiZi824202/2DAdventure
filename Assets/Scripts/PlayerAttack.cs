using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    private Animator anim;
    private PolygonCollider2D skill1HitBox;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        skill1HitBox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Skill();
    }

    void Skill()
    {
        if (Input.GetButtonDown("Skill1"))
        {
            anim.SetTrigger("Skill1");
            StartCoroutine(startAttack());
        }
    }
    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(startTime);
        skill1HitBox.enabled = true;
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        skill1HitBox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDame(damage);
        }
    }
}
