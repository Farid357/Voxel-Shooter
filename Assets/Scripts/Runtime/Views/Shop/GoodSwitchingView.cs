﻿using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Shop;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class GoodSwitchingView : SerializedMonoBehaviour, IGoodSwitchingView
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Dictionary<Currency, Sprite> _paymentSystemsSprites;
        [SerializeField] private Image _paymentSystemImage;
        private IReadOnlyDictionary<Currency, IShoppingCart> _shoppingCarts;
        
        [field: SerializeField] public GoodInContentView GoodView { get; private set; }

        public void Init(IReadOnlyDictionary<Currency, IShoppingCart> shoppingCarts)
        {
            _shoppingCarts = shoppingCarts ?? throw new ArgumentNullException(nameof(shoppingCarts));
        }
        
        public void Switch(IGood good)
        {
            var currency = good.Data.Currency;
            
            if (_shoppingCarts.ContainsKey(currency) == false)
                throw new ArgumentOutOfRangeException(nameof(currency));

            _paymentSystemImage.sprite = _paymentSystemsSprites[currency];
            _priceText.text = good.Data.Price.ToString();
            _nameText.text = good.Data.Name;
            GoodView.Visualize(good.Data);
            GoodView.SelectingButton.DeleteAllSubscribers();
            GoodView.SelectingButton.Subscribe(new SelectingGoodButtonAction(good, _shoppingCarts[currency], GoodView.SelectingButton));
        }
    }
}