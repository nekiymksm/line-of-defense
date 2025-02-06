using System.Collections;
using System.Collections.Generic;
using _project.Code.data.hold;
using _project.Code.enemy.data;
using _project.Code.enemy.enemies.@base;
using _project.Code.session.result;
using _project.Code.session.state.@base;
using _project.Code.turret;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _project.Code.enemy
{
    public class EnemySpawner : MonoBehaviour, IStateObserver
    {
        private TurretTower _turretTower;
        private DataHolder _dataHolder;
        private SessionResultHandler _sessionResultHandler;
        
        private EnemiesCollection _enemiesCollection;
        private EnemiesSpawnConfig _enemiesSpawnConfig;
        
        private float minValue;
        private float maxValue;
        private float topValue;

        private bool isSpawn;
        private List<Enemy> _enemies;

        [Inject]
        private void Construct(TurretTower turretTower, DataHolder dataHolder, Camera viewCamera, 
            SessionResultHandler sessionResultHandler)
        {
            _turretTower = turretTower;
            _dataHolder = dataHolder;
            _sessionResultHandler = sessionResultHandler;
            
            minValue = viewCamera.ScreenToWorldPoint(Vector3.zero).x;
            maxValue = viewCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            
            var topPoint = viewCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            topValue = topPoint.y + topPoint.z;
        }

        private void Awake()
        {
            _enemiesCollection = _dataHolder.GetData<EnemiesCollection>();
            _enemiesSpawnConfig = _dataHolder.GetData<EnemiesSpawnConfig>();
            
            isSpawn = false;
        }

        public void OnStart()
        {
            isSpawn = true;
            _enemies = new List<Enemy>();
            StartCoroutine(Spawn());
        }

        public void OnEnd()
        {
            isSpawn = false;
            StopAllCoroutines();

            foreach (var enemy in _enemies)
            {
                enemy.Die(true);
            }

            _enemies.Clear();
        }

        private IEnumerator Spawn()
        {
            while (isSpawn)
            {
                var enemyConfig = _enemiesCollection.Get();
                
                var enemy = Instantiate(
                    enemyConfig.EnemyPrefab, 
                    new Vector3(Random.Range(minValue, maxValue), 0, topValue), 
                    Quaternion.identity);

                var enemyTargetPosition = new Vector3(
                    Random.Range(minValue, maxValue), 
                    enemy.transform.position.y, 
                    _turretTower.DefenseArea.transform.position.z);
                
                enemy.Initialize(
                    enemyConfig, 
                    enemyTargetPosition,
                    _sessionResultHandler.AddPoints,
                    () => _enemies.Remove(enemy));
            
                _enemies.Add(enemy);
                
                yield return new WaitForSeconds(_enemiesSpawnConfig.GetSpawnDelay());
            }
        }
    }
}