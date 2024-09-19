using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public float gameTime;
    private bool stopTimer;
    private float time;
    // public SceneManagerScript sm;
    
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        time = gameTime;
        // BarPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopTimer){
        time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes *60);
        timerSlider.value = time;
        if (time <= 0){
            Debug.Log("Stopped;");
            stopTimer = true;
            SceneManager.LoadScene(3);
            
        }
        }
     
    }
}


