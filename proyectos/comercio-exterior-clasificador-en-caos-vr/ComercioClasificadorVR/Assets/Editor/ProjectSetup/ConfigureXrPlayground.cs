using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

namespace ComercioClasificador.EditorTools
{
    public static class ConfigureXrPlayground
    {
        private const string ScenePath = "Assets/Scenes/Comercio.unity";

        public static void Configure()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath);
            var origin = Object.FindFirstObjectByType<XROrigin>();
            if (origin == null)
            {
                Debug.LogError("No se encontró XR Origin en Comercio.unity.");
                return;
            }

            var mediator = origin.GetComponent<LocomotionMediator>() ?? origin.gameObject.AddComponent<LocomotionMediator>();
            var move = origin.GetComponent<ContinuousMoveProvider>() ?? origin.gameObject.AddComponent<ContinuousMoveProvider>();
            var turn = origin.GetComponent<SnapTurnProvider>() ?? origin.gameObject.AddComponent<SnapTurnProvider>();

            move.mediator = mediator;
            move.moveSpeed = 1.5f;
            move.enableStrafe = true;
            move.enableFly = false;
            turn.mediator = mediator;
            turn.turnAmount = 30f;
            turn.debounceTime = 0.35f;
            turn.enableTurnLeftRight = true;
            turn.enableTurnAround = false;

            foreach (var camera in Object.FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (camera.transform.IsChildOf(origin.transform))
                {
                    camera.enabled = true;
                    camera.tag = "MainCamera";
                }
                else
                {
                    camera.enabled = false;
                    if (camera.CompareTag("MainCamera"))
                        camera.tag = "Untagged";
                }
            }

            EnsurePlayground();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            Debug.Log("Comercio: XR Origin configurado con movimiento continuo, giro por pasos y escenario de prueba.");
        }

        private static void EnsurePlayground()
        {
            if (GameObject.Find("VR Test Floor") == null)
            {
                var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
                floor.name = "VR Test Floor";
                floor.transform.position = Vector3.zero;
                floor.transform.localScale = new Vector3(3f, 1f, 3f);
                var renderer = floor.GetComponent<Renderer>();
                renderer.sharedMaterial = CreateMaterial("Comercio Floor", new Color(0.08f, 0.12f, 0.18f));
            }

            var obstacles = new[]
            {
                new Vector3(0f, 0.5f, 3f), new Vector3(0f, 0.5f, -3f),
                new Vector3(3f, 0.5f, 0f), new Vector3(-3f, 0.5f, 0f),
                new Vector3(2.2f, 0.5f, 2.2f), new Vector3(-2.2f, 0.5f, -2.2f)
            };
            for (var i = 0; i < obstacles.Length; i++)
            {
                var name = "VR Obstacle " + (i + 1);
                if (GameObject.Find(name) != null)
                    continue;
                var obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obstacle.name = name;
                obstacle.transform.position = obstacles[i];
                obstacle.transform.localScale = new Vector3(1.2f, 1f, 0.35f);
                obstacle.transform.Rotate(0f, i % 2 == 0 ? 0f : 90f, 0f);
                obstacle.GetComponent<Renderer>().sharedMaterial = CreateMaterial(name + " Material", new Color(0.15f, 0.3f, 0.45f));
            }
        }

        private static Material CreateMaterial(string name, Color color)
        {
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.name = name;
            material.color = color;
            return material;
        }
    }
}
