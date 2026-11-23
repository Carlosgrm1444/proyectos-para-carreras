using UnityEngine;

public sealed class GameBootstrap : MonoBehaviour
{
    [SerializeField] private int arenaSize = 32;
    [SerializeField] private int guardianHealth = 5;
    [SerializeField] private int dataCenterHealth = 10;
    [SerializeField] private Shader primitiveShader;

    private GameSession session;

    private void Start()
    {
        BuildWorld();
    }

    private void BuildWorld()
    {
        gameObject.AddComponent<WindowModeController>();
        PrimitiveMaterial.Configure(primitiveShader);
        RenderSettings.ambientLight = new Color(.42f, .48f, .58f);
        CreatePrimitive(PrimitiveType.Plane, "Arena", Vector3.zero, new Vector3(arenaSize / 10f, 1f, arenaSize / 10f), new Color(.08f, .13f, .20f));
        CreateArenaTiles();
        CreatePrimitive(PrimitiveType.Cube, "DataCenter", new Vector3(0f, 0.75f, 0f), new Vector3(2f, 1.5f, 2f), new Color(.22f, .74f, .97f), out var centerObject);
        var center = centerObject.AddComponent<DataCenter>();
        center.SetMaxHealth(dataCenterHealth);

        BuildDefensiveArena();

        CreatePrimitive(PrimitiveType.Capsule, "Guardian", new Vector3(0f, 1f, -3f), Vector3.one, new Color(1f, .76f, .28f), out var guardianObject);
        var guardian = guardianObject.AddComponent<GuardianController>();
        guardian.SetHealth(guardianHealth);
        var worldFeedback = gameObject.AddComponent<WorldFeedback>();
        worldFeedback.Configure(center.transform, guardian.transform);

        var cameraObject = new GameObject("IsometricCamera");
        var camera = cameraObject.AddComponent<Camera>();
        camera.transform.position = new Vector3(0f, 28f, -10f);
        // El encuadre compensa las bandas del HUD: llena la zona segura sin quedar debajo de ellas.
        camera.transform.LookAt(new Vector3(0f, 0f, -1.5f));
        camera.orthographic = false;
        camera.fieldOfView = 54f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(.078f, .102f, .141f);
        cameraObject.AddComponent<CameraHudSafeArea>();
        cameraObject.AddComponent<AudioListener>();
        cameraObject.tag = "MainCamera";
        var keyLightObject = new GameObject("KeyLight");
        var keyLight = keyLightObject.AddComponent<Light>();
        keyLight.type = LightType.Directional;
        keyLight.intensity = 1.15f;
        keyLight.color = new Color(1f, .92f, .78f);
        keyLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        var fillLightObject = new GameObject("CyanFillLight");
        var fillLight = fillLightObject.AddComponent<Light>();
        fillLight.type = LightType.Point;
        fillLight.color = new Color(.18f, .72f, 1f);
        fillLight.intensity = 2.2f;
        fillLight.range = 13f;
        fillLight.transform.position = new Vector3(0f, 5f, 0f);

        var spawnerObject = new GameObject("EntitySpawner");
        var spawner = spawnerObject.AddComponent<EntitySpawner>();
        spawner.Configure(center.transform, guardian.transform);

        var sessionObject = new GameObject("GameSession");
        session = sessionObject.AddComponent<GameSession>();
        sessionObject.AddComponent<CombatAudio>();
        sessionObject.AddComponent<MusicController>();
        var juice = sessionObject.AddComponent<GameJuice>();
        juice.Configure(camera);
        session.Configure(guardian, center, spawner, juice);
        guardianObject.AddComponent<GuardianWeapons>().Configure(session);
        CreateHud(session);
    }

    private void CreateHud(GameSession target)
    {
        var hudObject = new GameObject("HUD");
        var hud = hudObject.AddComponent<ArcadeHudController>();
        hudObject.AddComponent<InputDiagnostics>();
        hudObject.AddComponent<TutorialDirector>().Configure(target);
        hud.Configure(target);
    }

    private static GameObject CreatePrimitive(PrimitiveType type, string name, Vector3 position, Vector3 scale, Color color)
    {
        CreatePrimitive(type, name, position, scale, color, out var result);
        return result;
    }

    private static void CreatePrimitive(PrimitiveType type, string name, Vector3 position, Vector3 scale, Color color, out GameObject result)
    {
        result = GameObject.CreatePrimitive(type);
        result.name = name;
        result.transform.position = position;
        result.transform.localScale = scale;
        PrimitiveMaterial.Apply(result.GetComponent<Renderer>(), color);
        if (name == "Obstacle") result.AddComponent<ObstacleMarker>();
    }

    private static void CreateArenaTiles()
    {
        for (var x = -12; x <= 12; x += 4)
        for (var z = -12; z <= 12; z += 4)
        {
            var tile = CreatePrimitive(PrimitiveType.Cube, "FloorTile", new Vector3(x, -.055f, z), new Vector3(3.75f, .06f, 3.75f), new Color(.10f, .17f, .27f));
            tile.GetComponent<Collider>().enabled = false;
        }
    }

    private static void BuildDefensiveArena()
    {
        // Anillo defensivo incompleto: cuatro huecos claros para aproximarse al Centro.
        Barrier(new Vector3(-3.2f, .6f, 3.5f), new Vector3(2.9f, 1.2f, .8f), 0f);
        Barrier(new Vector3(3.2f, .6f, 3.5f), new Vector3(2.9f, 1.2f, .8f), 0f);
        Barrier(new Vector3(-3.2f, .6f, -3.5f), new Vector3(2.9f, 1.2f, .8f), 0f);
        Barrier(new Vector3(3.2f, .6f, -3.5f), new Vector3(2.9f, 1.2f, .8f), 0f);
        Barrier(new Vector3(-4.0f, .6f, 0f), new Vector3(.8f, 1.2f, 3f), 0f);
        Barrier(new Vector3(4.0f, .6f, 0f), new Vector3(.8f, 1.2f, 3f), 0f);

        // Coberturas asimétricas y corredores secundarios.
        Barrier(new Vector3(-8.5f, .55f, 5.8f), new Vector3(4.4f, 1.1f, .9f), -20f);
        Barrier(new Vector3(8.2f, .55f, 5.4f), new Vector3(3.6f, 1.1f, .9f), 26f);
        Barrier(new Vector3(-9.2f, .55f, -4.8f), new Vector3(3.6f, 1.1f, .9f), 28f);
        Barrier(new Vector3(8.8f, .55f, -5.5f), new Vector3(4.5f, 1.1f, .9f), -24f);
        Barrier(new Vector3(-6.4f, .55f, 9.5f), new Vector3(2.4f, 1.1f, .9f), 80f);
        Barrier(new Vector3(6.7f, .55f, -9.2f), new Vector3(2.7f, 1.1f, .9f), 78f);
        Barrier(new Vector3(-1.5f, .55f, 9.5f), new Vector3(2.8f, 1.1f, .9f), 0f);
        Barrier(new Vector3(1.8f, .55f, -9.5f), new Vector3(2.8f, 1.1f, .9f), 0f);

        EntranceSignal(new Vector3(0f, .07f, 13.2f), 0f);
        EntranceSignal(new Vector3(13.2f, .07f, 0f), 90f);
        EntranceSignal(new Vector3(0f, .07f, -13.2f), 180f);
        EntranceSignal(new Vector3(-13.2f, .07f, 0f), 270f);
    }

    private static void Barrier(Vector3 position, Vector3 scale, float rotation)
    {
        var obstacle = CreatePrimitive(PrimitiveType.Cube, "Obstacle", position, scale, new Color(.13f, .20f, .30f));
        obstacle.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        var trim = CreatePrimitive(PrimitiveType.Cube, "BarrierCyanTrim", position + Vector3.up * (scale.y * .5f + .035f), new Vector3(scale.x * .74f, .06f, .07f), new Color(.12f, .75f, 1f));
        trim.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        trim.GetComponent<Collider>().enabled = false;
        var stripe = CreatePrimitive(PrimitiveType.Cube, "BarrierGoldTrim", position + Vector3.up * (scale.y * .5f + .07f), new Vector3(scale.x * .22f, .065f, .09f), new Color(1f, .70f, .12f));
        stripe.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        stripe.GetComponent<Collider>().enabled = false;
    }

    private static void EntranceSignal(Vector3 position, float rotation)
    {
        var signal = CreatePrimitive(PrimitiveType.Cube, "EntranceSignal", position, new Vector3(2.4f, .04f, .28f), new Color(.95f, .25f, .22f));
        signal.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        signal.GetComponent<Collider>().enabled = false;
    }
}

