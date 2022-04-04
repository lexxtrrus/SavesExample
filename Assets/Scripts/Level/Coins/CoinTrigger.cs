using System;
using UnityEngine;

namespace Level.Coins
{
    public class CoinTrigger : MonoBehaviour
    {
        public event Action<CoinTrigger> OnCoinCollect;
        private void OnTriggerEnter(Collider other)
        {
            OnCoinCollect?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
