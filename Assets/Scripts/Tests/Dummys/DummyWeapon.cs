﻿using Shooter.Model;

namespace Shooter.Test
{
    public sealed class DummyWeapon : IWeapon
    {
        public void Shoot()
        {
        }

        public int StartBullets { get; }

        public void VisualizeBullets()
        {
            
        }

        public bool CanShoot => true;

        public void AddBullets(int bullets)
        {
        }

        public int Bullets { get; }
    }
}