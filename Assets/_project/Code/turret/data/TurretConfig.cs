using UnityEngine;

namespace _project.Code.turret.data
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Data/Turret/TurretConfig")]
    public class TurretConfig : ScriptableObject
    {
        [SerializeField][Range(0, 90)] private float rotationAngleValue;

        public float GetAngleValue(float axisValue)
        {
            var lerp = Mathf.InverseLerp(0, Screen.width, axisValue);
            var angle = Mathf.Lerp(-rotationAngleValue, rotationAngleValue, lerp);
            return angle;
        }
    }
}