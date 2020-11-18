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
            }


        }
    }
}