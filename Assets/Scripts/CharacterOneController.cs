using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOneController : MonoBehaviour
{
    public static CharacterOneController instance;

    [HideInInspector]
    public bool isAbleToMove = true;

    public Rigidbody2D rigidBody;
    public Transform gunHoldingArm;
    private Vector2 movingInput;
    public float movingSpeed;
    public SpriteRenderer bodySpriteRenderer;
    private float activeMovingSpeed;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        activeMovingSpeed = movingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAbleToMove)
        {
            movingInput.x = Input.GetAxisRaw("Horizontal");
            movingInput.y = Input.GetAxisRaw("Vertical");
            movingInput.Normalize();

            rigidBody.velocity = movingInput * activeMovingSpeed;

        } else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
}
