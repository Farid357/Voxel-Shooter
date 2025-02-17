﻿using System;
using System.Threading.Tasks;

namespace Shooter.Model
{
    public sealed class EnemyWaves : IEnemyWaves
    {
        private readonly IEnemiesSimulation _simulation;

        public EnemyWaves(IEnemiesSimulation simulation)
        {
            _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        }

        public IReadOnlyEnemiesSimulation Simulation => _simulation;

        public async void CreateNext(EnemyWaveData wave)
        {
            for (var x = 0; x < wave.EnemyFactoriesData.Length; x++)
            {
                var data = wave.EnemyFactoriesData[x];
                
                for (var y = 0; y < data.EnemiesCount; y++)
                {
                    var enemy = data.Factory.Create();
                    _simulation.Add(enemy);
                    await Task.Delay(TimeSpan.FromSeconds(wave.CreateDelaySeconds));
                }
            }
        }
    }
}