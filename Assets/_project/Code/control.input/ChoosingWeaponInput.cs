using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.ModuleInput
{
    public class ChoosingWeaponInput : MonoBehaviour
    {
        [SerializeField] private Button[] _weaponButtons;
        [SerializeField] private Image[] _reloadImages;

        public event Action<int> ButtonChosen;
        
        private void Start()
        {
            for (int i = 0; i < _weaponButtons.Length; i++)
            {
                int number = i;
                _weaponButtons[i].onClick.AddListener((() => OnButtonClick(number)));
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _weaponButtons.Length; i++)
            {
                int number = i;
                _weaponButtons[i].onClick.RemoveListener((() => OnButtonClick(number)));
            }
        }

        public async void ShowReload(int buttonNumber, float time)
        {
            var image = _reloadImages[buttonNumber];
            
            image.gameObject.SetActive(true);
            image.fillAmount = 1;
            
            while (image.fillAmount > 0)
            {
                image.fillAmount -= 1 / time * Time.deltaTime;
                await Task.Yield();
            }
            
            image.gameObject.SetActive(false);
        }
        
        private void OnButtonClick(int buttonNumber)
        {
            ButtonChosen?.Invoke(buttonNumber);
        }
    }
}