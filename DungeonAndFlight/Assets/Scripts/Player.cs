using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(0f, verticalInput, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
    }
}
