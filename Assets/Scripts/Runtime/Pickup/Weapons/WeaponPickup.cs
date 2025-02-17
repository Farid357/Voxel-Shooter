﻿using System;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponPickup : Pickup
    {
        private IInventory<(IWeapon, IWeaponInput)> _inventory;
        private InventorySlot<(IWeapon, IWeaponInput)> _inventorySlot;

        [field: SerializeField] public ItemData ItemData { get; private set; }
        
        public void Init(IInventory<(IWeapon, IWeaponInput)> inventory, InventorySlot<(IWeapon, IWeaponInput)> inventorySlot)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _inventorySlot = inventorySlot ?? throw new ArgumentNullException(nameof(inventorySlot));
        }

        protected override void OnPicked()
        {
            if (_inventory.IsFull == false)
            {
                _inventory.Add(_inventorySlot);
            }
        }
    }
}