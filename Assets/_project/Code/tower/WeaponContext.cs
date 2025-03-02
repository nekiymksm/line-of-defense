using UnityEngine;

namespace _project.Code.tower
{
    public class WeaponContext : MonoBehaviour
    {
        [SerializeField] private Transform _pointerTransform;
        
        public void SetPointer(bool isShow)
        {
            _pointerTransform.gameObject.SetActive(isShow);
        }
    }
}