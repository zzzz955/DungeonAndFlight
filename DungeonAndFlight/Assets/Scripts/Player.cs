using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2f;
    [SerializeField]
    private GameObject[] weapon;
    
    public int weaponIndex;

    [SerializeField]
    private Transform shootTransform;

    public int ownGold;

    public int playerHp;

    private float lastShotTime = 0f;

    private float delay = 1f;

    private AnimationController Upgrade;
    
    private GameManager gameManager;

    private int cnt;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = 10;
        weaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        Shoot();

        if (playerHp <= 0) {
            GameManager.instance.EndGame();
            Destroy(gameObject);
        }
    }

    void Shoot() {
        if (Time.time > lastShotTime) {
            Instantiate(weapon[weaponIndex], shootTransform.position, Quaternion.identity);
            cnt += 1;
            if (cnt < 3) {
                lastShotTime = Time.time + (0.5f * delay);
            } else if (cnt == 3) {
                lastShotTime = Time.time + (1.0f * delay);
            } else {
                lastShotTime = Time.time + (1.5f * delay);
                cnt = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Gold") {
            GameManager.instance.IncreseCoin();
            Destroy(other.gameObject);
        }
    }

    public void UpgradeWeapon() {
        weaponIndex += 1;
    }

    public void UpgradeDelay() {
        delay *= 0.9f;
    }

    public void UpgradeMovement() {
        moveSpeed += 1;
        if (moveSpeed > 7) {
            AnimationController movemotion = GetComponent<AnimationController>();
            movemotion.movemotionUpgrade();
        }
    }
}
