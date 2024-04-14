using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRoot : Singleton<LevelRoot>
{
    [SerializeField] private EnemySpawner _enemySpawner;
    private int _enemyWaveNum;   
    private int _currentEnemyCount;

    public int EnemyWaveNum => _enemyWaveNum;
   public void StartGame()
    {
        Time.timeScale = 1;

        _enemyWaveNum = 1;
        UIRoot.Instance.SetNewEnemyWaveToPanel(_enemyWaveNum);

        _enemySpawner.Spawn(_enemyWaveNum);
        UIRoot.Instance.SetNewEnemyWaveToPanel(_enemyWaveNum);
    }

    public void EnemyIsDead()
    {
        _currentEnemyCount--;
        if(_currentEnemyCount == 0)
        {
            StartNewWave();
        }
    }

    public void StartNewWave()
    {
        _enemySpawner.Enemys.Clear();
        _enemyWaveNum++;
        _enemySpawner.Spawn(_enemyWaveNum);
        _currentEnemyCount = _enemySpawner.Enemys.Count;
        UIRoot.Instance.SetNewEnemyWaveToPanel(_enemyWaveNum);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
