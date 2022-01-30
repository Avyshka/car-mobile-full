using System.Collections.Generic;

namespace AI
{
    public abstract class DataPlayer
    {
        private string _titleData;
        private int _countHealth;
        private int _countPower;
        private int _countMoney;
        private List<IEnemy> _enemies = new List<IEnemy>();

        protected DataPlayer(string titleData)
        {
            _titleData = titleData;
        }

        public void AttachEnemy(IEnemy enemy)
        {
            _enemies.Add(enemy);
        }
        
        public void DetachEnemy(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }

        protected void Notify(DataType dataType)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Update(this, dataType);
            }
        }

        public int Health
        {
            get => _countHealth;
            set
            {
                if (_countHealth != value)
                {
                    _countHealth = value;
                    Notify(DataType.Health);
                }
            }
        }

        public int Power
        {
            get => _countPower;
            set
            {
                if (_countPower != value)
                {
                    _countPower = value;
                    Notify(DataType.Power);
                }
            }
        }

        public int Money
        {
            get => _countMoney;
            set
            {
                if (_countMoney != value)
                {
                    _countMoney = value;
                    Notify(DataType.Money);
                }
            }
        }
    }

    class Health : DataPlayer
    {
        public Health(string titleData) : base(titleData)
        {
        }
    }

    class Power : DataPlayer
    {
        public Power(string titleData) : base(titleData)
        {
        }
    }

    class Money : DataPlayer
    {
        public Money(string titleData) : base(titleData)
        {
        }
    }
}