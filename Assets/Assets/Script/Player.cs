using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private Animator anim1;
    
    public bool continueGame = false;


    [SerializeField] private float gravity = -12f;
    [SerializeField] private float strength = 4f;
    public string circleTag = "Coin";

    private GameManager gameManager;
    private EdgeCollider2D playerCollider;
    public GameObject pipes;

    void Start()
    {
        playerCollider = GetComponent<EdgeCollider2D>();
        anim1 = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
        if (playerCollider == null)
        {
            Debug.LogError("EdgeCollider2D not found on the player GameObject!");
        }

    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        if (!continueGame)
        {
            position.y = 0f;
        }
        else if (continueGame)
        {
            DisableColliderForDuration(0.5f);
        }


        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        

        CheckCircleCollisions();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            anim1.SetBool("Click", true);
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
        
    }

    private void CheckCircleCollisions()
    {
        float circleRadius = 0.2f;
        Collider2D[] circleColliders = Physics2D.OverlapCircleAll(transform.position, circleRadius);

        foreach (Collider2D circleCollider in circleColliders)
        {
            if (circleCollider.CompareTag(circleTag))
            {
                Destroy(circleCollider.gameObject);
                gameManager.AddCoin();
                gameManager.SaveCoins();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        if (other.CompareTag("Obstacle"))
        {
            
            gameManager.GameOver();
    
        }
        else if (other.CompareTag("Scoring"))
        {
            gameManager.AddScore();
        }
        else if(other.CompareTag("Ground"))
        {
            gameManager.GameOver();
        }    
    }
    private void DisableColliderForDuration(float duration)
    {
        // Disable the collider.
        playerCollider.enabled = false;

        // Start a coroutine to enable the collider after the specified duration.
        StartCoroutine(EnableColliderAfterDelay(duration));
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        // Wait for the specified duration.
        yield return new WaitForSeconds(delay);

        // Enable the collider.
        playerCollider.enabled = true;
    }
}


