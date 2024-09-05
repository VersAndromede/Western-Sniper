using System;
using VContainer;

namespace Scripts.ShootingSystem.AmmunitionSystem
{
    public class AmmunitionPresenter : IDisposable
    {
        private readonly Ammunition _ammunition;
        private readonly AmmunitionView _ammunitionView;

        [Inject]
        public AmmunitionPresenter(Ammunition ammunition, AmmunitionView ammunitionView)
        {
            _ammunition = ammunition;
            _ammunitionView = ammunitionView;

            _ammunition.Changed += OnChanged;
        }

        public void Dispose()
        {
            _ammunition.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _ammunitionView.UpdateIcons(_ammunition.Count, _ammunition.MaxCount);
        }
    }
}