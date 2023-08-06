using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLoopManager : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Vector3 _obstacleSpawnPosition;
    [SerializeField] private GameObject _spikePrefab;
    [SerializeField] private Vector3 _spikeSpawnPosition;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _playerSpawnPosition;
    [SerializeField] private GameObject _playerPipesPrefab;
    [SerializeField] private Vector3 _playerPipesSpawnPosition;
    [SerializeField] private Sprite _playerWithPipesSprite;
    [SerializeField] private float _playerGracePeriod;
    [Space]
    [SerializeField][Range(0, 20)] private float _obstacleSpawnDelay;
    [SerializeField][Range(0, 20)] private float _obstacleSpawnDelayVariance;
    [SerializeField][Range(0, 20)] private float _spikeSpawnDelay;
    [SerializeField][Range(0, 20)] private float _spikeSpawnDelayVariance;
    [Space]
    [SerializeField][Range(0, 20)] private float _levelDuration;

    private bool _shouldSpawnObstacles;
    private float _timeSinceLastObstacleSpawn;
    private float _nextObstacleSpawnDelay;
    private float _timeSinceLastSpikeSpawn;
    private float _nextSpikeSpawnDelay;

    private void Awake()
    {
        var player = Instantiate(_playerPrefab, _playerSpawnPosition, _playerPrefab.transform.rotation);
        player.GetComponent<Collector>().OnCollected += PipesCollected;
        player.GetComponent<Killable>().OnKilled += PlayerKilled;
        StartCoroutine(LevelTimer());
        StartCoroutine(PlayerSpawnGracePeriod());
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (_shouldSpawnObstacles)
        {
            SpawnObstacles();
        }
    }

    private void SpawnObstacles()
    {
        _timeSinceLastObstacleSpawn += Time.deltaTime;
        if (_timeSinceLastObstacleSpawn > _nextObstacleSpawnDelay)
        {
            Instantiate(_obstaclePrefab, _obstacleSpawnPosition, _obstaclePrefab.transform.rotation);
            _timeSinceLastObstacleSpawn = 0;
            _nextObstacleSpawnDelay = _obstacleSpawnDelay + Random.Range(-_obstacleSpawnDelayVariance, _obstacleSpawnDelayVariance);
        }

        _timeSinceLastSpikeSpawn += Time.deltaTime;
        if (_timeSinceLastSpikeSpawn > _nextSpikeSpawnDelay)
        {
            Instantiate(_spikePrefab, _spikeSpawnPosition, _spikePrefab.transform.rotation);
            _timeSinceLastSpikeSpawn = 0;
            _nextSpikeSpawnDelay = _spikeSpawnDelay + Random.Range(-_spikeSpawnDelayVariance, _spikeSpawnDelayVariance);
        }
    }

    private IEnumerator PlayerSpawnGracePeriod()
    {
        yield return new WaitForSeconds(_playerGracePeriod);
        _shouldSpawnObstacles = true;
    }

    private IEnumerator LevelTimer()
    {
        yield return new WaitForSeconds(_levelDuration);
        _shouldSpawnObstacles = false;
        yield return new WaitForSeconds(_playerGracePeriod);
        SpawnPipes();
    }

    private void SpawnPipes()
    {
        Instantiate(_playerPipesPrefab, _playerPipesSpawnPosition, _playerPipesPrefab.transform.rotation);
    }

    private void PipesCollected(GameObject player, GameObject pipes)
    {
        Destroy(pipes);
        player.GetComponent<SpriteRenderer>().sprite = _playerWithPipesSprite;
    }

    private void PlayerKilled(GameObject player)
    {

    }
}
