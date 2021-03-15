using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;

    private float damageTimeout = 1f;
    private bool canTakeDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTakeDamage && collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            StartCoroutine(damageTimer());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <=0)
        {
            Score.instance.AddScore();
            Destroy(gameObject);
        }
    }
    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }
}
