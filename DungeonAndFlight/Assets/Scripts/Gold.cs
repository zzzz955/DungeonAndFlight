using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    private float minX = -20f;
    // Start is called before the first frame update
    void Start()
    {
        moveCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minX) {
            Destroy(gameObject);
        }
    }

    void moveCoin() {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
