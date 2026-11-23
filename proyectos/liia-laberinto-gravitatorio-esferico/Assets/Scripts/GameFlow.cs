using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private float inactivityTimeoutSeconds = 45f;

        public int Lives { get; private set; }
        public int Score { get; private set; }
        public float ElapsedTime { get; private set; }
        public GameState State { get; private set; } = GameState.Attract;
        public int StarsCollected { get; private set; }
        public int TotalStars { get; private set; }
        public int LastTimeBonus { get; private set; }
        public int BestScore => PlayerPrefs.GetInt($"LIIA_BEST_SCORE_{LevelIndex}", 0);
        public float BestTime => PlayerPrefs.GetFloat($"LIIA_BEST_TIME_{LevelIndex}", 0f);
        public string StatusMessage { get; private set; }
        public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage) && Time.unscaledTime < statusMessageUntil;
        public string LevelName { get; private set; }
        public int LevelIndex { get; private set; }

        private readonly HashSet<Vector2Int> collectedStars = new HashSet<Vector2Int>();
        private Vector2Int activeCheckpoint;
        private bool hasCheckpoint;
        private float statusMessageUntil;
        private float stateChangedAt;
        private float lastInputAt;
        private float parTimeSeconds = 60f;

        public void Configure(MazeBallController player, MazeLevelInfo level, int lives = 3, int totalStars = 0)
        {
            ball = player;
            startingLives = lives;
            LevelName = level.DisplayName;
            LevelIndex = MazeLevelCatalog.SelectedIndex;
            parTimeSeconds = level.ParTimeSeconds;
            TotalStars = totalStars > 0 ? totalStars : level.ExpectedStars;
            EnterAttractMode();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            Lives = startingLives;
            stateChangedAt = Time.unscaledTime;
        }

        private void Update()
        {
            if (State == GameState.Attract)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) { ChangeLevel(0); return; }
                if (Input.GetKeyDown(KeyCode.Alpha2)) { ChangeLevel(1); return; }
                if (Input.GetKeyDown(KeyCode.Alpha3)) { ChangeLevel(2); return; }
                if (Input.GetKeyDown(KeyCode.Alpha4)) { ChangeLevel(3); return; }
                if (Input.GetKeyDown(KeyCode.Alpha5)) { ChangeLevel(4); return; }
                if (StartInputPressed()) StartAttempt();
                return;
            }

            if (State == GameState.Playing)
            {
                ElapsedTime += Time.deltaTime;
                if (Input.anyKey) lastInputAt = Time.unscaledTime;
                if (Input.GetKeyDown(KeyCode.R)) RestartAttempt();
                else if (Time.unscaledTime - lastInputAt > inactivityTimeoutSeconds) EnterAttractMode();
            }
            else if (State == GameState.Won)
            {
                if (Input.GetKeyDown(KeyCode.R)) RestartAttempt();
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.N))
                    ChangeLevel((LevelIndex + 1) % MazeLevelCatalog.Count);
                else if (Time.unscaledTime - stateChangedAt > 10f) EnterAttractMode();
            }
            if (!HasStatusMessage) StatusMessage = string.Empty;
        }

        public void ReachGoal()
        {
            if (State != GameState.Playing) return;
            Score += goalPoints;
            LastTimeBonus = Mathf.RoundToInt(maxSpeedBonus * Mathf.Clamp01(1f - ElapsedTime / parTimeSeconds));
            Score += LastTimeBonus;
            State = GameState.Won;
            stateChangedAt = Time.unscaledTime;
            LIIAAudioFeedback.Instance?.Play(GameAudioCue.Goal);
            SaveRecord();
        }

        public bool IsStarCollected(Vector2Int cell) => collectedStars.Contains(cell);

        public void CollectStar(Vector2Int cell)
        {
            if (State != GameState.Playing || !collectedStars.Add(cell)) return;
            StarsCollected++;
            Score += starPoints;
            ShowStatus($"ESTRELLA +{starPoints}");
            LIIAAudioFeedback.Instance?.Play(GameAudioCue.Star);
        }

        public void SetCheckpoint(Vector2Int cell)
        {
            if (State != GameState.Playing || (hasCheckpoint && activeCheckpoint == cell)) return;
            activeCheckpoint = cell;
            hasCheckpoint = true;
            ShowStatus("CHECKPOINT ACTIVADO");
            LIIAAudioFeedback.Instance?.Play(GameAudioCue.Checkpoint);
        }

        public void PlayerHitTrap()
        {
            if (State != GameState.Playing) return;
            Lives--;
            ShowStatus("TRAMPA: PIERDES UNA VIDA");
            LIIAAudioFeedback.Instance?.Play(GameAudioCue.Trap);
            if (Lives <= 0)
            {
                RestartAttempt();
                return;
            }
            ball.RespawnAt(hasCheckpoint ? activeCheckpoint : ball.StartCell);
        }

        public void RestartAttempt()
        {
            StartAttempt();
        }

        private void StartAttempt()
        {
            Lives = startingLives;
            Score = 0;
            StarsCollected = 0;
            LastTimeBonus = 0;
            ElapsedTime = 0f;
            State = GameState.Playing;
            collectedStars.Clear();
            hasCheckpoint = false;
            ball.RespawnStart();
            State = GameState.Playing;
            stateChangedAt = Time.unscaledTime;
            lastInputAt = Time.unscaledTime;
            ShowStatus("¡A JUGAR!");
            LIIAAudioFeedback.Instance?.Play(GameAudioCue.Start);
        }

        private void EnterAttractMode()
        {
            Lives = startingLives;
            Score = 0;
            StarsCollected = 0;
            LastTimeBonus = 0;
            ElapsedTime = 0f;
            collectedStars.Clear();
            hasCheckpoint = false;
            State = GameState.Attract;
            stateChangedAt = Time.unscaledTime;
            lastInputAt = Time.unscaledTime;
            if (ball != null) ball.RespawnStart();
            StatusMessage = string.Empty;
        }

        private static bool StartInputPressed()
        {
            return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) ||
                   Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                   Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
                   Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                   Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
        }

        private static void ChangeLevel(int levelIndex)
        {
            MazeLevelCatalog.Select(levelIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void SaveRecord()
        {
            if (Score > BestScore || (Score == BestScore && (BestTime <= 0f || ElapsedTime < BestTime)))
            {
                PlayerPrefs.SetInt($"LIIA_BEST_SCORE_{LevelIndex}", Score);
                PlayerPrefs.SetFloat($"LIIA_BEST_TIME_{LevelIndex}", ElapsedTime);
                PlayerPrefs.Save();
            }
        }

        private void ShowStatus(string message)
        {
            StatusMessage = message;
            statusMessageUntil = Time.unscaledTime + 2.2f;
        }
    }

    public enum GameState { Attract, Playing, Won }
}
