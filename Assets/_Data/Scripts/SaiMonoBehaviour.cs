using UnityEditor;
using UnityEngine;

public class SaiMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    protected virtual void Reset()
    {
        //For overrite
    }

    protected virtual void LoadComponent()
    {
        //For overrite
    }

    public void ButtonLoadComponent()
    {
        this.LoadComponent();
    }
}

[CustomEditor(typeof(SaiMonoBehaviour), true)]
public class SaiCustomInscpector : Editor
{
    public override void OnInspectorGUI()
    {
        SaiMonoBehaviour saiMono = (SaiMonoBehaviour)target;
        if (GUILayout.Button("Load Component"))
        {
            saiMono.ButtonLoadComponent();
        }

        DrawDefaultInspector();
    }
}
