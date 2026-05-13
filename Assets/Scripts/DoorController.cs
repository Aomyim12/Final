using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool playerNearby = false;
    public bool isUnlocked = false;     
    public GameObject interactPrompt;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!isUnlocked)
            {
              
                PasswordUI.Instance.OpenKeypad(this);
            }
            else
            {
                isOpen = !isOpen;
                animator.SetBool("isOpen", isOpen);
            }
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        isOpen = true;
        animator.SetBool("isOpen", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (interactPrompt) interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (interactPrompt) interactPrompt.SetActive(false);
            PasswordUI.Instance.CloseKeypad();
        }
    }
    }