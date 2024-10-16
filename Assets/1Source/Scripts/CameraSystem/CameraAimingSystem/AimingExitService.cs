﻿using Cysharp.Threading.Tasks;
using Scripts.UI;
using UnityEngine;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class AimingExitService : MonoBehaviour
    {
        [SerializeField] private CameraAiming _cameraAiming;
        [SerializeField] private AnimatedUI _aimingScreen;
        [SerializeField] private AnimatedButton _exitAimingButton;

        public async UniTask Exit()
        {
            _aimingScreen.Hide();
            HideButton(_exitAimingButton);
            await _cameraAiming.EndAim();
        }

        public void HideButton(AnimatedButton animatedButton)
        {
            animatedButton.Lock();
            animatedButton.Hide();
        }

        public void ShowButton(AnimatedButton animatedButton)
        {
            animatedButton.Unlock();
            animatedButton.Show();
        }

        public void Activate(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }


        public void Deactivate(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}