using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager FindInstance(){
        return instance;
    }
    // [SerializeField] private GameObject startCanvas;
    // [SerializeField] private GameObject player;


    
    void awake(){
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Debug.Log("Current Loop: " + cycleCount);
        //Debug.Log("Current Index: " + sceneIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
    //    startCanvas.SetActive(true);
    //    player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    //    if(Input.GetKeyDown(KeyCode.Space)){
    //         startCanvas.SetActive(false);
    //         player.SetActive(true);
    //     }

   
    }

public class Helper{


    public void TestFunction(){
        Debug.Log("Helper class called");
        }
    }
}