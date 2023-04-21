using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private uint _score;


    public void UpdateScore()
    {
        _score++;
        _scoreText.SetText("You kill" + _score);
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
    }
}
