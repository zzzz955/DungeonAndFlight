using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startCanvas;
    public GameObject startGame;
    public bool isGameOver = false;
    public GameObject gameOver;

    public GameObject shop;

    private int coin = 100;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private TextMeshProUGUI HPText;

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

    private int[] weaponPrice = new int[] {1, 2, 3, 4, 5};
    [SerializeField]
    private int nextWeaponIndex = 1;

    
    private int[] delayPrice = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    [SerializeField]
    private int nextdelayIndex = 1;

    private int[] movementPrice = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    [SerializeField]
    private int nextmovementIndex = 1;

    private MonsterSpawner monsterSpawner;
    private BackGroundController backGroundController;
    private BGMController bgmController;
    
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

    public void StartGame()
    {
        startCanvas.SetActive(false);
        startGame.SetActive(true);

        if (monsterSpawner == null || backGroundController == null) {
            monsterSpawner = FindObjectOfType<MonsterSpawner>();
            backGroundController = FindObjectOfType<BackGroundController>();
            bgmController = FindAnyObjectByType<BGMController>();
        }
    }

    public void GoToMain() {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) {
            gameOver.SetActive(true);
        }
    }

    public void EndGame() {
        isGameOver = true;
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
        if (coin >= 100) {
            coin -= 100;
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
        stageClear.SetActive(false);
    }
}
