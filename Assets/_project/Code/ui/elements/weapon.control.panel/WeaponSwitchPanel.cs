using System.Collections;
using _project.Code.ui.@base;
using _project.Code.weapon;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Code.ui.elements.weapon.control.panel
{
    public class WeaponSwitchPanel : UiElement
    {
        [SerializeField] private Button[] _switchButtons;
        [SerializeField] private Image[] _cooldownViews;

        private WeaponHandle _weaponHandle;

        private void Start()
        {
            foreach (var view in _cooldownViews)
            {
                view.gameObject.SetActive(false);
            }
        }

        protected override void OnShow()
        {
            _weaponHandle = Container.Resolve<WeaponHandle>();

            for (int i = 0; i < _switchButtons.Length; i++)
            {
                var index = i;
                _switchButtons[i].onClick
                    .AddListener(() => _weaponHandle.ActivateWeapon(index));
            }

            foreach (var weapon in _weaponHandle.Weapons)
            {
                weapon.ReloadStarted += OnReloadStarted;
            }
        }

        protected override void OnHide()
        {
            foreach (var button in _switchButtons)
            {
                button.onClick.RemoveAllListeners();
            }
            
            foreach (var weapon in _weaponHandle.Weapons)
            {
                weapon.ReloadStarted -= OnReloadStarted;
            }
        }

        private void OnReloadStarted(int weaponIndex, float reloadTime)
        {
            StartCoroutine(ShowCooldown(weaponIndex, reloadTime));
        }

        private IEnumerator ShowCooldown(int weaponIndex, float reloadTime)
        {
            _cooldownViews[weaponIndex].gameObject.SetActive(true);
            _cooldownViews[weaponIndex].fillAmount = 0;

            while (_cooldownViews[weaponIndex].fillAmount < 1)
            {
                _cooldownViews[weaponIndex].fillAmount += Time.deltaTime * (1 / reloadTime);
                yield return null;
            }
            
            _cooldownViews[weaponIndex].gameObject.SetActive(false);
        }
    }
}