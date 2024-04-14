public class ScoreRoot : Singleton<ScoreRoot>
{
    private float _totalScore;

    public float TotalScore { get => _totalScore; set => _totalScore = value; }

    private void Start()
    {
        _totalScore = 0;        
    }

    public void Add(float value)
    {
        _totalScore += value;
        UIRoot.Instance.SetScoreToScorePanel(_totalScore);
    }
}
