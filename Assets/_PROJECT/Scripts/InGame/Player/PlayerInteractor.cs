using UnityEngine;

namespace _PROJECT.Scripts.InGame.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IIntractable intractable = other.GetComponent<IIntractable>();
            intractable?.Interact();
        }
    }
}