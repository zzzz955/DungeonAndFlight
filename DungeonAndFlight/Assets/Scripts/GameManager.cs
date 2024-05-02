using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startCanvas;
    public GameObject startGame;
    public bool isGameOver = false;
    public GameObject gameOver;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI text;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        ShowMain();
    }
    public void ShowMain() {
         startCanvas.SetActive(true);
         startGame.SetActive(false);
         gameOver.SetActive(false);
    }

    public void IncreseCoin() {
        coin += 1;
        text.SetText(coin.ToString());
    }

    public void StartGame()
    {
        // 게임 시작 버튼 클릭 시 게임 플레이 캔버스로 전환
        startCanvas.SetActive(false);
        startGame.SetActive(true);
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
}
