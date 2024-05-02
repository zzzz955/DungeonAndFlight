using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float hp;          // 몬스터 체력
    public float moveSpeed;     // 몬스터 이동 속도
    public int damage;

    private float minX = -10f;
    private float posY;
    private bool isMovingUp;

    void Start()
    {
        posY = Random.Range(-5f, 5f);
        isMovingUp = Random.value > 0.5f; // 랜덤으로 이동 방향 결정
    }

    void Update() {
        Vector3 moveDirection = isMovingUp ? Vector3.up : Vector3.down;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (transform.position.y > 4f && isMovingUp)
        {
            isMovingUp = false;
        }
        else if (transform.position.y < -4f && !isMovingUp)
        {
            isMovingUp = true;
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
            }
        } else if (other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            player.playerHp -= damage;
            Destroy(gameObject);
        }
    }
}