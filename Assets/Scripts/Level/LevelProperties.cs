using UnityEngine;

namespace LastChild
{
    [CreateAssetMenu(fileName = "LevelProperty", menuName = "ScriptableObjects/LevelProperties")]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private string _title;
        public string Title => _title;

        [SerializeField] private string _sceneName;
        public string SceneName => _sceneName;

        [SerializeField] private LevelProperties _nextLevel;
        public LevelProperties NextLevel => _nextLevel;
    }
}
