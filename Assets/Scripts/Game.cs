using System;
using DefaultNamespace;
using Level.Character;
using Level.Coins;
using Level.Input;
using UI;
using UI.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Game: MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private InputService _input;
    [SerializeField] private LevelConfig[] _levelConfig;
    [SerializeField] private UIController _uiController;

    private GameObject _player;
    private CoinsController _coins;
    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _uiController.OnSaveButton -= SavePosition;
        _coins.OnLevelEnd -= SetNextLevelConfig;
    }

    private void Init()
    {
        Instantiate(_levelConfig[Profile.CurrentLevel].levelBasePrefab);
        Instantiate(_levelConfig[Profile.CurrentLevel].levelObstacles);
        
        _coins = Instantiate(_levelConfig[Profile.CurrentLevel].levelCollectables).GetComponent<CoinsController>();
        _coins.Init(Profile.GetLevelCoinsData(Profile.CurrentLevel), _uiController);
        _coins.OnLevelEnd += SetNextLevelConfig;
        
        _player = Instantiate(_character, Profile.Position, Quaternion.identity);
        _player.GetComponent<CharacterMovement>().Init(_input);

        _uiController.OnSaveButton += SavePosition;
        _uiController.Init(Profile.GetLevelCoinsData(Profile.CurrentLevel));
    }

    private void SavePosition()
    {
        Profile.Position = _player.transform.position;
        Profile.SaveLevelProgress();
    }

    private void SetNextLevelConfig()
    {
        _uiController.OnLevelEnd();

        if (Profile.CurrentLevel == 0)
        {
            Profile.CurrentLevel = 1;
            Profile.Position = _levelConfig[1].startPosition;
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("GAME OVER");
        }
    }
}