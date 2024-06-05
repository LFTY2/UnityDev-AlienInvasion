using _PROJECT.Scripts.InGame.Bank;
using TMPro;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UIFoodView : MonoBehaviour
    {
        [Inject] private FoodVault _foodVault;
        [SerializeField] private TMP_Text _textCoinsText;
        
        private void OnEnable()
        {
            _textCoinsText.text = _foodVault.Food.ToString();
            _foodVault.OnCoinsChange += SyncFood;
        }

        private void OnDisable()
        {
            _foodVault.OnCoinsChange -= SyncFood;
        }

        private void SyncFood()
        {
            _textCoinsText.text = _foodVault.Food.ToString();
        }
    }
}