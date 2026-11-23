using System.Collections.Generic;
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
        public string StatusMessage { get; private set; }
        public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage) && Time.unscaledTime < statusMessageUntil;

        private readonly HashSet<Vector2Int> collectedStars = new HashSet<Vector2Int>();
        private Vector2Int activeCheckpoint;
        private bool hasCheckpoint;
        private float statusMessageUntil;

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
            if (!HasStatusMessage) StatusMessage = string.Empty;
        }

        public void ReachGoal()
        {
            if (State != GameState.Playing) return;
            Score += goalPoints;
            Score += Mathf.Clamp(maxSpeedBonus - Mathf.RoundToInt(ElapsedTime), 0, maxSpeedBonus);
            State = GameState.Won;
            SaveRecord();
        }

        public bool IsStarCollected(Vector2Int cell) => collectedStars.Contains(cell);

        public void CollectStar(Vector2Int cell)
        {
            if (State != GameState.Playing || !collectedStars.Add(cell)) return;
            StarsCollected++;
            Score += starPoints;
            ShowStatus($"ESTRELLA +{starPoints}");
        }

        public void SetCheckpoint(Vector2Int cell)
        {
            if (State != GameState.Playing || (hasCheckpoint && activeCheckpoint == cell)) return;
            activeCheckpoint = cell;
            hasCheckpoint = true;
            ShowStatus("CHECKPOINT ACTIVADO");
        }

        public void PlayerHitTrap()
        {
            if (State != GameState.Playing) return;
            Lives--;
            ShowStatus("TRAMPA: PIERDES UNA VIDA");
            if (Lives <= 0)
            {
                RestartAttempt();
                return;
            }
            ball.RespawnAt(hasCheckpoint ? activeCheckpoint : ball.StartCell);
        }

        public void RestartAttempt()
        {
            Lives = startingLives;
            Score = 0;
            StarsCollected = 0;
            ElapsedTime = 0f;
            State = GameState.Playing;
            collectedStars.Clear();
            hasCheckpoint = false;
            ball.RespawnStart();
            ShowStatus("NUEVO INTENTO");
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

        private void ShowStatus(string message)
        {
            StatusMessage = message;
            statusMessageUntil = Time.unscaledTime + 2.2f;
        }
    }

    public enum GameState { Playing, Won }
}
