using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class OpenSceneEditor : EditorWindow
{
    private static string _scenePath = "Assets/Scenes/{0}.unity";

    [MenuItem("OpenScene/MainMenu", false, 0)]
    public static void MainMenu()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "0_MainMenuScene"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/CharacterSelection", false, 1)]
    public static void ChrSel()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "1_CharacterSelectionScene"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/Village", false, 2)]
    public static void Village()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Map02_Village"), OpenSceneMode.Single);
    }
    
    [MenuItem("OpenScene/Dungeon", false, 3)]
    public static void Dungeon()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Map04_Dungeon"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/TestBattle", false, 9)]
    public static void TestBattle()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "TestBattle"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/Trainning", false, 10)]
    public static void Trainning()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "CharacterTrainningScene"), OpenSceneMode.Single);
    }
}