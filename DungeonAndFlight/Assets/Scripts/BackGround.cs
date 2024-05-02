using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    private float stopPositionX;
    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 spriteSize = spriteRenderer.bounds.size;

        stopPositionX = -((spriteSize.x / 2f) - 9f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            Vector3 moveTo = new Vector3(moveSpeed, 0f, 0f);
            transform.position -= moveTo * Time.deltaTime;
            if (transform.position.x <= stopPositionX) {
                isMoving = false;
            }
        }
    }
}
