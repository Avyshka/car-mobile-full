using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class FightWindowView : MonoBehaviour
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

        public TMP_Text CountHealthText => _countHealthText;

        public TMP_Text CountPowerText => _countPowerText;
        
        public TMP_Text CountMoneyText => _countMoneyText;

        public TMP_Text CountPowerEnemyText => _countPowerEnemyText;

        public Button IncreaseHealthButton => _increaseHealthButton;

        public Button DecreaseHealthButton => _decreaseHealthButton;

        public Button IncreasePowerButton => _increasePowerButton;

        public Button DecreasePowerButton => _decreasePowerButton;

        public Button IncreaseMoneyButton => _increaseMoneyButton;

        public Button DecreaseMoneyButton => _decreaseMoneyButton;

        public Button FightButton => _fightButton;
    }
}