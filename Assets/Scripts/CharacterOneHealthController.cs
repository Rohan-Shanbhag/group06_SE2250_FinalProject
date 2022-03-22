using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOneHealthController : MonoBehaviour
{
    public static CharacterOneHealthController instance;
    public int maxHealth;
    public int currHealth;
    private float invincibilityMetric;
    public float invincibilityLengthDamage = 1f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityMetric > 0)
        {
            invincibilityMetric -= Time.deltaTime;

            if(invincibilityMetric <= 0)
            {
                CharacterOneController.instance.bodySpriteRenderer.color = new Color(CharacterOneController.instance.bodySpriteRenderer.color.r, CharacterOneController.instance.bodySpriteRenderer.color.g, CharacterOneController.instance.bodySpriteRenderer.color.b, 1f);

            }
        }
    }

    public void DamagePlayer()
    {
        if (invincibilityMetric <= 0)
        {
            currHealth--;
            invincibilityMetric = invincibilityLengthDamage;
            CharacterOneController.instance.bodySpriteRenderer.color = new Color(CharacterOneController.instance.bodySpriteRenderer.color.r, CharacterOneController.instance.bodySpriteRenderer.color.g, CharacterOneController.instance.bodySpriteRenderer.color.b, .5f);

            if (currHealth <= 0)
            {
                CharacterOneController.instance.gameObject.SetActive(false);

            }
        }
    }

    public void MomentaryInvincibility(float length)
    {
        invincibilityMetric = length;
    }

    public void HealPlayer(int healAmount)
    {
        currHealth += healAmount;
        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currHealth = maxHealth;
    }
}
