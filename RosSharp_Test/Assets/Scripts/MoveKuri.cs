using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class MoveKuri : UnityPublisher<MessageTypes.Geometry.Twist>
    {

        bool u = false;
        bool d = false;
        bool l = false;
        bool r = false;

        Vector3 x_vec = new Vector3(1, 0, 0);
        Vector3 z_vec = new Vector3(0, 0, 1);

        // Update is called once per frame
        void Update()
        {
            //meObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5);
        }

        public void moveUp()
        {
            //u = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
        }

        public void moveDown()
        {
            d = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
        }

        public void moveLeft()
        {
            l = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        public void moveRight()
        {
            r = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }


    }
}
