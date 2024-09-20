using UnityEditor;
using UnityEngine;

namespace SiberUtility.Editor
{
    public class ArrangeInLineWindow : EditorWindow
    {
        private float spacing              = 1.5f;
        private bool  horizontalLineToggle = true;
        private bool  centerArrangeToggle  = true;

        [MenuItem(ToolPaths.ArrangeLine_Path)]
        private static void ShowWindow()
        {
            GetWindow<ArrangeInLineWindow>("Arrange in Line Settings");
        }

        private void OnGUI()
        {
            GUILayout.Label("Arrange in Line Settings", EditorStyles.boldLabel);

            spacing              = EditorGUILayout.FloatField("間距(Spacing)", spacing);
            horizontalLineToggle = EditorGUILayout.Toggle("是否水平?(IsHorizontal?)", horizontalLineToggle);
            centerArrangeToggle  = EditorGUILayout.Toggle("是否置中?(IsCenterArrange?)", centerArrangeToggle);

            if (GUILayout.Button("Arrange Selected GameObjects"))
            {
                ArrangeSelectedGameObjectsInLine();
            }
        }

        private void ArrangeSelectedGameObjectsInLine()
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            if (selectedGameObjects.Length == 0)
            {
                Debug.LogWarning("No GameObjects selected.");
                return;
            }

            var position = centerArrangeToggle ? CalculateCenterPosition(selectedGameObjects.Length) : Vector3.zero;

            foreach (GameObject obj in selectedGameObjects)
            {
                Undo.RecordObject(obj.transform, "Arrange GameObjects in Line");
                obj.transform.position = position;
                var offset = horizontalLineToggle ? Vector3.right : Vector3.up;
                position += spacing * offset;
            }
        }

        private Vector3 CalculateCenterPosition(int count)
        {
            float centerIndex = count / 2f;
            var   offset      = horizontalLineToggle ? Vector3.right : Vector3.up;
            return -spacing * centerIndex * offset;
        }
    }
}