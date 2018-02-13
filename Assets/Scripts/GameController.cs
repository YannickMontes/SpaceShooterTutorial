using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;

    public GameObject hazard;
    public Vector3 spawnValues;
    public float timeBeforeWavesStart;
    public float spawnHazardWait;
    public float wavesWait;

    public Text scoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;

    private bool restart;
    private bool gameOver;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public static GameController GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        InitGameController();
    }

    private void InitGameController()
    {
        score = 0;
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                InitGameController();
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(timeBeforeWavesStart);
        while (true)
        {
            int hazardCount = Random.Range(5, 15);
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnHazardWait);
            }
            yield return new WaitForSeconds(wavesWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int value)
    {
        this.score += value;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!!";
        gameOver = true;
        restart = true;
        StopCoroutine(SpawnWaves());
    }
}
