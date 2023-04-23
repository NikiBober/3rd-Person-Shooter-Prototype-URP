using TMPro;
using UnityEngine;

/// <summary>
/// Actions with UI on characthers death.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    private uint _score;

    public void UpdateScore()
    {
        _score++;
        _scoreText.SetText(_score.ToString());
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        _scoreText.gameObject.SetActive(false);
        _finalScoreText.SetText(_score.ToString());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
