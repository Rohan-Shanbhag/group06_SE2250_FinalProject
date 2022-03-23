using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D rigidBody;

    public int damageToGive = 50;

    // Start is called before the first frame update
    void Start()
    {
      rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }

        // add boss damage dealt
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
