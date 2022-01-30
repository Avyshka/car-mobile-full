using UnityEngine;

namespace Rewards
{
    public class CurrencyController : BaseController
    {
        private CurrencyView _currencyView;

        public CurrencyController(Transform placeForUi, CurrencyView currencyView)
        {
            _currencyView = Object.Instantiate(currencyView, placeForUi);
            AddGameObjects(_currencyView.gameObject);
        }
    }
}