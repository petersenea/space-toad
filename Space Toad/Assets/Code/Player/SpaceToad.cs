using UnityEngine;
using System.Collections;

namespace Assets.Code.SpaceToad
{

    public class SpaceToad : MonoBehaviour
    {

        private bool _jumping;
        private float _jumpForce = 10f;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private bool _gameEnd;

        internal void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _gameEnd = false;
        }

        internal void Update()
        {
            CheckKeys();
        }

        private void CheckKeys()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_gameEnd)
            {
                if (!_jumping)
                {
                    _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
                }
                _jumping = true;
            }

            if (Input.GetKey(KeyCode.RightArrow) && !_gameEnd)
            {
                //_rb.position += new Vector2 (1f, 0f) * 0.005f;
				transform.position += Vector3.right * 0.1f;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !_gameEnd)
            {
                //_rb.position += new Vector2 (1f, 0f) * 0.005f;
				transform.position += Vector3.right * -0.1f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_gameEnd && !_jumping)
            {
                SpawnRocket();
                
            }

            if (_gameEnd)
            {
                Game.Ctx.PauseGameElements();
            }
        }

        internal void SpawnRocket()
        {
            var rocket = (GameObject)Instantiate(Resources.Load("GameElements/RocketBullet"));
            float width = GetComponent<SpriteRenderer>().size.x;
			rocket.transform.position = new Vector3(transform.position.x + width, transform.position.y-0.2f, transform.position.z);
            //rocket.transform.SetPositionAndRotation(new Vector3(transform.position.x + width, transform.position.y-1f, transform.position.z), rocket.transform.rotation);
        }

        internal void OnTriggerEnter2D(Collider2D collision)
        { 
            if (collision.gameObject.tag == "MoonFly")
            {
                Destroy(collision.gameObject);
                _sr.color = Color.yellow;
            }
            

            else if (collision.gameObject.tag == "SpaceShip")
            {
                Game.Ctx.PauseGameElements();
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "AlienFrog")
            {

                if (_sr.color == Color.yellow)
                {
                    _sr.color = Color.white;
                    
                }
                else
                {
                    _sr.color = Color.red;
                    _gameEnd = true;

                }
            }
            else if (collision.gameObject.tag == "LaserBullet")
            {
                if (_sr.color == Color.yellow)
                {
                    _sr.color = Color.white;

                }
                else
                {
                    _sr.color = Color.red;
                    _gameEnd = true;
                }
                Destroy(collision.gameObject);
            }

        }
        internal void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "MoonFloor")
            {
                _jumping = false;
            }

           

        }

    }
}