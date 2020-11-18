using UnityEditor;
using UnityEngine;
using Assets.Code.Menus;

namespace Assets.Code
{
    public interface IManager
    {
        void GameStart ();
        void GameEnd ();
    }

    public class Game : MonoBehaviour
    {
        /// <summary>
        /// The Game Context. A pointer to the canonical instance of "The Game."
        /// </summary>
        public static Game Ctx;

        public UIManager UI { get; private set; }
        //public TimeManager Clock { get; private set; }
        //public PlatformManager Platforms { get; private set; }
        //public ScoreManager Score { get; private set; }

        //private Player.Player _player;

        internal void Start () {
            Ctx = this;

            UI = new UIManager();
            //Clock = new TimeManager();
            //Platforms = new PlatformManager();
            //Score = new ScoreManager();

            UI.ShowStartMenu();
        }

		private void Update() 
		{
            if (Input.GetKeyDown(KeyCode.S))
            {
                UI.HideStartMenu();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                UI.Pause();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                UI.Unpause();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                UI.ShowInstructionsMenu();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                UI.HideInstructionsMenu();
            }
        }

        #region MenuFunctions

        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartGame () {
            //SpawnGameElements();

            UI.GameStart();
            //Clock.GameStart();
            //Platforms.GameStart();
            //Score.GameStart();

            Debug.Log("start");

        }

        /*
         private void SpawnGameElements () {
            var playerprefab = Resources.Load("Player");
            var player = (GameObject)Instantiate(playerprefab);
            _player = player.GetComponent<Player.Player>();
            Camera.main.transform.SetParent(_player.transform);
        }
        */

        /// <summary>
        /// Leave the game and return to the main menu
        /// </summary>
        public void ReturnToMenu () {
            //CleanUpGameElements();

            UI.GameEnd();
            //Clock.GameEnd();
            //Platforms.GameEnd();
            //Score.GameEnd();
        }

        /*
        private void CleanUpGameElements () {
            Camera.main.transform.SetParent(null);
            Camera.main.transform.position = Vector3.back;

            Destroy(_player.gameObject);
            _player = null;
        }
        */

        /// <summary>
        /// Quits the game if in editor, otherwise closes the application
        /// </summary>
        public void QuitGame () {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}
