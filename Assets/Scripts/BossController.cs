using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossMove[] moves;
    private int currMove;
    private float moveTracker;

    public BossSequence[] bossSets;
    public int currBossSet;

    private float shotTracker;
    private Vector2 moveDir;
    public Rigidbody2D rigidBody;

    public int currHealth;

    public GameObject splatterEffect;
    public GameObject levelEndMechanism;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        moves = bossSets[currBossSet].moves;
        moveTracker = moves[currMove].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTracker > 0)
        {
            moveTracker -= Time.deltaTime;

            //handle movement
            moveDir = Vector2.zero;

            if(moves[currMove].shouldMove)
            {
                if(moves[currMove].shouldChasePlayer)
                {
                    moveDir = CharacterOneController.instance.transform.position - transform.position;
                    moveDir.Normalize();
                }

                if(moves[currMove].moveToPoint && Vector3.Distance(transform.position, moves[currMove].pointToMoveTo.position) > .5f)
                {
                    moveDir = moves[currMove].pointToMoveTo.position - transform.position;
                    moveDir.Normalize();
                }
            }

            rigidBody.velocity = moveDir * moves[currMove].moveSpeed;

            //handle shooting
            if(moves[currMove].shouldShoot)
            {
                shotTracker -= Time.deltaTime;
                if(shotTracker <= 0)
                {
                    shotTracker = moves[currMove].timeBetweenShots;

                    foreach(Transform t in moves[currMove].shotPoints)
                    {
                        Instantiate(moves[currMove].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        } else
        {
            currMove++;
            if(currMove >= moves.Length)
            {
                currMove = 0;
            }

            moveTracker = moves[currMove].actionLength;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;

        if (currHealth <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(splatterEffect, transform.position, transform.rotation);

            if (Vector3.Distance(CharacterOneController.instance.transform.position, levelEndMechanism.transform.position) < 2f)
            {
                levelEndMechanism.transform.position += new Vector3(4f, 0f, 0f);
            }

            levelEndMechanism.SetActive(true);
        }
        else
        {
            if(currHealth <= bossSets[currBossSet].endSequenceHealth && currBossSet < bossSets.Length - 1)
            {
                currBossSet++;
                moves = bossSets[currBossSet].moves;
                currMove = 0;
                moveTracker = moves[currMove].actionLength;
            }
        }
    }
}

[System.Serializable]
public class BossMove
{
    [Header("Boss Moves")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;


}

[System.Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossMove[] moves;

    public int endSequenceHealth;
}
