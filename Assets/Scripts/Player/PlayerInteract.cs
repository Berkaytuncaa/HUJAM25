using InteractableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float _interactRange = 1.5f;
        [SerializeField] private LayerMask _interactableLayer;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
                Debug.Log("You Interacted");
            }
        }

        private void Interact()
        {
            var hit = Physics2D.OverlapCircle(transform.position, _interactRange, _interactableLayer);
            if (hit is null)
            {
                Debug.Log("It's null");
                return;
            }
            Debug.Log("It's not null");
            var interactable = hit.GetComponent<IInteractable>();
            interactable?.OnInteract();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _interactRange);
        }
    }
}
