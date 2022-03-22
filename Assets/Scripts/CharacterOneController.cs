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

    public float dashingSpeed = 8f, dashingLength = .5f, dashingInaccessible = 1f, dashingGodMode = .5f;

    [HideInInspector]
    public float dashingTimer;
    private float dashingInaccessibleTimer;

    // Implement gun functionalities
    public List<Gun> heldWeapons = new List<Gun>();
    [HideInInspector]
    public int equippedWeapon;

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

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunHoldingArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunHoldingArm.localScale = Vector3.one;
            }

            // rotate arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunHoldingArm.rotation = Quaternion.Euler(0, 0, angle);

            // switch guns
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if(heldWeapons.Count > 0)
                {
                    equippedWeapon++;
                    if(equippedWeapon >= heldWeapons.Count)
                    {
                        equippedWeapon = 0;
                    }

                    ChangeWeapon();

                } else
                {
                    Debug.LogError("Player has no guns!");
                }
            }

            // dash movement
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashingTimer <= 0 && dashingInaccessibleTimer <= 0)
                {
                    activeMovingSpeed = dashingSpeed;
                    dashingTimer = dashingLength;
                    CharacterOneHealthController.instance.MomentaryInvincibility(dashingGodMode);
                }
            }

            if (dashingTimer > 0)
            {
                dashingTimer -= Time.deltaTime;
                if (dashingTimer <= 0)
                {
                    activeMovingSpeed = movingSpeed;
                    dashingInaccessibleTimer = dashingInaccessible;
                }
            }

            if (dashingInaccessibleTimer > 0)
            {
                dashingInaccessibleTimer -= Time.deltaTime;
            }
        } else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    public void ChangeWeapon()
    {
        foreach(Gun weapon in heldWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
        heldWeapons[equippedWeapon].gameObject.SetActive(true);
    }
}
