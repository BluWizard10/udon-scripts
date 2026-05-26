
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace BluWizard.Udon.Local
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AnalogClock : UdonSharpBehaviour
    {
        [Header("Clock Hand Transforms")]
        public Transform hourHand;
        public Transform minuteHand;
        public Transform secondHand;

        [Header("Rotation Axis")]
        public Vector3 hourAxis = new Vector3(0f, 0f, 1f);
        public Vector3 minuteAxis = new Vector3(0f, 0f, 1f);
        public Vector3 secondAxis = new Vector3(0f, 0f, 1f);
        [Tooltip("If enabled, changes the Second Hand to rotate in an analog tick-tock motion.")]
        public bool quartzMotion = false;

        // Cache the starting local rotation of each hand so the configured axis
        // rotates from the hand's authored "12 o'clock" pose.
        private Quaternion hourBaseRotation;
        private Quaternion minuteBaseRotation;
        private Quaternion secondBaseRotation;

        private void Start()
        {
            if (hourHand != null) hourBaseRotation = hourHand.localRotation;
            if (minuteHand != null) minuteBaseRotation = minuteHand.localRotation;
            if (secondHand != null) secondBaseRotation = secondHand.localRotation;
        }

        private void Update()
        {
            System.DateTime now = System.DateTime.Now;

            // Use fractional units so hands sweep smoothly, like a real analog clock. If quartzMotion is true, change to use Quartz motion instead.
            float seconds = quartzMotion ? now.Second : (float)(now.Second + now.Millisecond / 1000.0);
            float minutes = now.Minute + seconds / 60f;
            float hours = (now.Hour % 12) + minutes / 60f;

            // 360 / 12 = 30 deg per hour
            // 360 / 60 = 6 deg per minute
            // 360 / 60 = 6 deg per second
            float hourAngle = hours * 30f;
            float minuteAngle = minutes * 6f;
            float secondAngle = seconds * 6f;

            if (hourHand != null) hourHand.localRotation = hourBaseRotation * Quaternion.AngleAxis(hourAngle, hourAxis);
            if (minuteHand != null) minuteHand.localRotation = minuteBaseRotation * Quaternion.AngleAxis(minuteAngle, minuteAxis);
            if (secondHand != null) secondHand.localRotation = secondBaseRotation * Quaternion.AngleAxis(secondAngle, secondAxis);
        }
    }
}
