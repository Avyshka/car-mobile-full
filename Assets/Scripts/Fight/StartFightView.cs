using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class StartFightView : MonoBehaviour
    {
        [SerializeField] private Button _startFightButton;

        public Button StartFightButton => _startFightButton;
    }
}