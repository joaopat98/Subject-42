using UnityEngine;

namespace JoystickUtils
{
    public static class Joystick
    {
        static Vector2 JoystickDir(string HorizontalAxis, string VerticalAxis)
        {
            var x = Input.GetAxis(HorizontalAxis);
            var y = Input.GetAxis(VerticalAxis);
            var dirRaw = new Vector2(x, y);
            float angle = Mathf.Atan2(y, x);
            float absAngle = Mathf.Atan2(Mathf.Abs(y), Mathf.Abs(x));
            absAngle = absAngle > Mathf.PI / 4 ? Mathf.PI / 2 - absAngle : absAngle;
            float ratio = absAngle == 0 || absAngle == 90 ? 1 : Mathf.Sin(absAngle) / Mathf.Tan(absAngle);
            return ratio * dirRaw;
        }

        public static Vector2 GetJoystick1Dir()
        {
            return JoystickDir("Horizontal", "Vertical");
        }

        public static Vector2 GetJoystick2Dir()
        {
            return JoystickDir("Horizontal2", "Vertical2");
        }
    }
}