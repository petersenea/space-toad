using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class InstructionsMenu : Menu
        {
            public InstructionsMenu()
            {
                Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Instructions Menu"), Canvas);
            }

        }
    }
}