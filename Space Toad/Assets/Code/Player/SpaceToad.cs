using UnityEngine;
using System.Collections;

namespace Assets.Code.SpaceToad
{

    public class SpaceToad : MonoBehaviour
    {

        private bool _jumping;
        private float _jumpForce = 5f;
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
            if (Input.GetKeyDown(KeyCode.Space) && !_gameEnd)
            {
                if (!_jumping)
                {
                    Debug.Log("jumping");
                    _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
                }
                _jumping = true;
            }

            if (Input.GetKey(KeyCode.RightArrow) && !_gameEnd)
            {
                _rb.position += new Vector2 (1f, 0f) * 0.005f;
            }
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
                Debug.Log("you win");
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "AlienFrog")
            {

                if (_sr.color == Color.yellow)
                {
                    _sr.color = Color.green;
                    
                }
                else
                {
                    Debug.Log("you lose");
                    _sr.color = Color.red;
                    _gameEnd = true;

                }
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