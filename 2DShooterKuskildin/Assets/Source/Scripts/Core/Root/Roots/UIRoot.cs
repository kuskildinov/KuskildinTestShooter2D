using UnityEngine;
using UnityEngine.UIElements;

public class UIRoot : Singleton<UIRoot>
{
    [SerializeField] private UIDocument _document;

    [SerializeField] private VisualTreeAsset _mainMenuAsset;
    [SerializeField] private VisualTreeAsset _gameAsset;
    [SerializeField] private VisualTreeAsset _gameOverAsset;

    [SerializeField] private PlayerGun _playerGun;

   
    private Button _startGameButton;
    private Button _restartGameButton;
    private Button _backToMenuButton;
    private Button _settingsButton;
    private Button _exitGameButton;
   
    private ScrollView _weaponsScrollView;
    private ProgressBar _healthProgressBar;
    private Label _scoreLable;
    private Label _enmyWaveNumLabel;
    private Button _pistolButton;
    private Button _revolverButton;
    private Button _shootgunButton;
    private Button _weaponPanelButton;

    private bool _weaponPanelIsActive = false;

    public void Awake()
    {
        LevelRoot.Instance.StopGame();
        _document.visualTreeAsset = _mainMenuAsset;
        var root = _document.rootVisualElement;
        _startGameButton = root.Q<Button>("StartButton");
        _startGameButton.clicked += StartGame;
        _exitGameButton = root.Q<Button>("ExitGameButton");
        _exitGameButton.clicked += ExitGame;
    }   

    private void StartGame()
    {
        _document.visualTreeAsset = _gameAsset;
        var root = _document.rootVisualElement;

        _weaponsScrollView = root.Q<ScrollView>("WeaponScrollView");
        _weaponsScrollView.visible = false;

        _healthProgressBar = root.Q<ProgressBar>("HealthProgressBar");
        _scoreLable = root.Q<Label>("ScoreText");
        _enmyWaveNumLabel = root.Q<Label>("EnemyWaveNum");

        _weaponPanelButton = root.Q<Button>("WeaponMenuButton");
        _weaponPanelButton.clicked += WeaponButtonClick;

        _pistolButton = root.Q<Button>("PistolButton");
        _pistolButton.clicked += () =>
        {
            _playerGun.ChangeGun(GunType.PISTOL);
            root.Q<Button>("PistolButton").SetEnabled(true);
        };

        _revolverButton = root.Q<Button>("RevolverButton");
        _revolverButton.clicked += () =>
        {
            _playerGun.ChangeGun(GunType.REVOLVER);
        };

        _shootgunButton = root.Q<Button>("ShootgunButton");
        _shootgunButton.clicked += () =>
        {
            _playerGun.ChangeGun(GunType.SHOTGUN);
        };

        LevelRoot.Instance.StartGame();
    }

    public void GameOver()
    {
        _document.visualTreeAsset = _gameOverAsset;
        var root = _document.rootVisualElement;
        _scoreLable = root.Q<Label>("ScoreText");
        _scoreLable.text = ScoreRoot.Instance.TotalScore.ToString();

        _enmyWaveNumLabel = root.Q<Label>("EnemyWaveNum");
        _enmyWaveNumLabel.text = LevelRoot.Instance.EnemyWaveNum.ToString();

        _restartGameButton = root.Q<Button>("RestartLevelButton");
        _restartGameButton.clicked += RestartGame;

        _backToMenuButton = root.Q<Button>("BackMenuButton");
        _restartGameButton.clicked += RestartGame;

        _exitGameButton.clicked -= ExitGame;
        _exitGameButton = root.Q<Button>("ExitGameButton");
        _exitGameButton.clicked += ExitGame;
    }

    public void SetHpToHealthBar(float hp)
    {        
        _healthProgressBar.SetValueWithoutNotify(hp);
    }

    public void SetScoreToScorePanel(float score)
    {
        _scoreLable.text = score.ToString();
    }

    public void SetNewEnemyWaveToPanel(int waveNum)
    {
        _enmyWaveNumLabel.text = $"Волна #{waveNum}";
    }

    private void WeaponButtonClick()
    {
        if(_weaponPanelIsActive)
        {
            LevelRoot.Instance.ResumeGame();
            _weaponsScrollView.visible = false;
            _weaponPanelIsActive = false;
        }
        else
        {
            LevelRoot.Instance.StopGame();
            _weaponsScrollView.visible = true;
            _weaponPanelIsActive = true;
        }
    }

    private void RestartGame()
    {
        _startGameButton.clicked -= StartGame;
        _exitGameButton.clicked -= ExitGame;
        _weaponPanelButton.clicked -= WeaponButtonClick;
        _restartGameButton.clicked -= RestartGame;
        LevelRoot.Instance.RestartLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
