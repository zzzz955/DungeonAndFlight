using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 3f;
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

    public TMP_Text coinTextPrefab; // UI Text 프리팹
    public float coinDisplayDuration = 3f; // 코인 획득량이 표시되는 시간
    public float coinDisplayMoveSpeed = 1f;

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
            GameManager.instance.UpdateHP(0);
            GameManager.instance.EndGame();
            Destroy(gameObject);
        } else {
            GameManager.instance.UpdateHP(playerHp);
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
            Gold gold = other.GetComponent<Gold>();
            GameManager.instance.IncreseCoin(gold.amount);
            Destroy(other.gameObject);

            // 코인 획득량을 표시하는 UI Text 생성
            DisplayCoinText(gold.amount, other.transform.position);
        }
    }

    private void DisplayCoinText(int coinAmount, Vector3 position) {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas == null)
        {
            return;
        }
        if (coinTextPrefab != null)
        {
            TMP_Text coinText = Instantiate(coinTextPrefab, canvas.transform);
            coinText.text = "+ " + coinAmount.ToString() + "G";
            coinText.rectTransform.position = Camera.main.WorldToScreenPoint(position);

            Destroy(coinText.gameObject, coinDisplayDuration);
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

    public void IncreaseHP() {
        playerHp += 5;
    }
}
