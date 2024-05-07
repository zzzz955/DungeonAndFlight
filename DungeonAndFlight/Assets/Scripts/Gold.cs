using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    private float minX = -20f;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        // moveCoin();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.y < minX) {
            Destroy(gameObject);
        }
    }

    public void SetGoldeAmount(int goldAmount) {
        int minGold = Mathf.RoundToInt(goldAmount * 0.8f);
        int maxGold = Mathf.RoundToInt(goldAmount * 1.2f);
        amount = Random.Range(minGold, maxGold);
    }

    // void moveCoin() {
    //     transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    // }
}
