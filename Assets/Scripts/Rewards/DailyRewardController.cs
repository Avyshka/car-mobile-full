using System;
using System.Collections;
using System.Collections.Generic;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    public class DailyRewardController : BaseController
    {
        private readonly DailyRewardView _dailyRewardView;
        private List<SlotRewardContainerView> _slots;
        private CurrencyController _currencyController;
        private ProfilePlayer _profilePlayer;

        private bool _isGetReward;

        public DailyRewardController(
            Transform placeForUi,
            DailyRewardView dailyRewardView,
            CurrencyView currencyView,
            ProfilePlayer profilePlayer
        )
        {
            _profilePlayer = profilePlayer;
            
            _dailyRewardView = Object.Instantiate(dailyRewardView, placeForUi);
            AddGameObjects(_dailyRewardView.gameObject);
            
            _currencyController = new CurrencyController(placeForUi, currencyView);
            AddController(_currencyController);
        }

        public void RefreshView()
        {
            InitSlots();
            _dailyRewardView.StartCoroutine(RewardsStateUpdater());
            RefreshUi();
            SubscribeButtons();
        }

        private void InitSlots()
        {
            _slots = new List<SlotRewardContainerView>();
            for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                var slot = GameObject.Instantiate(
                    _dailyRewardView.SlotRewardViewContainer,
                    _dailyRewardView.SlotRewardsContainer,
                    false
                );
                slot.Show(i * 0.1f + 0.5f);
                _slots.Add(slot);
            }
        }

        private IEnumerator RewardsStateUpdater()
        {
            while (true)
            {
                RefreshRewardsState();
                yield return new WaitForSeconds(1);
            }
        }

        private void RefreshRewardsState()
        {
            _isGetReward = true;
            if (_dailyRewardView.TimeGetReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;
                if (timeSpan.TotalSeconds > _dailyRewardView.TimeDeadline)
                {
                    _dailyRewardView.TimeGetReward = null;
                    _dailyRewardView.CurrentSlotInActive = 0;
                }
                else if (timeSpan.TotalSeconds < _dailyRewardView.TimeCooldown)
                {
                    _isGetReward = false;
                }
            }

            RefreshUi();
        }

        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;
            _dailyRewardView.ProgressBar.gameObject.SetActive(!_isGetReward);
            if (_isGetReward)
            {
                _dailyRewardView.TimerNewReward.text = "The reward is received today";
                _slots[_dailyRewardView.CurrentSlotInActive].StartIconAnimation();
            }
            else
            {
                if (_dailyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward =
                        $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                    _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
                    _dailyRewardView.ProgressBar.value =
                        1 - (float) currentClaimCooldown.TotalSeconds / _dailyRewardView.TimeCooldown;
                }
            }

            for (var i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotInActive);
            }
        }

        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
            _dailyRewardView.CloseButton.onClick.AddListener(CloseWindow);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
            {
                return;
            }

            _slots[_dailyRewardView.CurrentSlotInActive].StopIconAnimation();

            var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];
            CurrencyView.Instance.SetCurrency(reward.RewardType, reward.Value);

            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.CurrentSlotInActive =
                (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;

            RefreshRewardsState();
        }

        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
            CurrencyView.Instance.SetCurrency(RewardType.Food, 0);
            CurrencyView.Instance.SetCurrency(RewardType.Wood, 0);
            CurrencyView.Instance.SetCurrency(RewardType.Gold, 0);
            CurrencyView.Instance.SetCurrency(RewardType.Diamonds, 0);
        }

        private void CloseWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            GameObject.Destroy(_dailyRewardView.gameObject);
        }

        protected override void OnDispose()
        {
            _dailyRewardView.GetRewardButton.onClick.RemoveAllListeners();
            _dailyRewardView.ResetButton.onClick.RemoveAllListeners();
            _dailyRewardView.CloseButton.onClick.RemoveAllListeners();
            base.OnDispose();
        }
    }
}