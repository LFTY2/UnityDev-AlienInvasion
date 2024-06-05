using System;
using UnityEngine;

namespace _PROJECT.Scripts.InGame.Bank
{
    public class FoodVault
    {
        private const string COINS_KEY = "Coins";
        public event Action OnCoinsChange;
        private int _food = PlayerPrefs.GetInt(COINS_KEY, 0);

        public int Food
        {
            get => _food;
            set
            {
                _food = value;
                PlayerPrefs.SetInt(COINS_KEY, _food);
                OnCoinsChange?.Invoke();
            }
        }
    }
}