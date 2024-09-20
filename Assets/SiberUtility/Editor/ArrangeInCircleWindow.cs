using UnityEditor;
using UnityEngine;

namespace SiberUtility.Editor
{
    public class ArrangeInCircleWindow : EditorWindow
    {
        private float radius              = 5f;
        private int   itemCount           = 1;
        private bool  centerArrangeToggle = true;
        private bool  is3D                = false;

        [MenuItem(ToolPaths.ArrangeCircle_Path)]
        public static void ShowArrangeInCircleWindow()
        {
            GetWindow<ArrangeInCircleWindow>("Arrange In Circle");
        }

        private void OnEnable()
        {
            var selectedGameObjects = Selection.gameObjects;
            if (selectedGameObjects.Length > 0)
                itemCount = selectedGameObjects.Length;
        }

        private void OnGUI()
        {
            GUILayout.Label("Circle Parameters", EditorStyles.boldLabel);

            radius              = EditorGUILayout.FloatField("半徑(Radius)", radius);
            itemCount           = EditorGUILayout.IntField("數量(ItemCount)", itemCount);
            centerArrangeToggle = EditorGUILayout.Toggle("是否置中?(IsCenterArrange?", centerArrangeToggle);
            is3D                = EditorGUILayout.Toggle("Is3D?", is3D);

            GUILayout.Space(10);

            var selectedGameObjects = Selection.gameObjects;
            GUILayout.Label($"Selected GameObjects: {selectedGameObjects.Length}");

            GUILayout.Space(10);

            if (GUILayout.Button("排列(Arrange)"))
            {
                ArrangeInCircle(selectedGameObjects);
            }
        }

        private void ArrangeInCircle(GameObject[] selectedGameObjects)
        {
            if (selectedGameObjects.Length == 0)
            {
                Debug.LogWarning("No GameObjects selected.");
                return;
            }

            Vector3 center    = centerArrangeToggle ? Vector3.zero : selectedGameObjects[0].transform.position;
            float   angleStep = 360f / itemCount;

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                float angle   = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;
                float x       = center.x + radius * Mathf.Cos(radians);
                float y       = center.y + radius * Mathf.Sin(radians);

                Vector3 newPosition   = new Vector3(x, y, center.z);
                if (is3D) newPosition = new Vector3(x, center.z, y);

                Undo.RecordObject(selectedGameObjects[i].transform, "Arrange in Circle");
                selectedGameObjects[i].transform.position = newPosition;
            }
        }
    }
}