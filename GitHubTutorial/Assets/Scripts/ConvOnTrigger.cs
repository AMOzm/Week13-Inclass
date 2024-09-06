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
    [SerializeField] private Collider2D gate;
    [SerializeField] private int changeSortingLayerto;
    [SerializeField] private int changeSortingLayerback;
    

    private void Start()
    {
        // Ensure the canvas is initially hidden
        if (DialogueCanvas != null)
        {
            DialogueCanvas.SetActive(false);
        }
    
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                DialogueCanvas.SetActive(true);
                if(hasKey == false){
                dialogueText.text = GirlText;
                NameText.text = GirlName;
                }
                if(hasKey == true){
                dialogueText.text = GirlTextKey;
                NameText.text = GirlName;
                }
                if(gateOpen == true){
                dialogueText.text = GirlTextNothing;
                NameText.text = GirlName;
                }
            }
        }

        if(other.CompareTag("Trees"))
        {
            Debug.Log("Tree!");
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerto; // Disable the sprite renderer to make it invisible
            }
        }
        if(other.CompareTag("TreeExit"))
        {
            Debug.Log("Exit!");
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerto; // Disable the sprite renderer to make it invisible
            }
            if(ExitOpen == true){
                SceneManager.LoadScene(0);
            }
        }
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
        if (other.CompareTag("Chest"))
        {
            Debug.Log("Chest!");

            // Change the sprite of the Gate object
            SpriteRenderer chestSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (chestSpriteRenderer != null && ChestOpenSprite != null)
            {
                chestSpriteRenderer.sprite = ChestOpenSprite; // Change the sprite of the Gate object
            }
            // Enable the canvas
            if (DialogueCanvas != null)
            {
                DialogueCanvas.SetActive(true);
                dialogueText.text = ChestText;
                NameText.text = ChestName;
                isChestCanvasActive = true;
                ExitOpen = true;

            }
        }
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
                spriteRenderer.sortingOrder = changeSortingLayerback; // Disable the sprite renderer to make it invisible
            }
        }
        if(other.CompareTag("TreeExit"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = changeSortingLayerback; // Disable the sprite renderer to make it invisible
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
            dialogueText.text = ChestTextCont;
        }
    }
}
