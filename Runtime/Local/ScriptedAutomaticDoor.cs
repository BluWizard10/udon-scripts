
using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace BluWizard.Udon.Local
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScriptedAutomaticDoor : UdonSharpBehaviour
    {
        [Tooltip("This should be the target Transform of the Door.")]
        public Transform door;

        [Header("Opened & Closed Positions")]
        public Transform closedPosition;
        public Transform openPosition;
        
        [Header("Travel Duration (in Seconds)")]
        public float openDuration = 0.5f;
        public float closeDuration = 0.5f;
        [Tooltip("Sets how long to wait before the Door closes.")]
        public float closeDelay = 3.0f;

        [Header("Scripted Animations")]
        public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Sound Effects")]
        public AudioSource audioSource;
        public AudioClip openingSound;
        public AudioClip closingSound;

        private float progress = 0f;
        private bool wantOpen = false;
        private float closeTimer = 0f;

        private void Start()
        {
            if (door == null) door = transform;
        }

        private void Update()
        {
            if (closedPosition == null || openPosition == null || door == null) return;

            if (closeTimer > 0f)
            {
                closeTimer -= Time.deltaTime;
                if (closeTimer <= 0f)
                {
                    closeTimer = 0f;
                    wantOpen = false;
                    if (progress > 0f) PlayClip(closingSound);
                }
            }

            float target = wantOpen ? 1f : 0f;
            if (progress != target)
            {
                float duration = wantOpen ? openDuration : closeDuration;
                float step = duration > 0f ? (Time.deltaTime / duration) : 1f;
                progress = Mathf.MoveTowards(progress, target, step);

                float eased = easeCurve.Evaluate(progress);
                door.position = Vector3.LerpUnclamped(closedPosition.position, openPosition.position, eased);
                door.rotation = Quaternion.SlerpUnclamped(closedPosition.rotation, openPosition.rotation, eased);
            }
        }

        // Custom Event entry points. Call these via SendCustomEvent from another
        // UdonBehaviour (e.g. PlayerColliderTrigger) to drive the door.
        public void _OpenDoor()
        {
            if (!wantOpen && progress < 1f) PlayClip(openingSound);
            wantOpen = true;
            closeTimer = 0f;
        }

        public void _CloseDoor()
        {
            closeTimer = closeDelay;
        }

        private void PlayClip(AudioClip clip)
        {
            if (audioSource != null && clip != null) audioSource.PlayOneShot(clip);
        }
    }
}
