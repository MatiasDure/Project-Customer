using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ButtonBehaviour : MonoBehaviour
    {

        public void ExitGame() => Application.Quit();

        public void LoadScene(string newScene) => SceneManager.LoadScene(newScene);
    }
}
