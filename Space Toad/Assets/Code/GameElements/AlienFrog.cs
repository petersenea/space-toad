using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Assets.Code.AlienFrog
{

    public class AlienFrog : MonoBehaviour
    {
        private bool _didWin;
        private float Timer = 0f;
        public bool facingRight = false;
        private GameObject toad;
        private Text _scoreText;
        private int _score;
        private Rigidbody2D _rb;

        internal void Start()
        {
            _didWin = false;
            toad = GameObject.FindGameObjectWithTag("SpaceToad");
            _rb = GetComponent<Rigidbody2D>();
        }

        internal void Update()
        {
            bool endAnim = toad.GetComponent<SpaceToadns.SpaceToad>()._endAnimation;
            if (!_didWin)
            {
                Timer += Time.deltaTime;
                if (Timer >= 6f && !endAnim)
                {
                    SpawnLaser();
                    Timer = 0f;
                }

                if ((toad.transform.position.x < transform.position.x) && facingRight)
                {
                    FlipFrog();
                }
                else if ((toad.transform.position.x >= transform.position.x) && !facingRight)
                {
                    FlipFrog();
                }

                float dir;
                if (facingRight)
                {
                    dir = -1f;
                }
                else
                {
                    dir = 1f;
                }

                if (!endAnim)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * dir * 2f);
                }
                else
                {
                    Vector3 force = new Vector3(dir*0.25f, 0.2f, 0f);
                    _rb.isKinematic = false;
                    _rb.AddForce(force, ForceMode2D.Impulse);
                    _rb.transform.Rotate(0,0,1f);
                }
            }
        }
        
        private void FlipFrog() 
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        internal void SpawnLaser()
        {
            var rocket = (GameObject) Instantiate(Resources.Load("GameElements/LaserBullet"));
            float dir = 1f;
            float width = GetComponent<SpriteRenderer>().size.x;
            if (facingRight) 
            {
                rocket.GetComponent<LaserBullet>().FlipLaser();
                dir *= -1f;
            }
            rocket.transform.position = new Vector3(transform.position.x - (dir * width), transform.position.y-0.07f, transform.position.z);
            //rocket.transform.SetPositionAndRotation(
              //  new Vector3(transform.position.x - width, transform.position.y, transform.position.z),
                //rocket.transform.rotation);
            Destroy(rocket, 20);
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
                _scoreText = GameObject.Find("Score").GetComponent<Text>();
                _score = Convert.ToInt32(_scoreText.text);
                _score = _score + 1;
                _scoreText.text = _score.ToString();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                //Debug.Log("hello");
            }

        }

        internal void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}