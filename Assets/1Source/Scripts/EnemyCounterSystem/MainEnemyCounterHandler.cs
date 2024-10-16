﻿using Scripts.EnemySystem;
using UnityEngine;

namespace EnemyCounterSystem
{
    public class MainEnemyCounterHandler : MonoBehaviour
    {
        [SerializeField] private EnemyCounterView _view;
        [SerializeField] private MainEnemy _mainTarget;

        private void Start()
        {
            _mainTarget.Died += OnDied;
        }

        private void OnDestroy()
        {
            _mainTarget.Died -= OnDied;
        }

        private void OnDied()
        {
            _view.UpdateCount(1, 1);
        }
    }
}