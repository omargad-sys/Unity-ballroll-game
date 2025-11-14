using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public float jumpForce = 1.5f;
    public float sprintMultiplier = 2.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject New_Level_Portal;
    public GameObject RespawnPoint;
        private AudioSource audioSource;
        private Rigidbody rb;
        private int count;
        private float movementX;
        private float movementY;
        private bool grounded;
        private bool isPaused = false;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = Camera.main.GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
            }
            else
            {
                audioSource.playOnAwake = false;
                audioSource.loop = false;
            }
    
            count = 0;
            SetCountText();
            winTextObject.SetActive(false);
            Time.timeScale = 1f;
        grounded = true; 
            
        }
    
        void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    
        void Update()
    {
            
            Debug.Log("Grounded: " + grounded);
            // Jump 
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {            
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                grounded = false;
            }
    
            // Pause 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    
        void TogglePause()
        {
            isPaused = !isPaused;
    
            if (isPaused)
            {            Time.timeScale = 0f; // Pause the game
            }
            else
            {            Time.timeScale = 1f; // Resume the game
            }
        }
    
        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
            if (count >= 12)
            {
                winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

                if (New_Level_Portal != null)
          {
              New_Level_Portal.SetActive(true);
          }
    
            }
            
            if (count >= 6)
            {
                Destroy(Door2);
            }
            if (count >= 8)
            {
                Destroy(Door3);
            }
              if (count >= 10)
            {
                Destroy(Door4);
            }
        }
        void FixedUpdate()
        {
            float currentSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= sprintMultiplier;
            }
    
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * currentSpeed);
    
            if (count >=4)
            {
                Door1.transform.Rotate(50 * Time.deltaTime ,0, 0);
            }
        }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

        }
        if (other.gameObject.CompareTag("Portal"))
        {
            LoadNextLevel();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            Destroy(gameObject);
        }

    }
           void LoadNextLevel()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

        private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.CompareTag("respawn"))
            {
                transform.position = RespawnPoint.transform.position;
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {            // Destroy the current object
                Destroy(gameObject);
                // Update the winText to display "You Lose!"
                winTextObject.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                grounded = true;
            }
        }
            
        }
    
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                grounded = false;
            }
        }
}