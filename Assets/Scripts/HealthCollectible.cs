using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int amtToHeal = 1;
    public float collectionDelay = .5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(collectionDelay > 0)
        {
            collectionDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && collectionDelay <= 0)
        {
            CharacterOneHealthController.instance.HealPlayer(amtToHeal);

            Destroy(gameObject);
        }
    }
}
