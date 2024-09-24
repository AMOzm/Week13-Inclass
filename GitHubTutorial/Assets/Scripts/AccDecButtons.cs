using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccDecButtons : MonoBehaviour
{
    public Button increaseButton;  // Reference to the button that increases the integer
    public Button decreaseButton;  // Reference to the button that decreases the integer
    public Text displayText;       // Reference to a UI Text component to display the integer value

    private int counter = 0;       // The integer value to be modified
    [SerializeField]public GameObject Coin;

    void Start()
    {
        // Assign click listeners to the buttons
        increaseButton.onClick.AddListener(IncreaseCounter);
        decreaseButton.onClick.AddListener(DecreaseCounter);
        
        // Display the initial value
        UpdateDisplayText();
    }

    void IncreaseCounter()
    {
        counter++;
        Coin.SetActive(false);
        UpdateDisplayText();
    }

    void DecreaseCounter()
    {
        counter--;

        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        // Update the text to show the current value of the counter
        // displayText.text = "Counter: " + counter.ToString();
    }
}
