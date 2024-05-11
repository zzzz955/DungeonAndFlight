using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] Bosses;
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject[] level3;
    public GameObject[] level4;
    public GameObject[] level5;
    private GameObject[][] levels;
    public int levelIndex;
    private bool showWarning = true;
    public GameObject warningTop;
    public GameObject warningBottom;

    private float[] posY = {4f, 2f, 0f, -2f, -4f};
    private bool isCreate;
    private float coolTime;
    private float spawnTime = 0f;
    public float coolDown = 1.0f;
    private int createCnt = 0;
    public bool isBossRound;

    [SerializeField]
    private int spawnPerLevels = 30;

    void Awake() {
        levels = new GameObject[][] {level1, level2, level3, level4, level5};
    }
    void Start()
    {
        isCreate = true;
        isBossRound = false;
        levelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreate && gameObject.activeSelf) {
            spawnTime += Time.deltaTime;
            if (createCnt == spawnPerLevels - 5) {
                if (showWarning) SetWarning();
                showWarning = false;
            }
            if (spawnTime >= coolTime && createCnt < spawnPerLevels) {
                SpawnEnemy(Random.Range(0, posY.Length), Random.Range(0, levels[levelIndex].Length));
                createCnt += 1;
                coolTime = Random.Range(1.0f, 4.0f) * coolDown;
                spawnTime = 0f;
            }
            if (createCnt == spawnPerLevels && isBossRound == false) {
                isBossRound = true;
                SpawnBoss(levelIndex);
            }
        }
    }

    void SpawnEnemy(int posIndex, int index) {
        Vector3 spawnPos = new Vector3(transform.position.x, posY[posIndex], transform.position.z);
        Instantiate(levels[levelIndex][index], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }

    void SpawnBoss(int index) {
        Vector3 spawnPos = new Vector3(7f, 0f, transform.position.z);
        Instantiate(Bosses[levelIndex], spawnPos, Quaternion.Euler(0f, 180f, 0f));
    }

    void SetWarning() {
        Vector3 setTop = new Vector3(0f, 3.5f, transform.position.z);
        Vector3 setBottom = new Vector3(0f, -3.5f, transform.position.z);
        GameObject top = Instantiate(warningTop, setTop, Quaternion.identity);
        GameObject bottom = Instantiate(warningBottom, setBottom, Quaternion.identity);
        Destroy(top, 10f);
        Destroy(bottom, 10f);
    }

    public void NextLevel() {
        levelIndex += 1;
        createCnt = 0;
        isBossRound = false;
        coolDown *= 0.9f;
        spawnPerLevels += 15;
        showWarning = true;
    }
}
