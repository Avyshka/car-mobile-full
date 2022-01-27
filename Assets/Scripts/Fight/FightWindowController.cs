using Profile;
using UnityEngine;

namespace AI
{
    public class FightWindowController : BaseController
    {
        private FightWindowView _fightWindowView;
        private ProfilePlayer _profilePlayer;

        private int _countHealthPlayerTotal;
        private int _countPowerPlayerTotal;
        private int _countMoneyPlayerTotal;

        private Health _health;
        private Power _power;
        private Money _money;

        private Enemy _enemy;

        public FightWindowController(Transform placeForUi, FightWindowView fightWindowView, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _fightWindowView = Object.Instantiate(fightWindowView, placeForUi);
            AddGameObjects(_fightWindowView.gameObject);
        }

        public void RefreshView()
        {
            _enemy = new Enemy("Enemy Flappy");

            _health = new Health(nameof(Health));
            _power = new Power(nameof(Power));
            _money = new Money(nameof(Money));

            _health.AttachEnemy(_enemy);
            _power.AttachEnemy(_enemy);
            _money.AttachEnemy(_enemy);

            SubscribeButtons();
        }

        private void SubscribeButtons()
        {
            _fightWindowView.IncreaseHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _fightWindowView.DecreaseHealthButton.onClick.AddListener(() => ChangeHealth(false));

            _fightWindowView.IncreasePowerButton.onClick.AddListener(() => ChangePower(true));
            _fightWindowView.DecreasePowerButton.onClick.AddListener(() => ChangePower(false));

            _fightWindowView.IncreaseMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _fightWindowView.DecreaseMoneyButton.onClick.AddListener(() => ChangeMoney(false));

            _fightWindowView.FightButton.onClick.AddListener(Fight);
        }

        private void ChangeHealth(bool isAddCount)
        {
            _countHealthPlayerTotal += isAddCount ? 1 : -1;
            ChangeDataWindow(_countHealthPlayerTotal, DataType.Health);
        }

        private void ChangePower(bool isAddCount)
        {
            _countPowerPlayerTotal += isAddCount ? 1 : -1;
            ChangeDataWindow(_countPowerPlayerTotal, DataType.Power);
        }

        private void ChangeMoney(bool isAddCount)
        {
            _countMoneyPlayerTotal += isAddCount ? 1 : -1;
            ChangeDataWindow(_countMoneyPlayerTotal, DataType.Money);
        }

        private void Fight()
        {
            Debug.Log(_countPowerPlayerTotal >= _enemy.Power
                ? "<color=#07FF00>Win!!!</color>"
                : "<color=#FF0000>Lose!!!</color>");
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    _fightWindowView.CountHealthText.text = $@"Player Health {countChangeData}";
                    _health.Health = countChangeData;
                    break;

                case DataType.Power:
                    _fightWindowView.CountPowerText.text = $@"Player Power {countChangeData}";
                    _power.Power = countChangeData;
                    break;

                case DataType.Money:
                    _fightWindowView.CountMoneyText.text = $@"Player Money {countChangeData}";
                    _money.Money = countChangeData;
                    break;
            }

            _fightWindowView.CountPowerEnemyText.text = $@"Enemy Power {_enemy.Power}";
        }

        protected override void OnDispose()
        {
            _fightWindowView.IncreaseHealthButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreaseHealthButton.onClick.RemoveAllListeners();

            _fightWindowView.IncreasePowerButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreasePowerButton.onClick.RemoveAllListeners();

            _fightWindowView.IncreaseMoneyButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreaseMoneyButton.onClick.RemoveAllListeners();

            _fightWindowView.FightButton.onClick.RemoveAllListeners();

            _health.DetachEnemy(_enemy);
            _power.DetachEnemy(_enemy);
            _money.DetachEnemy(_enemy);
            
            base.OnDispose();
        }
    }
}