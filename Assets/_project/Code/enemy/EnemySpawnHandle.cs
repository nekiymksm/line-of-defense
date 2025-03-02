using System.Collections;
using System.Collections.Generic;
using _project.Code.data.hold;
using _project.Code.enemy.data;
using _project.Code.enemy.enemies.@base;
using _project.Code.session.result;
using _project.Code.session.state.@base;
using _project.Code.tower;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _project.Code.enemy
{
    public class EnemySpawnHandle : MonoBehaviour, IStateObserver
    {
        private DefenseHandle _defenseHandle;
        private SessionResultsHandle _sessionResultsHandle;
        
        private EnemiesCollection _enemiesCollection;
        private EnemiesSpawnConfig _enemiesSpawnConfig;
        
        private float minValue;
        private float maxValue;
        private float topValue;

        private bool isSpawn;
        private List<Enemy> _enemies;

        [Inject]
        private void Construct(DefenseHandle defenseHandle, DataCollection dataCollection, Camera viewCamera, 
            SessionResultsHandle sessionResultsHandle)
        {
            _defenseHandle = defenseHandle;
            _sessionResultsHandle = sessionResultsHandle;
            _enemiesCollection = dataCollection.GetData<EnemiesCollection>();
            _enemiesSpawnConfig = dataCollection.GetData<EnemiesSpawnConfig>();
            
            minValue = viewCamera.ScreenToWorldPoint(Vector3.zero).x;
            maxValue = viewCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            
            var topPoint = viewCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            topValue = topPoint.y + topPoint.z;
            
            isSpawn = false;
        }

        public void OnStart()
        {
            isSpawn = true;
            _enemies = new List<Enemy>();
            
            StartCoroutine(Spawn());
        }

        public void OnPause()
        {
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
                var spawnPoint = new Vector3(Random.Range(minValue, maxValue), 0, topValue);
                var enemyTargetPosition = new Vector3();
                var hasTargetPoint = false;

                var enemy = Instantiate(
                    enemyConfig.EnemyPrefab, 
                    new Vector3(Random.Range(minValue, maxValue), 0, topValue), 
                    transform.rotation);
                
                while (hasTargetPoint == false)
                {
                    enemyTargetPosition = new Vector3(
                        Random.Range(minValue, maxValue), 
                        enemy.transform.position.y, 
                        _defenseHandle.DefenseArea.transform.position.z);
                    
                    Ray ray = new Ray(spawnPoint, enemyTargetPosition - spawnPoint);
                    
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        hasTargetPoint = hit.collider.TryGetComponent(out Turret turret) == false;
                    }
                    
                    yield return null;
                }

                enemy.Initialize(
                    enemyConfig, 
                    enemyTargetPosition,
                    _sessionResultsHandle.AddPoints,
                    () => _enemies.Remove(enemy));
            
                _enemies.Add(enemy);
                
                yield return new WaitForSeconds(_enemiesSpawnConfig.GetSpawnDelay());
            }
        }
    }
}