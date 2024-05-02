using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject startGame;
    // Start is called before the first frame update
    void Start()
    {
        ShowMain();
    }
    public void ShowMain() {
         startCanvas.SetActive(true);
         startGame.SetActive(false);
    }

    public void StartGame()
    {
        // 게임 시작 버튼 클릭 시 게임 플레이 캔버스로 전환
        startCanvas.SetActive(false);
        startGame.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
