using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.InGame.Player
{
    public class PlayerFoodCounter : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private TMP_Text _foodText;
        private void OnEnable()
        {
            playerController.OnFoodChange += ChangeFoodText;
        }
        private void OnDisable()
        {
            playerController.OnFoodChange -= ChangeFoodText;
        }

        private void ChangeFoodText(int maxFood, int food)
        {
            _foodText.text = $"{food}/{maxFood}";
        }
    }
}