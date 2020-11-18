using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            public PauseMenu()
            {
                Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Pause Menu"), Canvas);
            }

        }
    }
}