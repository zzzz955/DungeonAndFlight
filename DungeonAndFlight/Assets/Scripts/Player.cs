using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // AnimationController animation = new AnimationController();
        
        // // 첫 번째 애니메이션을 재생합니다.
        // animation.PlayAnimation("Run");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
    }
}
