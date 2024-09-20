using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConvOnTrigger : MonoBehaviour
{
   // Reference to Canvas for text and TMP to display text
    [SerializeField] private GameObject DialogueCanvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text NameText;
    // different strings of text for different needs
    [SerializeField] private string GirlText;
    [SerializeField] private string GirlTextKey;
    [SerializeField] private string GirlTextNothing;
    [SerializeField] private string GirlName;
    [SerializeField] private string ChestText;
    [SerializeField] private string ChestTextCont;
    [SerializeField] private string ChestName;
     private bool isChestCanvasActive = false;

    //Sprite of character and sprites needed for on trigger
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite newGateSprite;
    [SerializeField] private Sprite ChestOpenSprite;
    [SerializeField] private GameObject Key;
    [SerializeField] private bool hasKey;
    [SerializeField] private bool gateOpen;
    [SerializeField] private bool ExitOpen;
    [SerializeField] private bool TimerVer;
    [SerializeField] private Collider2D gate;
    [SerializeField] private int changeSortingLayerto;
    [SerializeField] private int changeSortingLayerback;
    public GameObject[] gameObjects = new GameObject[5];
    

    private void Start()
    {
        // Ensure the canvas is initially hidden
        if (DialogueCanvas != null)
        {
            DialogueCanvas.SetActive(false);
        }
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObjects.Length == 5)
        {
            // Select random index from 0 to 5
            int randomIndex = Random.Range(0, gameObjects.Length);

            // Set tag of the randomly selected object to "Exit"
            gameObjects[randomIndex].tag = "TreeExit";
        }
        else
        {
            Debug.LogError("Array must contain exactly 6 GameObjects.");
        }
    }
    private void Update()
    {
        // Check if the chest canvas is active and the space key is pressed
        if (isChestCanvasActive && Input.GetKeyDown(KeyCode.Space))
        {
            UpdateDialogueText(); // Call the method to update the text
        }
        if(hasKey == true){
            gate.isTrigger = true;
        }
    }

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object has the tag "Player" or any other tag you want to check
        if (other.CompareTag("Guest")) 
        {
            Debug.Log("Hit@!");
            // Enable the canvas
            if (DialogueCanvas != null)
            {
                //what happens when collide with girl before getting key
                DialogueCanvas.SetActive(true);
                if(hasKey == false){
                dialogueText.text = GirlText;
                NameText.text = GirlName;
                }
                //what happens when collide with girl after getting key but before opening gate
                if(hasKey == true){
                dialogueText.text = GirlTextKey;
                NameText.text = GirlName;
                }
                //what happens after opening gate.
                if(gateOpen == true){
                dialogueText.text = GirlTextNothing;
                NameText.text = GirlName;
                }
            }
        }
        //what happens on collide with tree object
        if(other.CompareTag("Trees"))
        {
            Debug.Log("Tree!");
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerto; // change sorting layer of player to make it hide behind tree
            }
        }
        //what happens on collide with special exit tree object
        if(other.CompareTag("TreeExit"))
        {
            Debug.Log("Exit!");
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerto; // change sorting layer of player to make it hide behind tree
            }
            if(ExitOpen == true){
                SceneManager.LoadScene(2);
            }
            if(TimerVer == true){
                SceneManager.LoadScene(2);
            }
        }
        //what happens on collide with gate object
        if (other.CompareTag("Gate"))
        {
            Debug.Log("Gate!");

            // Change the sprite of the Gate object
            SpriteRenderer gateSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (gateSpriteRenderer != null && newGateSprite != null)
            {
                gateSpriteRenderer.sprite = newGateSprite; // Change the sprite of the Gate object
            }
            Key.SetActive(false);
            gateOpen = true;
        }
        //what happens on collide with chest object
        if (other.CompareTag("Chest"))
        {
            Debug.Log("Chest!");

            // Change the sprite of the chest object
            SpriteRenderer chestSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (chestSpriteRenderer != null && ChestOpenSprite != null)
            {
                chestSpriteRenderer.sprite = ChestOpenSprite; 
            }
            // Enable the canvas
            if (DialogueCanvas != null)
            {
                //show dialogue from the chest
                DialogueCanvas.SetActive(true);
                dialogueText.text = ChestText;
                NameText.text = ChestName;
                isChestCanvasActive = true;
                ExitOpen = true;

            }
        }
        //what happens when player hits key, key gets "picked up", born as child of player object
        if (other.CompareTag("Key"))
        {
            Debug.Log("Key!");
            Key.SetActive(true);
            Destroy(other.gameObject);
            hasKey = true;
        }
    }

    // This method is called when another collider exits the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object has the tag "Player" or any other tag you want to check
        if (other.CompareTag("Guest")) 
        {
            // Hide the canvas
            if (DialogueCanvas != null)
            {
                DialogueCanvas.SetActive(false);
            }
        }
        if(other.CompareTag("Trees"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerback; //move plyaer sorting layer back to make it visible
            }
        }
        if(other.CompareTag("TreeExit"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerback; //move plyaer sorting layer back to make it visible
            }
        }
        if (other.CompareTag("Chest"))
        {
            // Enable the canvas
            if (DialogueCanvas != null)
            {
                DialogueCanvas.SetActive(false);
            }
        }
    }
     private void UpdateDialogueText()
    {
        if (dialogueText != null)
        {
            //change dialogue to the next sentence
            dialogueText.text = ChestTextCont;
        }
    }
}
