using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public int coinVal = 1;

    public float collectionDelay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collectionDelay > 0)
        {
            collectionDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && collectionDelay <= 0)
        {
            LevelAttributeManagement.instance.AccumulateCoins(coinVal);

            Destroy(gameObject);
        }
    }
}
