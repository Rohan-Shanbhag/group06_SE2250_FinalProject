using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMysteryBox : MonoBehaviour
{
    public GunCollectible[] probableWeaponCollectibles;

    public SpriteRenderer mysteryBoxSprite;
    public Sprite openSprite;

    public GameObject onScreenDirective;

    private bool boxIsClosed, boxIsOpened;

    public Transform spawnPoint;

    public float scaleSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(boxIsClosed && !boxIsOpened)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                int gunSelect = Random.Range(0, probableWeaponCollectibles.Length);
                Instantiate(probableWeaponCollectibles[gunSelect], spawnPoint.position, spawnPoint.rotation);
                mysteryBoxSprite.sprite = openSprite;
                boxIsOpened = true;
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }

        if(boxIsOpened)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            onScreenDirective.SetActive(true);
            boxIsClosed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            onScreenDirective.SetActive(false);
            boxIsClosed = false;
        }
    }
}
