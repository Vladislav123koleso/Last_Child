using UnityEngine;
using UnityEngine.Video;

namespace LastChild
{

    public class VideoHelper : MonoBehaviour
    {
        private VideoPlayer _player;

        private void Start()
        {
            _player = GetComponent<VideoPlayer>();
            _player.loopPointReached += OnLoopReached;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LevelController.Instance.NextLevel();
            }
        }

        private void OnLoopReached(VideoPlayer player)
        {
            LevelController.Instance.NextLevel();
        }
    }
}
