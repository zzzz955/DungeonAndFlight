using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] Bosses;
    public GameObject[] level1;
    public GameObject[] level2;
    private GameObject[][] levels;
    public int levelIndex;

    private float[] posY = {4f, 2f, 0f, -2f, -4f};
    private bool isCreate;
    private float coolTime;
    private float spawnTime = 0f;
    public float coolDown = 1.0f;
    private int createCnt = 0;
    private bool isBoss;

    [SerializeField]
    private int spawnPerLevels = 10;

    void Awake() {
        levels = new GameObject[][] {level1, level2};
    }
    void Start()
    {
        isCreate = true;
        isBoss = false;
        levelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreate && gameObject.activeSelf) {
            spawnTime += Time.deltaTime;
            if (spawnTime >= coolTime && createCnt < spawnPerLevels) {
                SpawnEnemy(Random.Range(0, posY.Length), Random.Range(0, levels[levelIndex].Length));
                createCnt += 1;
                coolTime = Random.Range(1.0f, 5.0f) * coolDown;
                spawnTime = 0f;
            }
            if (createCnt == spawnPerLevels && isBoss == false) {
                isBoss = true;
                SpawnBoss(levelIndex);
            }
        }
    }

    void SpawnEnemy(int posIndex, int index) {
        Vector3 spawnPos = new Vector3(transform.position.x, posY[posIndex], transform.position.z);
        Instantiate(levels[levelIndex][index], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }

    void SpawnBoss(int index) {
        Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z);
        Instantiate(Bosses[levelIndex], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }

    void NextLevel() {
        levelIndex += 1;
        isBoss = false;
        createCnt = 0;
    }
}
