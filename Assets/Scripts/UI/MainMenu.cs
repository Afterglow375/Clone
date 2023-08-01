using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Main");
        }
        
        public void Host()
        {
        }
        
        public void Server()
        {
        }
        
        public void Client()
        {
        }
    }
}
