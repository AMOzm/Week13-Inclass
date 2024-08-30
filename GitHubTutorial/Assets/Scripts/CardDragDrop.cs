using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CardDragDrop : MonoBehaviour
{
    // private Color mouseOverColor;
    // private Color originalColor;
    public bool dragging = false;
    private float distance;
    public float snapDistance = .8f;
    private Vector3 startDist;
    public Vector2 originalPos;
    private Vector2 otherCardOriginalPos;
    // private Vector2 TargetPos;
    private GameObject otherCard;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;
    
 
   
    void OnMouseEnter()
    {
        // GetComponent<Renderer>().material.color = mouseOverColor;
    }
 
    void OnMouseExit()
    {
        // GetComponent<Renderer>().material.color = originalColor;
    }
    void Start(){
        originalPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
    }
 
    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;
        spriteRenderer.sortingOrder = 9999;
    }
 
    void OnMouseUp()
    {
        dragging = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, snapDistance);
        foreach (var collider in colliders)
        {
            // if (collider.gameObject.CompareTag("cardSlot"))
            // {
            //     transform.position = collider.transform.position;
            //     break;
            // }
            if (collider.gameObject.CompareTag("CardOne") )
                {
                    otherCard = collider.gameObject;
                    otherCardOriginalPos = otherCard.transform.position;
                    transform.position = otherCardOriginalPos;
                    otherCard.transform.position = originalPos;
                    
                    break; 
                }
            else if(collider.gameObject.CompareTag("CardTwo"))
                {
                    otherCard = collider.gameObject;
                    otherCardOriginalPos = otherCard.transform.position;
                    transform.position = otherCardOriginalPos;
                    otherCard.transform.position = originalPos;
                    
                    break; 
                }
                else if(collider.gameObject.CompareTag("CardThree"))
                {
                    otherCard = collider.gameObject;
                    otherCardOriginalPos = otherCard.transform.position;
                    transform.position = otherCardOriginalPos;
                    otherCard.transform.position = originalPos;
                    
                    break; 
                }
            else{
                transform.position = originalPos;
            }
        }
        spriteRenderer.sortingOrder = originalSortingOrder;
        
    }
   
 
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint + startDist;
        }
        else{
            originalPos = transform.position;
        }
        
    }
    
}
