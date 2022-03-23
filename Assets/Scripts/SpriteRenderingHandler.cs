using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderingHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = Mathf.RoundToInt( transform.position.y * -10f);
    }
}
