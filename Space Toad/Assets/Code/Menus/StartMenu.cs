using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class StartMenu : Menu
        {
            public StartMenu()
            {
                Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Start Menu"), Canvas);

                InitializeButtons();
            }



            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons()
            {
                Button start_button = GameObject.Find("Start").GetComponent<Button>();
                start_button.onClick.AddListener(() => Game.Ctx.StartGame());
                Button quit_button = GameObject.Find("Quit").GetComponent<Button>();
                quit_button.onClick.AddListener(() => Game.Ctx.QuitGame());
            }

        }
    }
}