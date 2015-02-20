using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public class InGame : BaseMenu
    {
        private const int PointsPerScore = 100;
        public AudioClip LoseClip;
        public AudioClip WinClip;
        private int _activatedPads;
        private GameMode _guiMode = GameMode.Ingame;
        private int _levelScore;
        private int _totalPads = 2;

        public void LandingPadActivated()
        {
            _activatedPads++;
            if (_activatedPads == _totalPads)
                Win();

            var canvas = GetComponentInChildren<Canvas>();
            var score = canvas.GetComponentsInChildren<Text>();

            _levelScore = _activatedPads*PointsPerScore;
            score.Single(s => s.name.Equals("Score")).text = "SCORE - " + _levelScore;
        }

        private void Win()
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = WinClip;
            audioSource.Play();

            Time.timeScale = 0;
            SetTotalScore();
            var canvas = GetComponentInChildren<Canvas>();
            var score = canvas.GetComponentsInChildren<Text>();
            score.Single(s => s.name.Equals("Score")).text = "SCORE - 0";
            score.Single(s => s.name.Equals("Total")).text = "Total Score - " + _levelScore;
            _guiMode = GameMode.Win;
        }

        private void SetTotalScore()
        {
            if (PlayerPrefs.HasKey("totalScore"))
                PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore" + _levelScore));
            PlayerPrefs.Save();
        }

        public void Lose()
        {
            StartCoroutine(WaitAndLose());
        }

        private IEnumerator WaitAndLose()
        {
            yield return new WaitForSeconds(3);
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = LoseClip;
            audioSource.Play();
            Time.timeScale = 0;
            _guiMode = GameMode.Lose;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                _guiMode = GameMode.Paused;
            }
        }

        private void OnGUI()
        {
            if (_guiMode == GameMode.Paused)
                ShowTwoButton("Resume game", OnResumeGame, "Exit to Main Menu", OnMainMenu);

            if (_guiMode == GameMode.Win)
                ShowTwoButton("Next Level", OnNextLevel, "Exit to Main Menu", OnMainMenu);

            if (_guiMode == GameMode.Lose)
                ShowTwoButton("Retry Level", OnRetryLevel, "Exit to Main Menu", OnMainMenu);
        }

        private static void OnRetryLevel()
        {
            Time.timeScale = 1;
            var currentLevel = Application.loadedLevel;
            Application.LoadLevel(currentLevel);
        }

        private static void OnNextLevel()
        {
            Time.timeScale = 1;
            var nextLevel = Application.loadedLevel + 1;
            Application.LoadLevel(nextLevel);
        }

        private void OnResumeGame()
        {
            Time.timeScale = 1;
            _guiMode = GameMode.Ingame;
        }

        private static void OnMainMenu()
        {
            Time.timeScale = 1;
            Application.LoadLevel(0);
        }

        private enum GameMode
        {
            Ingame,
            Paused,
            Win,
            Lose
        }
    }
}