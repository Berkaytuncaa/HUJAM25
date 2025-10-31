using UnityEngine;

namespace InteractableObjects
{
    public class SimpleInteractableObject : MonoBehaviour, IInteractable
    {
        public void OnInteract()
        {
            Debug.Log("You Interacted with " + gameObject.name);
        }
    }
}
