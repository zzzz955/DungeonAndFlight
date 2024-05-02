using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    private float[] posY = {4f, 2f, 0f, -2f, -4f};
    private bool isCreate;
    private float coolTime;
    private float spawnTime = 0f;
    public float coolDown = 1.0f;
    void Start()
    {
        isCreate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreate && gameObject.activeSelf) {
            spawnTime += Time.deltaTime;
            if (spawnTime >= coolTime) {
                SpawnEnemy(Random.Range(0, posY.Length), Random.Range(0, enemies.Length));
                coolTime = Random.Range(1.0f, 5.0f) * coolDown;
                spawnTime = 0f;
            }
        }
    }

    void SpawnEnemy(int posIndex, int index) {
        Vector3 spawnPos = new Vector3(transform.position.x, posY[posIndex], transform.position.z);
        Instantiate(enemies[index], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }

    void SpawnBoss(int posIndex, int index) {
        Vector3 spawnPos = new Vector3(transform.position.x, posY[posIndex], transform.position.z);
        Instantiate(enemies[index], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }
}
