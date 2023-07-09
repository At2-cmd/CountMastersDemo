using UnityEditor;
using UnityEngine;

public class StairPlacer : EditorWindow
{
    public GameObject stairPrefab;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 currentScale = new Vector3(1f, 1f, 1f);
    private float scaleReduction = 0.02f;
    private int numberOfLines = 40;

    [MenuItem("Window/Stair Placer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StairPlacer));
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Stair Placer", EditorStyles.boldLabel);

        stairPrefab = EditorGUILayout.ObjectField("Stair Prefab", stairPrefab, typeof(GameObject), false) as GameObject;

        EditorGUILayout.LabelField("Current Position: " + currentPosition.ToString());
        EditorGUILayout.LabelField("Current Scale: " + currentScale.ToString());

        if (GUILayout.Button("Place Stair"))
        {
            PlaceStair();
        }
    }

    private void PlaceStair()
    {
        for (int i = 0; i < numberOfLines; i++)
        {
            GameObject stairLine = PrefabUtility.InstantiatePrefab(stairPrefab) as GameObject;
            stairLine.transform.position = currentPosition;
            stairLine.transform.localScale = currentScale;

            currentPosition += new Vector3(0, currentScale.y * 1.5f, currentScale.z *2);
            currentScale -= new Vector3(scaleReduction, 0, 0);
        }
    }
}
