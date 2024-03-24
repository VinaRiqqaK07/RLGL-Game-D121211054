using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float movementSpeed;

    public DollController dollController;
    public GameObject playerBody;
    public TextMeshProUGUI stateText;
    public bool isDead;

    public ParticleSystem bloodEffect;

    AudioSource audioSource;
    public AudioClip gotShotSfx;

    public bool hasWon;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        if (isDead) return;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);

        if(!hasWon)
        {
            if (moveX != 0 || moveZ != 0)
            {
                if (!dollController.isGreenLight)
                {
                    dollController.ShootPlayer(transform);
                    print("Kamu ditembak mati!");
                }
            }
        }

        characterController.Move(move * movementSpeed * Time.deltaTime);

        if (moveX == 0 && moveZ == 0) return;

        float heading = Mathf.Atan2(moveX, moveZ);
        transform.rotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

    }

    public void Dead()
    {
        isDead = true;
        audioSource.PlayOneShot(gotShotSfx);
        bloodEffect.Play();
        playerBody.SetActive(false);
        stateText.text = "Player is Dead";
        stateText.enabled = true;
        print("Player Mati");
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FinishLine>())
        {
            print("Player Menang!");
            stateText.text = "Player Won!";
            stateText.enabled = true;
            hasWon = true;
        }
    }
}
