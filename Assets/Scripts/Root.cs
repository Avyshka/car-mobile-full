using AI;
using Profile;
using Profile.Analytic;
using Rewards;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    [SerializeField] private DailyRewardView _dailyRewardView;
    [SerializeField] private CurrencyView _currencyView;
    [SerializeField] private StartFightView _startFightView;
    [SerializeField] private FightWindowView _fightWindowView;

    private MainController _mainController;

    private void Awake()
    {
        ProfilePlayer profilePlayer = new ProfilePlayer(15f, new UnityAnalyticTools());
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(
            _placeForUi,
            profilePlayer,
            _dailyRewardView,
            _currencyView,
            _startFightView,
            _fightWindowView
        );
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}