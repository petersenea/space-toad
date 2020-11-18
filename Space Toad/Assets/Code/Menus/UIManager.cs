
using UnityEngine;

namespace Assets.Code.Menus
{
    public partial class UIManager : IManager
    {
        public static Transform Canvas { get; private set; }

        private StartMenu _start;
        private PauseMenu _pause;
        private InstructionsMenu _instructions;

        public bool InMainMenu { get { return _start != null && _start.Showing; } }

        public UIManager () {
            Canvas = GameObject.Find("Canvas").transform; // There should only ever be one canvas
        }

        public void ShowStartMenu () {
            _start = new StartMenu();
            _start.Show();
        }

        public void HideStartMenu () {
            _start.Hide();
            _start = null;
        }

        public void Pause () {
            _pause = new PauseMenu();
            _pause.Show();
        }

        public void Unpause () {
            _pause.Hide();
            _pause = null;
        }

        public void ShowInstructionsMenu()
        {
            _instructions = new InstructionsMenu();
            _instructions.Show();
        }

        public void HideInstructionsMenu()
        {
            _instructions.Hide();
            _instructions = null;
        }

        public void GameStart () { HideStartMenu(); }

        public void GameEnd () { ShowStartMenu(); }

        private abstract class Menu
        {
            protected GameObject Go;
            public bool Showing { get; private set; }

            public virtual void Show () {
                Showing = true;
                Go.SetActive(true);
            }

            public virtual void Hide () {
                GameObject.Destroy(Go);
                Showing = false;
            }
        }
    }

}
