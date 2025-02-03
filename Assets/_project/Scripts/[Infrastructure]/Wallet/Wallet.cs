using System;

namespace _project.Scripts.Wallet
{
    public class Wallet
    {
        private WalletData _walletData;
        public WalletData WalletData => _walletData;
        public void Load(WalletData value) => 
            _walletData = value;
        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));
            
            _walletData.Money += value;
        }
        public void Reduce(int value)
        { 
            if (value < 0)
                throw new ArgumentException(nameof(value));
            
            _walletData.Money -= value;
        }
    }
}