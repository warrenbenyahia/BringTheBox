using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


namespace Assets.Scripts
{
    public static class ControlManager
    {
        public static bool IsInputEnabled = true;

        public static bool IsTaped()
        {
            return IsInputEnabled && (Input.GetMouseButtonUp((int)MouseButton.LeftMouse) ||
                (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended));           
        }

        private static Vector3 GetTouchPosition()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#elif UNITY_ANDROID
            return Input.GetTouch(0).position;
#endif
        }

        public static bool IsTouchingGameObject(GameObject go)
        {
            var position = GetTouchPosition();
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            return Physics.Raycast(ray, out hit) && hit.transform.gameObject == go;
        }
    }
}
