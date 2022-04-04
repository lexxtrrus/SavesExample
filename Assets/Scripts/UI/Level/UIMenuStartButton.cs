using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Level
{
    public class UIMenuStartButton : MonoBehaviour
    {
        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene("Level");
        }
    }
}
