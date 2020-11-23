using UnityEditor;
using UnityEngine;
using Assets.Code.Menus;
using System.Collections;

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

        private SpaceToad.SpaceToad _player;
        private bool _started;
        private float Timer = 0f;

        internal void Start () {
            Ctx = this;

            UI = new UIManager();

            UI.ShowStartMenu();
            _started = false;
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.S) && !_started)
            {
                StartGame();
                
                UI.HideStartMenu();
                
            }

            if (_started)
            {
                Timer += Time.deltaTime;
            }

            if (Timer >= 6f && _started)
            {
                SpawnAlien();
                Timer = 0f;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                // code for pausing components here
                PauseGameElements();
                // code for showing pause menu here
                UI.Pause();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                // code for unpausing components here
                UnpauseGameElements();
                // code for hiding pause menu here
                UI.Unpause();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                PauseGameElements();
                UI.ShowInstructionsMenu();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                UnpauseGameElements();
                UI.HideInstructionsMenu();
            }


        }

        #region MenuFunctions

        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartGame () {
            SpawnGameElements();

            UI.GameStart();

        }


        private void SpawnGameElements() {
            var player = (GameObject) Instantiate(Resources.Load("GameElements/SpaceToad"));
            _player = player.GetComponent<SpaceToad.SpaceToad>();

            var moonfloor = (GameObject) Instantiate(Resources.Load("GameElements/MoonFloor"));
            var spaceship = (GameObject) Instantiate(Resources.Load("GameElements/SpaceShip"));
            var moonfly = (GameObject) Instantiate(Resources.Load("GameElements/MoonFly"));

            SpawnAlien(); // one Alien to start

            _started = true;

        }

        private void SpawnAlien()
        {
            var alienfrog = (GameObject)Instantiate(Resources.Load("GameElements/AlienFrog"));

        }

        public void PauseGameElements()
        {
            // put code here
            Time.timeScale = 0;
            //Debug.Log("pausing game elements...");
            _started = false;
        }

        public void UnpauseGameElements()
        {
            // put code here
            Time.timeScale = 1;
            //Debug.Log("unpausing game elements...");
            _started = true;
        }

        

        /// <summary>
        /// Leave the game and return to the main menu
        /// </summary>
        public void ReturnToMenu () {
            CleanUpGameElements();

            UI.GameEnd();
            //Clock.GameEnd();
            //Platforms.GameEnd();
            //Score.GameEnd();
        }

        
        private void CleanUpGameElements () {

            Destroy(_player.gameObject);
            _player = null;
        }
        

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
