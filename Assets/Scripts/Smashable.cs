using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    public GameObject[] crateBrokenParts;

    public bool willDropCollectible;
    public GameObject[] collectibles;
    public float collectibleDropPct;

    public void Break()
    {
        Destroy(gameObject);

        for (int i = 0; i < 1; i++)
        {
            int randomPiece = Random.Range(0, crateBrokenParts.Length);

            Instantiate(crateBrokenParts[randomPiece], transform.position, transform.rotation);
        }

        //drop items
        if (willDropCollectible)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < collectibleDropPct)
            {
                int randomItem = Random.Range(0, collectibles.Length);

                Instantiate(collectibles[randomItem], transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (CharacterOneController.instance.dashingTimer > 0)
            {
                Break();
            }
        }

        if (other.tag == "PlayerBullet")
        {
            Break();
        }

        if (other.tag == "Sword")
        {
            Break();
        }
    }
}
