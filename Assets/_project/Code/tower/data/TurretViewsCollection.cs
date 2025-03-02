using System;
using _project.Code.tower.enums;
using _project.Code.utility.SampleSelection;
using UnityEngine;

namespace _project.Code.tower.data
{
    [CreateAssetMenu(fileName = "TurretViewsCollection", menuName = "Data/Turret/TurretViewsCollection")]
    public class TurretViewsCollection : ScriptableObject
    {
        [SerializeField] private TurretViewConfig[] _turretConfigs;

        public bool HasViewConfig(TurretViewKind turretViewKind, out DefenseHandle defenseHandle)
        {
            if (SampleSelection.HasSampleByIndex(_turretConfigs, (int)turretViewKind, out TurretViewConfig viewConfig))
            {
                defenseHandle = Instantiate(viewConfig.defenseHandlePrefab);
            }

            defenseHandle = null;
            return false;
        }
    }
    
    [Serializable]
    public class TurretViewConfig : ISampleSelectable
    {
        public TurretViewKind TurretViewKind;
        public DefenseHandle defenseHandlePrefab;
        
        public int Index => (int)TurretViewKind;
    }
}