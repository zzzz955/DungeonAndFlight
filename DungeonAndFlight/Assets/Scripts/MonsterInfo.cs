using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public int hp;          // 몬스터 체력
    private int maxHP;
    public float moveSpeed;     // 몬스터 이동 속도
    public int damage;

    private float minX = -10f;
    private bool isMovingUp;
    private bool isMovingHorizon;
    private float ran;

    [SerializeField]
    private GameObject gold;
    public int defaultGold;

    [SerializeField]
    private bool isBoss = false;

    [SerializeField]
    private GameObject enemyWeapon1;

    [SerializeField]
    private float delay1 = 1f;
    private float lastShotTime1 = 0f;

    [SerializeField]
    private GameObject enemyWeapon2;

    [SerializeField]
    private float delay2 = 1f;
    private float lastShotTime2 = 0f;

    private float lastTagTime = 0f;

    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        isMovingUp = Random.value > 0.5f; // 랜덤으로 이동 방향 결정
        isMovingHorizon = Random.value > 0.5f;
        maxHP = hp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    void Update() {
        Vector3 moveDirection = isMovingUp ? Vector3.up : Vector3.down;
        Vector3 moveDirection2 = isMovingHorizon ? Vector3.left : Vector3.right;
        if (!isBoss) {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (transform.position.y > 4f && isMovingUp)
        {
            isMovingUp = false;
        }
        else if (transform.position.y < -4f && !isMovingUp)
        {
            isMovingUp = true;
        }
        if (isBoss) {
            transform.Translate(moveDirection2 * moveSpeed * Time.deltaTime);
            if (transform.position.x > 9f && isMovingHorizon)
        {
            isMovingHorizon = false;
        }
        else if (transform.position.x < 5f && !isMovingHorizon)
        {
            isMovingHorizon = true;
        }
        }
        if (enemyWeapon1 != null) {
            Shoot1();         
        }
        if (enemyWeapon2 != null) {
            Shoot2();         
        }
        if (transform.position.x < minX) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            Destroy(other.gameObject);
            if (hp <= 0) {
                hp = 0;
                if (isBoss) {
                    ran = 1;
                } else {
                    ran = Random.Range(0f, 1f);
                }
                if (ran > 0.5f) {
                    if (gold != null) {
                        gold.GetComponent<Gold>().SetGoldeAmount(defaultGold);
                        Instantiate(gold, transform.position, Quaternion.identity);
                    }
                }
                if (isBoss) {
                    GameManager.instance.BossKilled();
                }
                GameManager.instance.UpdateEnemyHP(hp, maxHP);
                GameManager.instance.UpdateEnemyImage(sprite);
                Destroy(gameObject);
            } else {
                GameManager.instance.UpdateEnemyHP(hp, maxHP);
                GameManager.instance.UpdateEnemyImage(sprite);
            }
        } else if (other.gameObject.tag == "Player" && Time.time > lastTagTime) {
            lastTagTime = Time.time + 1f;
            Player player = other.gameObject.GetComponent<Player>();
            player.playerHp -= damage;
            if (!isBoss) {
                Destroy(gameObject);
            }
        }
    }

    void Shoot1() {
        if (Time.time > lastShotTime1) {
            Instantiate(enemyWeapon1, transform.position, Quaternion.Euler(0f, 0f, -90f));
            lastShotTime1 = Time.time + delay1;
        }
    }

    void Shoot2() {
        if (Time.time > lastShotTime2) {
            Instantiate(enemyWeapon2, transform.position, Quaternion.Euler(0f, 0f, -90f));
            lastShotTime2 = Time.time + delay2;
        }
    }
}