
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager : IManager
    {
        public static Transform Canvas { get; private set; }

        private StartMenu _start;
        private PauseMenu _pause;
        private InstructionsMenu _instructions;
        private readonly Text _keyText;
        private Text _hitText;

        public bool InMainMenu { get { return _start != null && _start.Showing; } }

        public UIManager () {
            Canvas = GameObject.Find("Canvas").transform; // There should only ever be one canvas
            _keyText = GameObject.Find("ReferenceKey").GetComponent<Text>();
            _hitText = GameObject.Find("Hit").GetComponent<Text>();
        }

        private void ShowReferenceKey()
        {
            _keyText.text = "'i': Instructions\n'p': Pause Game\n'Esc': Restart Game";
            _hitText.text = "Hits:";
        }

        private void HideReferenceKey()
        {
            _keyText.text = "";
            _hitText.text = "";
        }

        public void ShowStartMenu () {
            _start = new StartMenu();
            _start.Show();
        }

        public void HideStartMenu () {
            if (_start != null)
            {
                _start.Hide();
                _start = null;
                this.ShowReferenceKey();
            }
        }

        public void Pause () {
            if (_start == null && _instructions == null && _pause == null)
            {
                _pause = new PauseMenu();
                _pause.Show();
                //this.HideReferenceKey();
            }
            
        }

        public void Unpause () {
            if (_start == null && _instructions == null && _pause != null)
            {
                _pause.Hide();
                _pause = null;
                //this.ShowReferenceKey();
            }
            
        }

        public void ShowInstructionsMenu()
        {
            if (_start == null && _pause == null && _instructions == null)
            {
                _instructions = new InstructionsMenu();
                _instructions.Show();
                //this.HideReferenceKey();
            }
            
        }

        public void HideInstructionsMenu()
        {
            if (_start == null && _pause == null && _instructions != null)
            {
                _instructions.Hide();
                _instructions = null;
                //this.ShowReferenceKey();
            }

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
