using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastChild
{
    public class LevelController : SingletonBase<LevelController>
    {
        private const int MAIN_MENU_SCENE_INDEX = 0;

        [SerializeField] private LevelProperties _levelProperties;
        public LevelProperties LevelProperties => _levelProperties;

        public bool HasNextLevel => _levelProperties != null && _levelProperties.NextLevel != null;

        private void Awake()
        {
            Init();
        }

        public void NextLevel()
        {
            if (HasNextLevel)
            {
                SceneManager.LoadScene(_levelProperties.NextLevel.SceneName);
            }
            else
                LoadMainMenuScene();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);
        }

        public static void CloseApp()
        {
            Application.Quit();
        }
    }
}
