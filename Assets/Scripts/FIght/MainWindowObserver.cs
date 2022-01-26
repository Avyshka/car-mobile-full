using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class MainWindowObserver : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [SerializeField] private Button _increaseHealthButton;
        [SerializeField] private Button _decreaseHealthButton;

        [SerializeField] private Button _increasePowerButton;
        [SerializeField] private Button _decreasePowerButton;

        [SerializeField] private Button _increaseMoneyButton;
        [SerializeField] private Button _decreaseMoneyButton;

        [SerializeField] private Button _fightButton;

        private int _countHealthPlayerTotal;
        private int _countPowerPlayerTotal;
        private int _countMoneyPlayerTotal;

        private Health _health;
        private Power _power;
        private Money _money;

        private Enemy _enemy;

        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _health = new Health(nameof(Health));
            _power = new Power(nameof(Power));
            _money = new Money(nameof(Money));

            _health.AttachEnemy(_enemy);
            _power.AttachEnemy(_enemy);
            _money.AttachEnemy(_enemy);

            _increaseHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _decreaseHealthButton.onClick.AddListener(() => ChangeHealth(false));

            _increasePowerButton.onClick.AddListener(() => ChangePower(true));
            _decreasePowerButton.onClick.AddListener(() => ChangePower(false));

            _increaseMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _decreaseMoneyButton.onClick.AddListener(() => ChangeMoney(false));

            _fightButton.onClick.AddListener(Fight);
        }

        private void OnDestroy()
        {
            _increaseHealthButton.onClick.RemoveAllListeners();
            _decreaseHealthButton.onClick.RemoveAllListeners();

            _increasePowerButton.onClick.RemoveAllListeners();
            _decreasePowerButton.onClick.RemoveAllListeners();

            _increaseMoneyButton.onClick.RemoveAllListeners();
            _decreaseMoneyButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();

            _health.DetachEnemy(_enemy);
            _power.DetachEnemy(_enemy);
            _money.DetachEnemy(_enemy);
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
        }

        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    _countHealthText.text = $@"Player Health {countChangeData.ToString()}";
                    _health.Health = countChangeData;
                    break;
                 
                case DataType.Power:
                    _countPowerText.text = $@"Player Power {countChangeData.ToString()}";
                    _power.Power = countChangeData;
                    break;
                
                case DataType.Money:
                    _countMoneyText.text = $@"Player Money {countChangeData.ToString()}";
                    _money.Money = countChangeData;
                    break;
            }

            _countPowerEnemyText.text = $@"Enemy Power {_enemy.Power}";
        }
    }
}