using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemysTemplates;
    [SerializeField] private List<Transform> _spawnPoints;

    private List<Enemy> _enemys = new List<Enemy>();
    public List<Enemy> Enemys { get => _enemys; set => _enemys = value; }
    public void Spawn(int waveNum)
    {
        for (int i = 1; i <= waveNum; i++)
        {
            for (int j = 0; j < _spawnPoints.Count; j++)
            {
                Enemy newEnemy = Instantiate(_enemysTemplates[Random.Range(0, _enemysTemplates.Count)], _spawnPoints[j].position, Quaternion.identity);
                newEnemy.Initialize();
                _enemys.Add(newEnemy);
            }
        }        
    }
}
