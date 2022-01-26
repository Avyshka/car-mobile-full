using AI;
using Game;
using Profile;
using Rewards;
using Ui;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private DailyRewardController _dailyRewardController;
    private CurrencyController _currencyController;
    private StartFightController _startFightController;
    private FightWindowController _fightWindowController;
    
    private readonly DailyRewardView _dailyRewardView;
    private readonly CurrencyView _currencyView;
    private readonly StartFightView _startFightView;
    private readonly FightWindowView _fightWindowView;
    
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    
    public MainController(
        Transform placeForUi,
        ProfilePlayer profilePlayer,
        DailyRewardView dailyRewardView,
        CurrencyView currencyView,
        StartFightView startFightView,
        FightWindowView fightWindowView
    )
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;

        _dailyRewardView = dailyRewardView;
        _currencyView = currencyView;
        _startFightView = startFightView;
        _fightWindowView = fightWindowView;
        
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        DisposeAllControllers();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _dailyRewardController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                _startFightController = new StartFightController(_placeForUi, _startFightView, _profilePlayer);
                _startFightController.RefreshView();
                
                _mainMenuController?.Dispose();
                _fightWindowController?.Dispose();
                break;
            case GameState.DailyRewards:
                _dailyRewardController =
                    new DailyRewardController(_placeForUi, _dailyRewardView, _currencyView, _profilePlayer);
                _dailyRewardController.RefreshView();
                
                _mainMenuController?.Dispose();
                break;
            case GameState.Fight:
                _fightWindowController = new FightWindowController(_placeForUi, _fightWindowView, _profilePlayer);
                _fightWindowController.RefreshView();
                
                _startFightController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _dailyRewardController?.Dispose();
        _startFightController?.Dispose();
        _fightWindowController?.Dispose();
    }
}