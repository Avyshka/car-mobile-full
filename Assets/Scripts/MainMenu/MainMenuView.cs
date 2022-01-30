using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonDailyRewards;
        [SerializeField] private Button _buttonExit;
            
        public void Init(UnityAction startGame, UnityAction watchDailyRewards)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonDailyRewards.onClick.AddListener(watchDailyRewards);
            _buttonExit.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonDailyRewards.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }
}

