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
    [SerializeField] private GameObject wall;
    [SerializeField] private int changeSortingLayerto;
    [SerializeField] private int changeSortingLayerback;
    // public GameObject[] treeObjects = new GameObject[5];
    [SerializeField] private GameObject treePrefab; 
    [SerializeField] private int NumTreeSpawn;
    [SerializeField] private Vector2 spawnRangeX = new Vector2(-5, 5); //x range to spawn trees
    [SerializeField] private Vector2 spawnRangeY = new Vector2(-5, 5);// y range to spawn trees
    [SerializeField] private Vector2 noSpawnRangeX = new Vector2(-1, 1); // X range where trees cannot spawn
    [SerializeField] private Vector2 noSpawnRangeY = new Vector2(-1, 1);// Y range where trees cannot spawn
    public List<GameObject> treeObjects = new List<GameObject>();
    [SerializeField] private GameObject KeyPick;
    [SerializeField] private Vector3[] possiblePositions; // Array for 3 possible positions for the GameObject
    [SerializeField] private string[] correspondingTexts; // Array for 3 possible texts for TextMeshPro
    [SerializeField] private int selectedIndex;
    

    private void Start()
    {
        // Ensure the canvas is initially hidden
        if (DialogueCanvas != null)
        {
            DialogueCanvas.SetActive(false);
        }
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(GameManager.Instance.SceneReloadCount == 0){
            SpawnTrees(0);
        }
        if(GameManager.Instance.SceneReloadCount == 1){
            SpawnTrees(5);
        }
        else if(GameManager.Instance.SceneReloadCount == 2){
            SpawnTrees(10);
        }
        else if(GameManager.Instance.SceneReloadCount == 3){
            SpawnTrees(15);
        }
        else if(GameManager.Instance.SceneReloadCount == 4){
            SpawnTrees(20);
        }
        else if(GameManager.Instance.SceneReloadCount >= 5){
            SpawnTrees(30);
        }
        

         if (treeObjects.Count > 0)
        {
            // Select random index from 0 to length of list
            int randomIndex = Random.Range(0, treeObjects.Count);

            // Set tag of the randomly selected tree to "Exit"
            treeObjects[randomIndex].tag = "TreeExit";
        }
        
        

        // Randomly select an index for the position and text
        selectedIndex = Random.Range(0, 3);

        // Position the object at the selected location
        KeyPick.transform.position = possiblePositions[selectedIndex];

        // Set the corresponding text in the TextMeshPro field
        GirlText = correspondingTexts[selectedIndex];
        
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
            Destroy(wall);
        }
    }

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object has the tag "Player" or any other tag you want to check
        if (other.CompareTag("Guest")) 
        {
            Debug.Log("Hit@!");
            GameManager.Instance.Rque.Play();
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
        if (other.CompareTag("Ghost"))
        {
            Debug.Log("Ghost");
            GameManager.Instance.SceneReloadCount++;
            SceneManager.LoadScene(4);
            GameManager.Instance.Wque.Play();
            

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
            GameManager.Instance.Rque.Play();
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
                GameManager.Instance.Rque.Play();
            }
            else{
                GameManager.Instance.Wque.Play();
            }
            Key.SetActive(false);
            gateOpen = true;
        }
        if(other.CompareTag("Wall")){
            GameManager.Instance.Wque.Play();
        }
        //what happens on collide with chest object
        if (other.CompareTag("Chest"))
        {
            Debug.Log("Chest!");
            GameManager.Instance.Rque.Play();
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
            GameManager.Instance.Rque.Play();
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
    private void SpawnTrees(int numberOfTrees){
        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 spawnPosition;
            do{
            // Generate random position within the given X and Y ranges
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            spawnPosition = new Vector3(randomX, randomY, 0); 
            }
            while(IsWithinNoSpawnZone(spawnPosition));
            // Instantiate the tree prefab at the random position
            GameObject newTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            newTree.tag = "Trees";

            // Add the new tree to the treeObjects list
            treeObjects.Add(newTree);
        }
    }
     private bool IsWithinNoSpawnZone(Vector3 position)
    {
        // Check if the spawn position is within the nospawn X and Y ranges
        if (position.x >= noSpawnRangeX.x && position.x <= noSpawnRangeX.y && 
            position.y >= noSpawnRangeY.x && position.y <= noSpawnRangeY.y)
        {
            
            return true;
        }
        return false;
    }
}
