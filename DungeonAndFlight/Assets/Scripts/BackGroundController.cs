using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backGrounds;
    public int backGroundIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShowBackGround(backGroundIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBackGround(int index) {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(backGrounds[index], transform.position, Quaternion.identity);
    }

    public void NextLevel() {
        backGroundIndex += 1;
        ShowBackGround(backGroundIndex);
    }
}