using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    [Header("Variables")]
    public SpriteRenderer bodySpriteRenderer;

    public int health = 150;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = Vector2.zero;
    }

}
