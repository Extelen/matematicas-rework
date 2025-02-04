using UnityEngine;
using Eflatun.SceneReference;

[System.Serializable]
public class ModuleData
{
    // Variables
    [SerializeField]
    private string m_identifier;

    [SerializeField]
    private SceneReference m_scene;
    public SceneReference Scene
    {
        get
        {
            return m_scene;
        }
    }
}