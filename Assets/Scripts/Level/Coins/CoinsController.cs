using System;
using System.Collections.Generic;
using System.Linq;
using UI.Level;
using UnityEngine;
using Utils;

namespace Level.Coins
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private List<CoinTrigger> _coins;

        public event Action OnCollectCoin;
        public event Action OnLevelEnd;

        private UIController _uiController;

        public void Init(bool[] isCollected, UIController uiController)
        {
            _uiController = uiController;
            OnCollectCoin += _uiController.IncreaseCount;
            
            for (int i = 0; i < isCollected.Length; i++)
            {
                if (isCollected[i])
                {
                    _coins[i].gameObject.SetActive(false);
                }
            }

            foreach (var coin in _coins)
            {
                coin.OnCoinCollect += OnCollected;
            }
        }

        private void OnDestroy()
        {
            OnCollectCoin -= _uiController.IncreaseCount;
            
            foreach (var coin in _coins)
            {
                coin.GetComponent<CoinTrigger>().OnCoinCollect -= OnCollected;
            }
        }

        private void OnCollected(CoinTrigger coin)
        {
            var index = _coins.IndexOf(coin);
            Profile.UpdateLevelCoinsData(Profile.CurrentLevel,index);
            OnCollectCoin?.Invoke();

            CheckAllCollected();
        }

        private void CheckAllCollected()
        {
            var coins = Profile.GetLevelCoinsData(Profile.CurrentLevel);

            if (coins.All(x => x == true))
            {
                OnLevelEnd?.Invoke();
            }
        }
    }
}
