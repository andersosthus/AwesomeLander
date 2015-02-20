using UnityEngine;

namespace Assets.Scripts.Gui
{
    public class MainMenu : BaseMenu
    {
        private void OnGUI()
        {
            ShowTwoButton("New Game", OnNewGame, "Quit!", OnQuitGame);
        }

        private static void OnQuitGame()
        {
            Application.Quit();
        }

        private static void OnNewGame()
        {
            PlayerPrefs.SetInt("totalScore", 0);
            PlayerPrefs.Save();

            Time.timeScale = 1;
            Application.LoadLevel(1);
        }
    }
}