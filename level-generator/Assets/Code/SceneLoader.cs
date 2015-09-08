using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public string m_sceneToLoad;

    public bool m_loadSceneOnAwake = false;

    public void Awake()
    {
        if (m_loadSceneOnAwake)
        {
            loadScene();
        }
    }

    public void loadScene()
    {
        Application.LoadLevel(m_sceneToLoad);
    }
}
