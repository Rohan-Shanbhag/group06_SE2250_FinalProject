using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform trgt;
    public Camera mainCamera;
    public float movingSpeed;

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
        if (trgt != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(trgt.position.x, trgt.position.y, transform.position.z), movingSpeed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform newTrgt)
    {
        trgt = newTrgt;
    }

}
