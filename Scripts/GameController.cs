using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] powerups;
    public Vector3 spawnValues;
    public int powerupCount;
    public int hazardCount;
    public float spawnWait;
    public float powerupspawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    private bool restart;
    private bool gameOver;
    private int score;

    public AudioSource musicSource;
    public AudioClip WinClip;
    public AudioClip LoseClip;

    [SerializeField] private Text uitext;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;


    private float mScore;


    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        timer = mainTimer;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uitext.text = timer.ToString("F");
        }

        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uitext.text = "0.00";
            timer = 0.0f;
            gameOver = true;
            restart = true;
        }



        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene("MainSS");
            }
        }

        if (Input.GetKeyDown("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            for (int i = 0; i < powerupCount; i++)
            {
                GameObject powerup = powerups[Random.Range(0, powerups.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(powerup, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(powerupspawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'K' for Restart";
                restart = true;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        mScore = score;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 200)
        {
            winText.text = "You Win! Game Created by Colin Hayes";
            musicSource.clip = WinClip;
            musicSource.Play();
            gameOver = true;
            restart = true;
                canCount = false;
                doOnce = true;
                timer = 0.0f;

        }
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        musicSource.clip = LoseClip;
        musicSource.Play();
        gameOver = true;
        canCount = false;
    }

    public float CurrentScore
    {
        get { return mScore; }
    }
}

