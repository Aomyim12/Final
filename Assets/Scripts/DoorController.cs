using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool playerNearby = false;
    public bool isUnlocked = false;      // ? เพิ่ม

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
                // เปิด UI กรอกรหัสแทน
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
        if (other.CompareTag("Player")) playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            PasswordUI.Instance.CloseKeypad();
        }
    }
}