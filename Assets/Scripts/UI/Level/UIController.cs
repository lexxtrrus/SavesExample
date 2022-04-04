using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI.Level
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Text _coinsCounter;
        [SerializeField] private Text _coinsTotal;

        public event Action OnSaveButton;
        private int current = 0;
        private int max = 0;
        private int total = 0;
        
        private void Awake()
        {
            _saveButton.onClick.AddListener(SaveCharacterPosition);
            OnSaveButton += SaveTotal;
        } 
        private void OnDestroy()
        {
            _saveButton.onClick.RemoveListener(SaveCharacterPosition);
            OnSaveButton -= SaveTotal;
        }
        private void SaveCharacterPosition()
        {
            OnSaveButton?.Invoke();
        }
        private void SetupCoinsCounter(int current, int max)
        {
            _coinsCounter.text = $"COINS: {current}/{max}";
        }
        public void Init(bool[] coins)
        {
            foreach (var coin in coins)
            {
                if (coin) current++;
            }

            max = coins.Length;

            total = Profile.Coins;
            _coinsTotal.text = $"TOTAL: {total}";
            SetupCoinsCounter(current, max);
        }
        public void IncreaseCount()
        {
            total++;
            _coinsTotal.text = $"TOTAL: {total}";
            current++;
            SetupCoinsCounter(current, max);
        }

        public void OnMenuButtonClicked()
        {
            SceneManager.LoadScene("Menu");
        }

        private void SaveTotal()
        {
            Profile.Coins = total;
        }

        public void OnLevelEnd()
        {
            SaveTotal();
        }
    }
}