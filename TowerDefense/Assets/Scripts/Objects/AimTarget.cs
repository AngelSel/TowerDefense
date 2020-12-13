using UnityEngine;
using Lean.Touch;

namespace Objects
{
    public class AimTarget : MonoBehaviour
    {
        //private Camera _camera = default;
        private Vector3 _remainingTranslation;
        private LeanFingerFilter _use = new LeanFingerFilter(true);
        
        private void OnEnable()
        {
            LeanTouch.OnFingerUpdate += AimTranslate;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerUpdate -= AimTranslate;
        }
        
        private void AimTranslate(LeanFinger finger)
        {
            var fingers = _use.GetFingers();
            var screenDelta = LeanGesture.GetScreenDelta(fingers);
            if (screenDelta != Vector2.zero)
                Translate(screenDelta);
        }

        private void Translate(Vector2 screenDelta)
        {
            //var camera = LeanTouch.GetCamera(_camera, gameObject);
            var camera = Camera.main;

            if (camera != null)
            {
                var screenPoint = camera.WorldToScreenPoint(transform.position);

                screenPoint += (Vector3)screenDelta;
                
                if (Screen.height - 15 < screenPoint.y)
                    screenPoint.y = Screen.height - 15;
                else if (screenPoint.y < 15) screenPoint.y = 15;

                if (screenPoint.x > Screen.width - 15)
                    screenPoint.x = Screen.width - 15;
                else if (screenPoint.x < 15) screenPoint.x = 15;

                Vector3 newPos = camera.ScreenToWorldPoint(screenPoint);
                newPos.y = 0.1f;
                transform.position = newPos;
            }
        }
    }
}
