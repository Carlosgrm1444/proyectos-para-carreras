using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class GameFlow : MonoBehaviour
    {
        public static GameFlow Instance { get; private set; }

        [SerializeField] private MazeBallController ball;
        [SerializeField] private int startingLives = 3;
        [SerializeField] private int starPoints = 100;
        [SerializeField] private int goalPoints = 200;
        [SerializeField] private int maxSpeedBonus = 100;

        public int Lives { get; private set; }
        public int Score { get; private set; }
        public float ElapsedTime { get; private set; }
        public GameState State { get; private set; } = GameState.Playing;
        public int StarsCollected { get; private set; }
        public int BestScore => PlayerPrefs.GetInt("LIIA_BEST_SCORE", 0);
        public float BestTime => PlayerPrefs.GetFloat("LIIA_BEST_TIME", 0f);

        public void Configure(MazeBallController player, int lives = 3)
        {
            ball = player;
            startingLives = lives;
            Lives = startingLives;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            Lives = startingLives;
        }

        private void Update()
        {
            if (State == GameState.Playing) ElapsedTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R)) RestartAttempt();
        }

        public void ReachGoal()
        {
            if (State != GameState.Playing) return;
            Score += goalPoints;
            Score += Mathf.Clamp(maxSpeedBonus - Mathf.RoundToInt(ElapsedTime), 0, maxSpeedBonus);
            State = GameState.Won;
            SaveRecord();
        }

        public void RestartAttempt()
        {
            Lives = startingLives;
            Score = 0;
            StarsCollected = 0;
            ElapsedTime = 0f;
            State = GameState.Playing;
            ball.RespawnStart();
        }

        private void SaveRecord()
        {
            if (Score > BestScore || (Score == BestScore && (BestTime <= 0f || ElapsedTime < BestTime)))
            {
                PlayerPrefs.SetInt("LIIA_BEST_SCORE", Score);
                PlayerPrefs.SetFloat("LIIA_BEST_TIME", ElapsedTime);
                PlayerPrefs.Save();
            }
        }
    }

    public enum GameState { Playing, Won }
}
