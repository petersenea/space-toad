using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class CameraSystem : MonoBehaviour
    {
        private GameObject game;
        public float xmin;
        public float xmax;

        // Start is called before the first frame update
        void Start()
        {
            game = GameObject.FindWithTag("GameControl");
        }

        // Update is called once per frame
        void Update()
        {
            if (game.GetComponent<Game>().CheckStart())
            {
                var player = GameObject.FindGameObjectWithTag("SpaceToad");
                float x = Mathf.Clamp(player.transform.position.x, xmin, xmax);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }
}
