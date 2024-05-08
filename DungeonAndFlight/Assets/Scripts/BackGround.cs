using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;
    private float stopPositionX;
    private float spwanPositionX;
    private bool isMoving = true;
    private Camera mainCamera;
    private float initialCameraHeight;
    private Vector3 initialScales;

    void Awake() {
        mainCamera = Camera.main;
        initialCameraHeight = 2f * mainCamera.orthographicSize;
    }

    void Start()
    {
        initialScales = transform.localScale;
        ScaleBackground();        
        transform.position = new Vector3(spwanPositionX, transform.position.y, transform.position.z);        
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

    void ScaleBackground()
    {
        // 배경 객체의 SpriteRenderer 컴포넌트 가져오기
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // 현재 배경의 스케일 계산
            float currentHeight = spriteRenderer.bounds.size.y;
            float scaleRatio = initialCameraHeight / currentHeight;

            // 배경의 스케일 조정
            transform.localScale = new Vector3(scaleRatio, scaleRatio, 1f);

            // 스케일 조정 후의 배경의 너비와 높이 계산
            float scaledWidth = spriteRenderer.bounds.size.x;

            stopPositionX = -((scaledWidth / 2f) - 9f);
            spwanPositionX = scaledWidth / 2f - 9f;
        }
    }    
}
