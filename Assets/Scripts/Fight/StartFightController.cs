using Profile;
using UnityEngine;

namespace AI
{
    public class StartFightController : BaseController
    {
        private StartFightView _startFightView;
        private ProfilePlayer _profilePlayer;

        public StartFightController(Transform placeForUi, StartFightView startFightView, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _startFightView = Object.Instantiate(startFightView, placeForUi);
            AddGameObjects(_startFightView.gameObject);
        }

        public void RefreshView()
        {
            _startFightView.StartFightButton.onClick.AddListener(StartFight);
        }

        private void StartFight()
        {
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }

        protected override void OnDispose()
        {
            _startFightView.StartFightButton.onClick.RemoveAllListeners();
            base.OnDispose();
        }
    }
}