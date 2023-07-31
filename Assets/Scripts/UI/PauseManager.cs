using Managers;
using UnityEngine;

namespace UI
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;
        private bool _isPaused = false;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isPaused)
                    PauseGame();
                else
                    ResumeGame();
            }
        }
        
        public void PauseGame()
        {
            GameManager.Instance.IsPaused = true;
            Time.timeScale = 0f; // Stop time
            _isPaused = true;

            // Show the pause canvas
            // Assuming you have a variable that references the pause canvas
            _pauseMenuUI.SetActive(true);
        }

        public void ResumeGame()
        {
            GameManager.Instance.IsPaused = false;
            Time.timeScale = 1f; // Resume time
            _isPaused = false;

            // Hide the pause canvas
            _pauseMenuUI.SetActive(false);
        }
    }
}
