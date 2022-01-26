using UnityEngine;

namespace AI
{
    public class Enemy : IEnemy
    {
        private const float HealthLimit = 1.5f;
        private const float PowerLimit = 1.8f;
        private const float CoinsLimit = 7f;

        private string _name;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _moneyPlayer;

        public Enemy(string name)
        {
            _name = name;
        }

        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    _healthPlayer = dataPlayer.Health;
                    break;

                case DataType.Power:
                    _powerPlayer = dataPlayer.Power;
                    break;

                case DataType.Money:
                    _moneyPlayer = dataPlayer.Money;
                    break;
            }

            Debug.Log($"Notified {_name} change to {dataPlayer}");
        }

        public int Power
        {
            get
            {
                var power = (int) (_moneyPlayer / CoinsLimit + _healthPlayer / HealthLimit + _powerPlayer / PowerLimit);
                return power;
            }
        }
    }
}