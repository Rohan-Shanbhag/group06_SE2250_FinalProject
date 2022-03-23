using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float movingSpeed;

    [Header("Variables")]
    public SpriteRenderer bodySpriteRenderer;

    [Header("Follow")]
    public bool willFollowCharacter;
    public float proximityToFollowCharacter;
    private Vector3 directionOfMovement;

    [Header("Move in Opposite Direction")]
    public bool willMoveAwayFromCharacter;
    public float proximityToMoveAway;

    [Header("Projectile")]
    public bool willShootProjectile;
    public float proximityToShoot;
    public GameObject projectile;
    public Transform firePoint;
    public float rateOfFire;
    private float fireCounter;

    public int health = 150;
    public bool willGiveItem;
    public GameObject[] collectibles;
    public float collectibleDropPct;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterOneController.instance.gameObject.activeInHierarchy && bodySpriteRenderer.isVisible)
        {
            directionOfMovement = Vector3.zero;

            if (Vector3.Distance(transform.position, CharacterOneController.instance.transform.position) < proximityToFollowCharacter && willFollowCharacter)
            {
                directionOfMovement = CharacterOneController.instance.transform.position - transform.position;
            }

            if(willMoveAwayFromCharacter && Vector3.Distance(transform.position, CharacterOneController.instance.transform.position) < proximityToMoveAway)
            {
                directionOfMovement = transform.position - CharacterOneController.instance.transform.position;
            }

            directionOfMovement.Normalize();

            rigidBody.velocity = directionOfMovement * movingSpeed;


            if (willShootProjectile && Vector3.Distance(transform.position, CharacterOneController.instance.transform.position) < proximityToShoot)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = rateOfFire;
                    Instantiate(projectile, firePoint.position, firePoint.rotation);
                }
            }


        } else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);

            //drop items
            if (willGiveItem)
            {
                float rngChance = Random.Range(0f, 100f);

                if (rngChance < collectibleDropPct)
                {
                    int randomItem = Random.Range(0, collectibles.Length);

                    Instantiate(collectibles[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
