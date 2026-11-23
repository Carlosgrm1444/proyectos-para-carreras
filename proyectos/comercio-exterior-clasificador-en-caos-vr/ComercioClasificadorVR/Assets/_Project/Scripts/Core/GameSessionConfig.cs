using UnityEngine;

namespace ComercioClasificador.Core
{
    [CreateAssetMenu(fileName = "GameSessionConfig", menuName = "Comercio Clasificador/Game Session Config")]
    public sealed class GameSessionConfig : ScriptableObject
    {
        [Header("Recepción")]
        [Min(1)] public int maxPendingCargo = 5;
        [Min(1f)] public float initialSpawnInterval = 20f;
        [Min(1f)] public float minimumSpawnInterval = 7f;
        [Min(0f)] public float intervalReductionPerWave = 1.5f;

        [Header("Carga")]
        [Range(0f, 100f)] public float initialIntegrity = 100f;
        [Range(0f, 100f)] public float standardDamage = 20f;

        [Header("Defensa")]
        [Min(0f)] public float repulsionCooldown = 3f;
        [Min(0f)] public float repulsionRange = 2f;

        [Header("Partida")]
        [Min(1f)] public float targetSessionMinutes = 5f;
        [Min(0)] public int baseDeliveryScore = 100;
    }
}
