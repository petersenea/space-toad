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
		public bool _endAnimation;
        public bool facingRight = true;
        private bool fired = false;
        private float timeToFire = 0f;
        private bool canEscape = false;
        public float timeToEscape = 60.0f;
		public Sprite safeShip;
		public Sprite escapeShip;
		GameObject spaceship;
       

        internal void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _gameEnd = false;
			_endAnimation = false;
			spaceship = GameObject.Find("SpaceShip(Clone)");
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

            if (timeToEscape <= 0 && !canEscape)
            {
                canEscape = true;
				//Debug.Log("Can escape!");
                //GameObject.Find("SpaceShip(Clone)").GetComponent<SpriteRenderer>().sprite = escapeShip;
				spaceship.GetComponent<SpriteRenderer>().sprite = escapeShip;
            }
        }

        private void CheckKeys()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_gameEnd && !_endAnimation)
            {
                if (!_jumping)
                {
                    _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
                }
                _jumping = true;
            }

            if (Input.GetKey(KeyCode.RightArrow) && !_gameEnd && !_endAnimation)
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

            if (Input.GetKey(KeyCode.LeftArrow) && !_gameEnd && !_endAnimation)
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

            if (Input.GetKeyDown(KeyCode.Space) && !_gameEnd && !_endAnimation && !fired)
            {
                SpawnRocket();
                fired = true;
            }

            if (_gameEnd)
            {
                Game.Ctx.PauseGameElements();
            }

			if (_endAnimation && !_gameEnd)
			{
				Vector3 pos = spaceship.transform.position;
				//spaceship.transform.Translate(Vector3.up * Time.deltaTime * 2f);
				spaceship.transform.position = new Vector3(pos.x, pos.y + Time.deltaTime, pos.z);
				//Debug.Log(spaceship.transform.position.y);
				if (spaceship.transform.position.y >= 10f)
				{
					_gameEnd = true;
				}
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
                //Destroy(gameObject, 0.1f);
                //Game.Ctx.PauseGameElements();
				collision.gameObject.GetComponent<SpriteRenderer>().sprite = safeShip;
                //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                //_gameEnd = true;
				EndingAnimation();
				_endAnimation = true;

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

		private void EndingAnimation() 
		{
			transform.position = new Vector3(7.833f, 10f, 0);
			_rb.isKinematic = true;
			_endAnimation = true;
		}


    }
}