using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    public class RocketBullet : MonoBehaviour
    {
        
        public float dir = 1f;
        

        internal void Start()
        {
            
        }

        internal void Update()
        {
            transform.Translate(Vector3.right * Time.deltaTime * 20f * dir);
        }
        
        public void FlipRocket() 
        {
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            dir *= -1f;
        }
        
    }
}