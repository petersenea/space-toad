using UnityEngine;
using System.Collections;

namespace Assets.Code
{

    public class LaserBullet : MonoBehaviour
    {

        public float dir = 1f;
        internal void Start()
        {

        }

        internal void Update()
        {
            
            transform.Translate(Vector3.left * Time.deltaTime * 2.5f * dir);
        }

        public void FlipLaser()
        {
            dir *= -1f;
        }
    }
}