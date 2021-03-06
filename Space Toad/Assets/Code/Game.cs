﻿using UnityEditor;
using UnityEngine;
using Assets.Code.Menus;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        private SpaceToadns.SpaceToad _player;
        private bool _started;
        private bool _paused;
        private float Timer = 0f;
        private float FlyTimer = 0f;

        internal void Start () {
            Ctx = this;

            UI = new UIManager();

            UI.ShowStartMenu();
            UnpauseGameElements();
            _started = false;
            _paused = false;
        }

        public bool CheckStart()
        {
            return _started;
        }
        
        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.S) && !_started && !_paused)
            {
                StartGame();
                
                UI.HideStartMenu();
                
            }

            if (_started)
            {
                Timer += Time.deltaTime;
                FlyTimer += Time.deltaTime;

                if (Timer >= 2.5f)
                {
                    SpawnAlien();
                    Timer = 0f;
                }
                
                if (FlyTimer >= 10f)
                {
                    SpawnFly();
                    FlyTimer = 0f;
                }
                

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RestartLevel();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGameElements();
                UI.Pause();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                UnpauseGameElements();
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
            GameObject.Find("Score").GetComponent<Text>().text = "0";
            UI.GameStart();
            _started = true;
        }


        private void SpawnGameElements() {
            var player = (GameObject) Instantiate(Resources.Load("GameElements/SpaceToad"));
            _player = player.GetComponent<SpaceToadns.SpaceToad>();

            var nightsky = (GameObject) Instantiate(Resources.Load("GameElements/NightSky"));
            var moonfloor = (GameObject) Instantiate(Resources.Load("GameElements/MoonFloor"));
            var spaceship = (GameObject) Instantiate(Resources.Load("GameElements/SpaceShip"));

            SpawnAlien(); // one Alien to start
            SpawnFly(); // one Fly to start

            

        }

        private void SpawnAlien()
        {
            GameObject alienfrog = (GameObject)Instantiate(Resources.Load("GameElements/AlienFrog"));
            float side = Random.Range(0, 2);
            if (side == 0)
            {
                alienfrog.transform.position =
                    new Vector3(31f, alienfrog.transform.position.y, alienfrog.transform.position.z);
            }
            else
            {
                alienfrog.transform.position =
                    new Vector3(-14f, alienfrog.transform.position.y, alienfrog.transform.position.z);
            }
        }
        
        private void SpawnFly()
        {
            var moonfly = (GameObject) Instantiate(Resources.Load("GameElements/MoonFly"));
        }

        public void PauseGameElements()
        {
            // put code here
            Time.timeScale = 0;
            //Debug.Log("pausing game elements...");
            _started = false;
            _paused = true;
            //GameObject.Find("SpaceToad(Clone)").isPaused = true;
        }

        public void UnpauseGameElements()
        {
            // put code here
            Time.timeScale = 1;
            //Debug.Log("unpausing game elements...");
            _started = true;
            _paused = false;
            //GameObject.Find("SpaceToad(Clone)").isPaused = false;
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
