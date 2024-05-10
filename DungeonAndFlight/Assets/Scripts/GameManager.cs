using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startCanvas;
    public GameObject startGame;
    public bool isGameOver = false;
    public GameObject gameOver;

    public GameObject quit;

    public GameObject shop;

    public GameObject pause;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private TextMeshProUGUI HPText;

    [SerializeField]
    private Image enemyImage;

    [SerializeField]
    private TextMeshProUGUI enemyHPText;    

    [SerializeField]
    private TextMeshProUGUI uiText1;

    [SerializeField]
    private TextMeshProUGUI uiText2;

    [SerializeField]
    private TextMeshProUGUI uiText3;

    [SerializeField]
    private TextMeshProUGUI shopText1;

    [SerializeField]
    private TextMeshProUGUI shopText2;

    [SerializeField]
    private TextMeshProUGUI shopText3;

    [SerializeField]
    private TextMeshProUGUI shopPrice1;

    [SerializeField]
    private TextMeshProUGUI shopPrice2;

    [SerializeField]
    private TextMeshProUGUI shopPrice3;

    [SerializeField]
    private TextMeshProUGUI gameCleared;

    public GameObject stageClear;
    private int currentStage = 0;
    public Button noMoreLevels;

    public Button shopButton1;
    public Button shopButton2;
    public Button shopButton3;
    public Button shopButton4;

    private int[] weaponPrice = new int[] {50, 100, 300, 500, 1000};
    [SerializeField]
    private int nextWeaponIndex = 1;

    
    private int[] delayPrice = Enumerable.Repeat(50, 15).ToArray();

    [SerializeField]
    private int nextdelayIndex = 1;

    private int[] movementPrice = Enumerable.Repeat(30, 20).ToArray();
    
    [SerializeField]
    private int nextmovementIndex = 1;

    private MonsterSpawner monsterSpawner;
    private BackGroundController backGroundController;
    private BGMController bgmController;
    private Player player;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        startCanvas.SetActive(true);
        startGame.SetActive(false);
        gameOver.SetActive(false);
    }
    void Start()
    {
        
    }

    // public void ShowMain() {
    //     startCanvas.SetActive(true);
    //     startGame.SetActive(false);
    //     gameOver.SetActive(false);
    // }

    public void IncreseCoin(int goldValue) {
        coin += goldValue;
        UpdateCoin();
    }

    private void UpdateCoin() {
        coinText.SetText(coin.ToString());
    }

    public void UpdateHP(int HP) {
        HPText.SetText(HP.ToString());
    }

    public void UpdateEnemyHP(int HP, int maxHP) {
        enemyHPText.SetText("HP : " + HP.ToString() + " / " + maxHP.ToString());
    }

    public void UpdateEnemyImage(Sprite sprite) {
        enemyImage.sprite = sprite;
    }    

    public void StartGame()
    {
        startCanvas.SetActive(false);
        startGame.SetActive(true);

        if (monsterSpawner == null || backGroundController == null) {
            monsterSpawner = FindObjectOfType<MonsterSpawner>();
            backGroundController = FindObjectOfType<BackGroundController>();
            bgmController = FindAnyObjectByType<BGMController>();
            player = FindAnyObjectByType<Player>();
        }
    }

    public void GoToMain() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowQuit() {
        quit.SetActive(true);
    }

    public void DodgeQuit() {
        quit.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) gameOver.SetActive(true);

        if (gameOver.activeSelf && Input.GetKeyDown(KeyCode.M)) GoToMain();

        if (startCanvas.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Escape)) ShowQuit();
        }

        if (startGame.activeSelf) {
            if (!shop.activeSelf && !pause.activeSelf) {
                if (Input.GetKeyDown(KeyCode.P)) EnterShop();
                if (Input.GetKeyDown(KeyCode.Escape)) DoPause();
            }

            if (shop.activeSelf) {
                if (Input.GetKeyDown(KeyCode.Escape)) ExitShop();
                if (Input.GetKeyDown(KeyCode.W)) UpgradeWeapon();
                if (Input.GetKeyDown(KeyCode.D)) UpgradeDelay();
                if (Input.GetKeyDown(KeyCode.M)) UpgradeMovement();
                if (Input.GetKeyDown(KeyCode.H)) HPRecovery();
            }
        }

        if (stageClear.activeSelf) {
            if (Input.GetKeyDown(KeyCode.T)) TryNextLevel();
            if (Input.GetKeyDown(KeyCode.M)) GoToMain();
        }
    }

    public void EndGame() {
        isGameOver = true;
    }

    public void DoPause() {
        Time.timeScale = 0f;
        pause.SetActive(true);
    }

    public void DoResume() {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnterShop() {
        Time.timeScale = 0f;
        ShopUpdate();
        shop.SetActive(true);
    }

    public void ExitShop() {
        Time.timeScale = 1f;
        shop.SetActive(false);
        ShopUpdate();
    }

    void ShopUpdate() {
        if (nextWeaponIndex <= weaponPrice.Length) {
            shopText1.SetText(nextWeaponIndex.ToString());
            shopPrice1.SetText(weaponPrice[nextWeaponIndex - 1].ToString());
        } else {
            shopText1.SetText("Max");
            shopPrice1.SetText("Max");
        }
        if (nextdelayIndex <= delayPrice.Length) {
            shopText2.SetText(nextdelayIndex.ToString());
            shopPrice2.SetText(delayPrice[nextdelayIndex - 1].ToString());
        } else {
            shopText2.SetText("Max");
            shopPrice2.SetText("Max");
        }
        if (nextmovementIndex <= movementPrice.Length) {
            shopText3.SetText(nextmovementIndex.ToString());
            shopPrice3.SetText(movementPrice[nextmovementIndex - 1].ToString());
        } else {
            shopText3.SetText("Max");
            shopPrice3.SetText("Max");
        }
    }

    public void UIUpdate(int damage, float delay, float moveMent) {
        uiText1.SetText(damage.ToString());
        uiText2.SetText((int)((1 - (float)Math.Round(delay, 2)) * 100) + "%".ToString());
        uiText3.SetText(moveMent.ToString());
    }

    public void UpgradeWeapon() {
        if (coin >= weaponPrice[nextWeaponIndex - 1]) {
            coin -= weaponPrice[nextWeaponIndex - 1];
            nextWeaponIndex += 1;
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.UpgradeWeapon();
            }
            ShopUpdate();
            UpdateCoin();
        }
        if (nextWeaponIndex > weaponPrice.Length) {
            shopButton1.interactable = false;
        }
    }

    public void UpgradeDelay() {
        if (coin >= delayPrice[nextdelayIndex - 1]) {
            coin -= delayPrice[nextdelayIndex - 1];
            nextdelayIndex += 1;
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.UpgradeDelay();
            }
            ShopUpdate();
            UpdateCoin();
        }
        if (nextdelayIndex > delayPrice.Length) {
            shopButton2.interactable = false;
        }
    }

    public void UpgradeMovement() {
        if (coin >= movementPrice[nextmovementIndex - 1]) {
            coin -= movementPrice[nextmovementIndex - 1];
            nextmovementIndex += 1;
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.UpgradeMovement();
            }
            ShopUpdate();
            UpdateCoin();
        }
        if (nextmovementIndex > movementPrice.Length) {
            shopButton3.interactable = false;
        }
    }

    public void HPRecovery() {
        if (coin >= 200) {
            coin -= 200;
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.IncreaseHP();
            }
            ShopUpdate();
            UpdateCoin();
        }
    }

    public void BossKilled() {
        
        currentStage += 1;
        Invoke("ShowStageClear", 1.5f);
    }

    private void ShowStageClear() {
        if (currentStage == monsterSpawner.Bosses.Length) {
            noMoreLevels.interactable = false;
            gameCleared.SetText("GameCleared!");
        }
        stageClear.SetActive(true);
    }

    public void TryNextLevel() {
        monsterSpawner.NextLevel();
        backGroundController.NextLevel();
        bgmController.NextLevel();
        player.NewLevel();
        stageClear.SetActive(false);
    }
}
