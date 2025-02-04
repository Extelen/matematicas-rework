using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eflatun.SceneReference;

using UnityScene = UnityEngine.SceneManagement.Scene;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelManager : Singleton<LevelManager>
{
    // Variables
    [Header("Modules")]
    [SerializeField]
    private List<SceneReference> m_modules;

    [Header("References")]
    [SerializeField]
    private LoadingScreen m_additiveLoadingScreen;

    private SceneReference m_lastModule;
    private SceneReference m_currentModule;

    private int m_debugModuleBuildIndex = -1;

    // Methods
    protected virtual void Start()
    {
#if UNITY_EDITOR
        for (int i = 0; i < UnitySceneManager.sceneCount; i++)
        {
            var scene = UnitySceneManager.GetSceneAt(i);

            if (scene.name.Contains("Module"))
            {
                bool isOnModules = false;
                for (int j = 0; j < m_modules.Count; j++)
                {
                    SceneReference module = m_modules[j];
                    if (module.BuildIndex == scene.buildIndex)
                    {
                        m_currentModule = module;
                        isOnModules = true;
                        break;
                    }
                }

                if (!isOnModules)
                {
                    m_debugModuleBuildIndex = scene.buildIndex;
                }

                ModuleBehaviour.Current.Play();
                break;
            }
        }

        if (m_debugModuleBuildIndex != -1)
            return;
#endif

        if (m_currentModule == null)
        {
            ChooseModule();

            if (!SceneManager.IsLoading)
                PlayModule();
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    protected virtual void OnEnable()
    {
        SceneManager.OnStateChange += OnStateChange;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    protected virtual void OnDisable()
    {
        SceneManager.OnStateChange -= OnStateChange;
    }

    private void ChooseModule()
    {
        m_currentModule = m_modules.Random();
    }

    private void OnStateChange(SceneManager.State state)
    {
        if (state == SceneManager.State.Loaded)
            PlayModule();
    }

    public void EndLevel()
    {
        UIManager.Instance.SwitchPanel("Victory");
    }

    /// <summary>
    /// Play a sequence
    /// </summary>
    private void PlayModule()
    {
        void ExecuteModule()
        {
            ModuleBehaviour.Current.Play();
        }

        StartCoroutine(DoAdditiveLoad(ExecuteModule));
    }

    /// <summary>
    /// Trigger the next sequence and save to the data.
    /// </summary>
    public void NextModule()
    {
        if (m_debugModuleBuildIndex == -1)
        {
            m_lastModule = m_currentModule;
            ChooseModule();
        }

        // if (m_currentModule == m_modules.Count)
        // EndLevel();

        // else
        PlayModule();
    }

    // Coroutines
    private IEnumerator DoAdditiveLoad(LoadCallback onEnd)
    {
        // If it is the same scene, skip all load screens and run the module.
        // if (m_lastModule != null && m_lastModule.BuildIndex == m_currentModule.BuildIndex)
        //     onEnd?.Invoke();

        // If not, do the loading screens.
        // else
        // {
        AsyncOperation asyncOperation;

        // Show the loading screen
        yield return m_additiveLoadingScreen.Show();

        if (m_debugModuleBuildIndex != -1)
            yield return StartCoroutine(UnloadDebugScene());

        else
        {
            // Wait to unload the last scene async
            if (m_lastModule != null)
            {
                asyncOperation = UnitySceneManager.UnloadSceneAsync(m_lastModule.BuildIndex);

                while (!asyncOperation.isDone)
                    yield return null;
            }
        }

        // Wait to load current scene async.
        asyncOperation = UnitySceneManager.LoadSceneAsync(m_currentModule.BuildIndex, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;

        yield return new WaitForSeconds(0.25f);

        yield return m_additiveLoadingScreen.Hide();

        // Trigger the end event
        onEnd?.Invoke();
        // }
    }

    private IEnumerator UnloadDebugScene()
    {
        if (m_debugModuleBuildIndex == -1)
            yield break;

        AsyncOperation asyncOperation = UnitySceneManager.UnloadSceneAsync(m_debugModuleBuildIndex);

        while (!asyncOperation.isDone)
            yield return null;

        m_debugModuleBuildIndex = -1;
    }

    // Delegates
    public delegate void LoadCallback();
}
