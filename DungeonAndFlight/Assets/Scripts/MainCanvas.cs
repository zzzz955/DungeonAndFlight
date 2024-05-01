using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject gamePlayCanvas;
    // Start is called before the first frame update
    void Start()
    {
        // 시작 시 게임 시작 캔버스만 활성화
        startCanvas.SetActive(true);
        gamePlayCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        // 게임 시작 버튼 클릭 시 게임 플레이 캔버스로 전환
        startCanvas.SetActive(false);
        gamePlayCanvas.SetActive(true);
    }
}
