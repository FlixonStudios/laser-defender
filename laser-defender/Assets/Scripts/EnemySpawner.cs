using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    //[SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
            //yield waits until it is finished;
        }
        while (looping);
        
    }
    private IEnumerator SpawnAllWaves()
    {
        foreach (WaveConfig currentWave in waveConfigs)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
        //yield return new StartCou
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount =1; enemyCount <= waveConfig.NumberOfEnemies; enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyObject(),
                                        waveConfig.GetWaypoints()[0].transform.position,
                                        Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            //Since the wave is alr set, we will use this wave information to tell the enemy what path it should follow
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
        
    }
}
