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
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}

public class Helper{


    public void TestFunction(){
        Debug.Log("Helper class called");
    }
}