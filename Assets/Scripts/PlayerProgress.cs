using UnityEngine;

namespace LastChild
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerProgress : MonoBehaviour
    {
        public PlayerController _playerController;

        public int _currentStage = 0; //текущий прогресс перса

        [SerializeField] private float weakSpeed = 100.0f;
        [SerializeField] private float basicSpeed = 250f;
        [SerializeField] private float fastWalkSpeed = 400f;

        public bool CanClimb { get; set; }
        public bool CanCrawl { get; set; }
        public bool CanJump { get; set; }
        public bool CanMoveObjects { get; set; }

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            UpdatePlayerAbilities();
        }

        public void ProgressToNextStage()
        {
            if (_currentStage < 6) // Предельный этап
            {
                _currentStage++;
                UpdatePlayerAbilities();
            }
        }

        private void UpdatePlayerAbilities()
        {
            switch (_currentStage)
            {
                case 0: // начальное состояние персонажа
                    _playerController.SetMovementSpeed(weakSpeed);
                    CanClimb = false;
                    CanCrawl = true;
                    CanJump = false;
                    CanMoveObjects = false;
                    break;

                case 1: // 
                    _playerController.SetMovementSpeed(basicSpeed);
                    CanClimb = true;
                    CanCrawl = true;
                    CanJump = false;
                    CanMoveObjects = false;
                    break;

                case 2: // 
                    _playerController.SetMovementSpeed(fastWalkSpeed);
                    CanClimb = true;
                    CanCrawl = true;
                    CanJump = true;
                    CanMoveObjects = false;
                    break;

                case 3: // 
                    CanMoveObjects = true;
                    break;
            }
        }
    }
}
