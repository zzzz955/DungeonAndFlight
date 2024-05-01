using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 3f;

    private GameObject Background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveTo = new Vector3(moveSpeed, 0f, 0f);
        transform.position -= moveTo * Time.deltaTime;
    }
}
