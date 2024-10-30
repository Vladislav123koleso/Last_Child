using UnityEngine;
using UnityEngine.Video;

namespace LastChild
{

    public class VideoHelper : MonoBehaviour
    {
        private VideoPlayer _player;

        private void Start()
        {
            Cursor.visible = false;
            _player = GetComponent<VideoPlayer>();
            _player.loopPointReached += OnLoopReached;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;

                LevelController.Instance.NextLevel();
            }
        }

        private void OnLoopReached(VideoPlayer player)
        {
            Cursor.visible = true;

            LevelController.Instance.NextLevel();
        }
    }
}
