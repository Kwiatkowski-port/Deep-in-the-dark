using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int scoreValue;
    public int highScore;

    public Text scoreText;
    public Text highScoreText;

    public AudioClip deadSound;
    AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        instance = this;

        scoreText.text = "Points: " + scoreValue.ToString();
        highScoreText.text = "Record: 0" + scoreValue.ToString();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "Record: " + highScore.ToString();
        }
    }

    public void AddScore()
    {
        scoreValue += 3;

        source.clip = deadSound;
        source.Play();

        scoreText.text = "Points: " + scoreValue.ToString();
    }

    public void UpdateHightScore()
    {
        if(scoreValue > highScore)
        {
            highScore = scoreValue;
            highScoreText.text = "Record: " + highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void DeleteHightScore()
    {
        PlayerPrefs.DeleteKey("HightScore");
        highScore = 0;
        highScoreText.text = "Record: " + highScore.ToString();
    }
}
