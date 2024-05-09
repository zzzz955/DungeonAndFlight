using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float hp;          // 몬스터 체력
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
    private GameObject enemyWeapon;

    [SerializeField]
    private float delay = 1f;
    private float lastShotTime = 0f;

    private float lastTagTime = 0f;

    void Start()
    {
        isMovingUp = Random.value > 0.5f; // 랜덤으로 이동 방향 결정
        isMovingHorizon = Random.value > 0.5f;
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
        if (enemyWeapon != null) {
            Shoot();         
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
                Destroy(gameObject);
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

    void Shoot() {
        if (Time.time > lastShotTime) {
            Instantiate(enemyWeapon, transform.position, Quaternion.Euler(0f, 0f, -90f));
            lastShotTime = Time.time + delay;
        }
    }
}