using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public float initialGameTime; // Initial game time set in the inspector
    [SerializeField]private float gameTime; // Actual game time adjusted for reloads
    private bool stopTimer;
    private float time;
    public AudioSource TUque;
    private bool musicStarted;
    
    // public SceneManagerScript sm;
    
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        int reloadCount = GameManager.Instance.SceneReloadCount;
        gameTime = Mathf.Max(10, initialGameTime - reloadCount * 2);

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
        if (time <= 5f && !musicStarted)
            {
                TUque.Play();
                musicStarted = true;
            }
        if (time <= 0){
            Debug.Log("Stopped;");
            stopTimer = true;
            TUque.Stop();
            musicStarted = false;
            GameManager.Instance.SceneReloadCount++;
            SceneManager.LoadScene(3);
            
        }
        }
     
    }
}


