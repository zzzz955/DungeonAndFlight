using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    public int damage = 1;

    [SerializeField]
    private bool isAdvanced = false;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (isAdvanced && player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = direction * moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isAdvanced) {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            player.playerHp -= damage;
            Destroy(gameObject);
        }
    }
}

