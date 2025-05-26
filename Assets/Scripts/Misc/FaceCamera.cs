using UnityEngine;

namespace Misc
{
    public class FaceCamera : MonoBehaviour
    {
        public Camera camera { get; set; } = null;

        private void Update()
        {
            //if(camera!=null)
            {
                transform.LookAt(transform.position+ Camera.main.transform.rotation *Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            }
        }
    }
}
