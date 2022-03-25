using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Vector3 direction;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementSpeed * direction * Time.deltaTime;

        if(!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterOneHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
