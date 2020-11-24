using UnityEngine;
using System.Collections;

namespace Assets.Code.LaserBullet
{

    public class LaserBullet : MonoBehaviour
    {


        internal void Start()
        {
            
        }

        internal void Update()
        {
            
            transform.Translate(Vector3.right * Time.deltaTime * -2f);
        }
    }
}