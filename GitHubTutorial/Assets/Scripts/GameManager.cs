using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // [SerializeField] private GameObject startCanvas;
    // [SerializeField] private GameObject player;
    public int SceneReloadCount { get; set; }
    public AudioSource BGM;  
    public AudioSource Rque;
    public AudioSource Wque;
    
    public bool musicStarted = false;

    
    void Awake(){
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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