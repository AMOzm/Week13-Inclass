using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        if(isEnd){
            GameManager.Instance.BGM.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isEnd){
            SceneManager.LoadScene(0);
            GameManager.Instance.SceneReloadCount = 0;
            }
            else{
                SceneManager.LoadScene("PlayScene");
            }
          if(isStart){
            GameManager.Instance.BGM.Play(); 
          }
        
        }
        
    }
}
