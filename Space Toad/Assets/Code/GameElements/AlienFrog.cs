using UnityEngine;
using System.Collections;

namespace Assets.Code.AlienFrog
{

    public class AlienFrog : MonoBehaviour
    {
        private bool _didWin;
        private float Timer = 0f;


        internal void Start()
        {
            _didWin = false;
        }

        internal void Update()
        {
            if (!_didWin)
            {
                Timer += Time.deltaTime;
                if (Timer >= 5f)
                {
                    SpawnLaser();
                    Timer = 0f;
                }

                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }

        internal void SpawnLaser()
        {
            var rocket = (GameObject) Instantiate(Resources.Load("GameElements/LaserBullet"));
            float width = GetComponent<SpriteRenderer>().size.x;
            rocket.transform.SetPositionAndRotation(
                new Vector3(transform.position.x - width, transform.position.y, transform.position.z),
                rocket.transform.rotation);
            Destroy(rocket, 10);
        }

        internal void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "SpaceToad")
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    Destroy(gameObject);
                }
                else
                {
                    _didWin = true;
                }

            }
            else if (collision.gameObject.tag == "RocketBullet")
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Debug.Log("hello");
            }

        }

        internal void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}