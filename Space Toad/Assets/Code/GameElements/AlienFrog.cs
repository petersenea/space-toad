using UnityEngine;
using System.Collections;

namespace Assets.Code.AlienFrog
{

    public class AlienFrog : MonoBehaviour
    {
        private bool _didWin;

        internal void Start()
        {
            _didWin = false;
        }
        
        internal void Update()
        {
            if (!_didWin)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }

        internal void OnTriggerEnter2D(Collider2D collision)
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
    }
}