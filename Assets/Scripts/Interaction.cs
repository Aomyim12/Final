using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject interacttionText;
    public LayerMask interactionLayers;

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            if (hit.collider.gameObject.GetComponent<Letter>())
            {
                interacttionText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Letter>().openCloseLetter();
                }
            }
            else
            {
                interacttionText.SetActive(false);
            }
        }
        else
        {
            interacttionText.SetActive(false);
        }
    }
}
