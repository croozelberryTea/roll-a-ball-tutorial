using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private Rigidbody rb;
    
    private float movementX;
    private float movementY;

    private int count; // score
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Disable win text on start
        winTextObject.SetActive(false);
        
        rb = GetComponent <Rigidbody>();
        count = 0;
        
        SetCountText();
    }

    private void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(speed * movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
            winTextObject.gameObject.SetActive(true);
            winTextObject.gameObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {count}";

        if (count >= 5)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
