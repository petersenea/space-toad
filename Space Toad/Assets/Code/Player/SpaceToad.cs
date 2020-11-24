using UnityEngine;
using System.Collections;

namespace Assets.Code.SpaceToadns
{

    public class SpaceToad : MonoBehaviour
    {

        private bool _jumping;
        private float _jumpForce = 10f;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private bool _gameEnd;
        public bool facingRight = true;
        private bool fired = false;
        private float timeToFire = 0f;
        private bool canEscape = false;
        public float timeToEscape = 60.0f;

        internal void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _gameEnd = false;
        }

        internal void Update()
        {
            CheckKeys();
            if (fired)
            {
                timeToFire += Time.deltaTime;
            }

            if (timeToFire >= 1.0f)
            {
                fired = false;
                timeToFire = 0f;
            }

            if (!canEscape)
            {
                timeToEscape -= Time.deltaTime;
            }

            if (timeToEscape <= 0)
            {
                canEscape = true;
            }
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
                if (!facingRight) 
                {
                    FlipToad();
                }

                if (transform.position.x <= 27f)
                {
                    _rb.position += new Vector2(1f, 0f) * 0.1f;
                    //transform.position += Vector3.right * 0.1f;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !_gameEnd)
            {
                if (facingRight) 
                {
                    FlipToad();
                }
                
                if (transform.position.x >= -10f) 
                {
                    _rb.position += new Vector2 (1f, 0f) * -0.1f;
				    //transform.position += Vector3.right * -0.1f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_gameEnd && !fired)
            {
                SpawnRocket();
                fired = true;
            }

            if (_gameEnd)
            {
                Game.Ctx.PauseGameElements();
            }
        }

        private void FlipToad() 
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        internal void SpawnRocket()
        {
            var rocket = (GameObject)Instantiate(Resources.Load("GameElements/RocketBullet"));
            float dir = 1f;
            float width = GetComponent<SpriteRenderer>().size.x;
            if (!facingRight) 
            {
                rocket.GetComponent<RocketBullet>().FlipRocket();
                dir *= -1f;
            }
			rocket.transform.position = new Vector3(transform.position.x + (dir * width), transform.position.y-0.2f, transform.position.z);
            Destroy(rocket, 3);
            //rocket.transform.SetPositionAndRotation(new Vector3(transform.position.x + width, transform.position.y-1f, transform.position.z), rocket.transform.rotation);
        }

        internal void OnTriggerEnter2D(Collider2D collision)
        { 
            if (collision.gameObject.tag == "MoonFly")
            {
                Destroy(collision.gameObject);
                _sr.color = Color.yellow;
            }
            

            else if (collision.gameObject.tag == "SpaceShip" && canEscape)
            {
                Destroy(gameObject, 0.1f);
                Game.Ctx.PauseGameElements();
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                
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