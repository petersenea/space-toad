using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{

    public class TimeManager : MonoBehaviour
    {
        public float CurrentTime { get; private set; }
        public float timeToEscape = 60f;
        private static Text _timeText;
        private GameObject game;
        private GameObject toad;

        // ReSharper disable once UnusedMember.Global
        private void Start()
        {
            _timeText = GetComponent<Text>();
            CurrentTime = 10.0f;
            game = GameObject.FindWithTag("GameControl");
            toad = GameObject.FindGameObjectWithTag("SpaceToad");
            UpdateTime();
        }

        private void Update()
        {
            if (game.GetComponent<Game>().CheckStart())
            {
                CurrentTime -= Time.deltaTime;
                //CurrentTime = System.Math.Round(CurrentTime, 2);
                UpdateTime();
            }
        }

        private void UpdateTime()
        {
            if (game.GetComponent<Game>().CheckStart())
            {
                if (CurrentTime >= 0f)
                {
                    _timeText.text = string.Format("{0}\n{1}", "Survive", Mathf.Round(CurrentTime))
                        .PadLeft(4, '0');
                }
                else if (GameObject.FindGameObjectWithTag("SpaceToad").GetComponent<SpaceToadns.SpaceToad>()._endAnimation)
                {
                    _timeText.text = string.Format("{0}", "");
                }
                else
                {
                    _timeText.text = string.Format("{0}", "Escape!")
                        .PadLeft(4, '0');
                }
            }
        }
    }
}
