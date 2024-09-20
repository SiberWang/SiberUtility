using UnityEditor;
using UnityEngine;

namespace SiberUtility.Editor
{
    public class ArrangeInRectangleWindow : EditorWindow
    {
        private float width               = 5f;
        private float height              = 5f;
        private int   rows                = 4;
        private int   columns             = 4;
        private bool  centerArrangeToggle = true;

        [MenuItem(ToolPaths.ArrangeRectangle_Path)]
        public static void ShowArrangeInRectangleWindow()
        {
            GetWindow<ArrangeInRectangleWindow>("Arrange In Rectangle");
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Rectangle Parameters", EditorStyles.boldLabel);

            width            = EditorGUILayout.FloatField("寬(Width)", width);
            height           = EditorGUILayout.FloatField("高(Height)", height);
            rows             = EditorGUILayout.IntField("行數(Rows)", rows);
            columns          = EditorGUILayout.IntField("列數(Columns)", columns);
            centerArrangeToggle = EditorGUILayout.Toggle("是否置中?(IsCenterArrange?)", centerArrangeToggle);

            GUILayout.Space(10);

            var selectedGameObjects = Selection.gameObjects;
            GUILayout.Label($"Selected GameObjects: {selectedGameObjects.Length}");

            GUILayout.Space(10);

            if (GUILayout.Button("排列(Arrange)"))
            {
                ArrangeInRectangle();
            }
        }

        private void ArrangeInRectangle()
        {
            var selectedGameObjects = Selection.gameObjects;
            if (selectedGameObjects.Length == 0)
            {
                Debug.LogWarning("No GameObjects selected.");
                return;
            }

            if (selectedGameObjects.Length > rows * columns)
            {
                Debug.LogWarning("Selected GameObjects exceed the specified rows and columns.");
                return;
            }

            Vector3 startPosition = Vector3.zero;
            float   cellWidth     = width / columns;
            float   cellHeight    = height / rows;

            if (centerArrangeToggle)
            {
                float offsetX = -width / 2 + cellWidth / 2;
                float offsetY = -height / 2 + cellHeight / 2;
                startPosition = new Vector3(offsetX, offsetY, 0f);
            }

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                int row    = i / columns;
                int column = i % columns;

                float x = startPosition.x + column * cellWidth;
                float y = startPosition.y + row * cellHeight;

                Vector3 newPosition = new Vector3(x, y, startPosition.z);
                Undo.RecordObject(selectedGameObjects[i].transform, "Arrange in Rectangle");
                selectedGameObjects[i].transform.position = newPosition;
            }
        }
    }
}