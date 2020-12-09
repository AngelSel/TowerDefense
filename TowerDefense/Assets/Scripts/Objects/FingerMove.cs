using Lean.Touch;
using UnityEngine;

namespace Objects
{
    public class FingerMove : MonoBehaviour
    {
        private bool isActive;
        private void OnEnable()
        {
            LeanTouch.OnFingerUpdate += FingerOn;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerUpdate -= FingerOn;
        }

        private void Start()
        {
            isActive= true;
        }

        internal void SetState(bool state)
        {
            isActive = state;
        }

        private void FingerOn(LeanFinger finger)
        {
            var plane = new Plane(Vector3.up, 0f);

            if (Screen.height - 15 < finger.ScreenPosition.y)
                finger.ScreenPosition.y = Screen.height - 15;
            else if (finger.ScreenPosition.y < 15) finger.ScreenPosition.y = 15;

            if (finger.ScreenPosition.x > Screen.width - 15)
                finger.ScreenPosition.x = Screen.width - 15;
            else if (finger.ScreenPosition.x < 15) finger.ScreenPosition.x = 15;

            //change here for camera inject
            var ray = Camera.main.ScreenPointToRay(new Vector3(finger.ScreenPosition.x, finger.ScreenPosition.y, 0f));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                var newPosition = ray.GetPoint(distance);
                newPosition.y = 0.1f;
                transform.position = newPosition;
            }

        
        }
    }
}
