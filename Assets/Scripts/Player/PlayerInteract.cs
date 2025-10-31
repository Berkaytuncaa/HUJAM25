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
            }
        }

        private void Interact()
        {
            var hit = Physics2D.OverlapCircle(transform.position, _interactRange, _interactableLayer);
            if (!hit.GetComponent<Collider>()) return;
            var interactable = hit.GetComponent<Collider>().GetComponent<IInteractable>();
            interactable?.OnInteract();
        }
    }
}
