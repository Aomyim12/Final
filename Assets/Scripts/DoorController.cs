using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool playerNearby = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log("playerNearby: " + playerNearby); // เพิ่มบรรทัดนี้
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
            Debug.Log("isOpen: " + isOpen); // เพิ่มบรรทัดนี้
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}