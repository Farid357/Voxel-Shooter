﻿using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class HandWeapon : MonoBehaviour, IShootingWeapon
    {
        [SerializeField, Min(0.01f)] private float _attackDistance = 1.5f;
        [SerializeField, Min(0.01f)] private float _attackCooldown = 1.5f;
        [SerializeField, ProgressBar(0, 100, 1, 0, 0)] private int _damage = 50;
        [SerializeField] private AttackAnimation _attackAnimation;
        
        private float _time;
        private AudioSource _sliceAudio;
        private CharacterMovement _character;

        public bool CanShoot => _time == 0;
        
        private void Awake() => _time = _attackCooldown;

        private void Update() => _time = Mathf.Max(0, _time - Time.deltaTime);

        public void Init(AudioSource sliceAudio, CharacterMovement character)
        {
            _sliceAudio = sliceAudio ?? throw new ArgumentNullException(nameof(sliceAudio));
            _character = character ?? throw new ArgumentNullException(nameof(character));
        }

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(CanShoot));

            _time = _attackCooldown;
            var ray = new Ray(transform.position, _character.transform.forward);
            _attackAnimation.Play().Forget();
            _sliceAudio.PlayOneShot(_sliceAudio.clip);
            
            if (Physics.Raycast(ray, out var hit, _attackDistance))
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out IHealthTransformView healthTransformView))
                {
                    if (healthTransformView.Health.IsAlive)
                        healthTransformView.Health.TakeDamage(_damage);
                }
            }
        }
    }
}