using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

namespace ComercioClasificador.EditorTools
{
    public static class CreateXrFoundation
    {
        public static void Create()
        {
            var scenePath = "Assets/Scenes/Comercio.unity";
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            if (!scene.IsValid())
            {
                Debug.LogError($"[Comercio] No se pudo abrir {scenePath}.");
                EditorApplication.Exit(1);
                return;
            }

            var existing = Object.FindFirstObjectByType<XROrigin>();
            if (existing == null)
            {
                var root = new GameObject("XR Origin");
                var origin = root.AddComponent<XROrigin>();
                var offset = new GameObject("Camera Offset");
                offset.transform.SetParent(root.transform, false);
                var cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                cameraObject.transform.SetParent(offset.transform, false);
                cameraObject.transform.localPosition = new Vector3(0f, 1.6f, 0f);
                var camera = cameraObject.AddComponent<Camera>();
                camera.nearClipPlane = 0.03f;
                cameraObject.AddComponent<AudioListener>();
                cameraObject.AddComponent<TrackedPoseDriver>();
                origin.CameraFloorOffsetObject = offset;
                origin.Camera = camera;
                Debug.Log("[Comercio] XR Origin base creado.");
            }
            else
            {
                Debug.Log("[Comercio] XR Origin ya existía; no se duplicó.");
            }

            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorApplication.Exit(0);
        }
    }
}
