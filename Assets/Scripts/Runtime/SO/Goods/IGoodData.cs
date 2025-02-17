﻿using Shooter.Model;
using UnityEngine;

namespace Shooter.Shop
{
    public interface IGoodData
    {
        string Name { get; }

        Sprite Sprite { get; }
        
        int Price { get; }
        
        Currency Currency { get; }
    }
}