using UnityEngine;

namespace ComercioClasificador.Core
{
    public sealed class GameSession : MonoBehaviour
    {
        [SerializeField] private GameSessionConfig config;

        public GameFlowState State { get; private set; } = GameFlowState.Boot;
        public int Score { get; private set; }
        public int Combo { get; private set; }
        public int Wave { get; private set; } = 1;
        public int PendingCargo { get; private set; }

        public GameSessionConfig Config => config;

        public void Begin()
        {
            Score = 0;
            Combo = 0;
            Wave = 1;
            PendingCargo = 0;
            State = GameFlowState.Playing;
        }

        public void SetState(GameFlowState nextState)
        {
            State = nextState;
            Time.timeScale = nextState == GameFlowState.Paused ? 0f : 1f;
        }

        public bool TryQueueCargo()
        {
            var capacity = config != null ? config.maxPendingCargo : 5;
            if (PendingCargo >= capacity)
            {
                SetState(GameFlowState.GameOver);
                return false;
            }

            PendingCargo++;
            return true;
        }

        public void ResolveCargo(bool correct, float integrity01)
        {
            if (PendingCargo > 0) PendingCargo--;
            if (!correct)
            {
                Combo = 0;
                return;
            }

            Combo++;
            var baseScore = config != null ? config.baseDeliveryScore : 100;
            Score += Mathf.RoundToInt(baseScore * Mathf.Clamp01(integrity01));
        }
    }
}
