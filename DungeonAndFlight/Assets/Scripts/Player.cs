using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2f;
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private Transform shootTransform;

    public int playerHp;

    private float lastShotTime = 0f;

    private float[] delay = new float[] {1.0f, 0.9f, 0.8f, 0.7f, 0.6f};

    public int delayIndex = 0;

    private AnimationController Upgrade;
    
    private GameManager gameManager;

    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        Upgrade = GetComponent<AnimationController>();
        playerHp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
        if (Upgrade.isUpgrade) {
            moveSpeed = 5f;
        }

        Shoot();

        if (playerHp <= 0) {
            GameManager.instance.EndGame();
            Destroy(gameObject);
        }
    }

    void Shoot() {
        if (Time.time > lastShotTime) {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            cnt += 1;
            if (cnt < 3) {
                lastShotTime = Time.time + (0.5f * delay[delayIndex]);
            } else if (cnt == 3) {
                lastShotTime = Time.time + (1.0f * delay[delayIndex]);
            } else {
                lastShotTime = Time.time + (1.5f * delay[delayIndex]);
                cnt = 0;
            }
        }
    }
}
