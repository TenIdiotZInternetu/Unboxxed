using System;
using UnityEngine;

namespace MonoScripts
{
    /// <summary>
    /// Listens to the Unity's Input Manager and triggers events based on the input
    /// </summary>
    public class Controls : MonoBehaviour
    {
        /*--------------------- Public Fields -------------------*/
        
        /// <summary>
        /// Value of the Input Manager's virtual horizontal axis (A/D)
        /// </summary>
        public static float MoveHorizontal;
        
        /// <summary>
        /// Value of the Input Manager's virtual vertical axis (W/S)
        /// </summary>
        public static float MoveVertical;
        
        public static bool Action1Held;
        public static bool JumpHeld;
        public static bool SubmitHeld;
        public static bool CancelHeld;
        
        /*------------------------- Events ----------------------*/
        
        /// <summary>
        /// Invoked when the primary action button is pressed (K/Z)
        /// </summary>
        public static event Action Action1;
        
        /// <summary>
        /// Invoked when the primary action button is released (K/Z)
        /// </summary>
        public static event Action Action1Release;
        
        /// <summary>
        /// Invoked when the secondary action button is pressed (L/X)
        /// </summary>
        public static event Action Jumps;
        
        /// <summary>
        /// Invoked when the secondary action button is pressed (L/X)
        /// </summary>
        public static event Action JumpsRelease;
        
        /// <summary>
        /// Invoked when the submit button is pressed (Enter)
        /// </summary>
        public static event Action Submit;
        
        /// <summary>
        /// Invoked when the submit button is released (Enter)
        /// </summary>
        public static event Action SubmitRelease;
        
        /// <summary>
        /// Invoked when the cancel button is pressed (Esc)
        /// </summary>
        public static event Action Cancel;
        
        /// <summary>
        /// Invoked when the cancel button is released (Esc)
        /// </summary>
        public static event Action CancelRelease;
        
        /// <summary>
        /// Invoked when the vertical axis is moved upwards, with its value as a parameter
        /// </summary>
        public static event Action<float> MovesUp;
        
        /// <summary>
        /// Invoked when the vertical axis is moved downwards, with its value as a parameter
        /// </summary>
        public static event Action<float> MovesDown;
        
        /// <summary>
        /// Invoked when the horizontal axis is moved left, with its value as a parameter
        /// </summary>
        public static event Action<float> MovesLeft;
        
        /// <summary>
        /// Invoked when the horizontal axis is moved right, with its value as a parameter
        /// </summary>
        public static event Action<float> MovesRight;

        /*----------------------- Unity Callbacks ------------------*/
        
        void Update()
        {
            MoveHorizontal = Input.GetAxis("Horizontal");
            MoveVertical = Input.GetAxis("Vertical");
            
            Action1Held = Input.GetButton("Fire1");
            JumpHeld = Input.GetButton("Jump");
            SubmitHeld = Input.GetButton("Submit");
            CancelHeld = Input.GetButton("Cancel");
            
            if (Input.GetButtonDown("Fire1")) Action1?.Invoke();
            if (Input.GetButtonUp("Fire1")) Action1Release?.Invoke();
            
            if (Input.GetButtonDown("Jump")) Jumps?.Invoke();
            if (Input.GetButtonUp("Jump")) JumpsRelease?.Invoke();
            
            if (Input.GetButtonDown("Submit")) Submit?.Invoke();
            if (Input.GetButtonUp("Submit")) SubmitRelease?.Invoke();
            
            if (Input.GetButtonDown("Cancel")) Cancel?.Invoke();
            if (Input.GetButtonUp("Cancel")) CancelRelease?.Invoke();
            
            if (MoveVertical > 0.001f) MovesUp?.Invoke(MoveVertical);
            if (MoveVertical < -0.001f) MovesDown?.Invoke(MoveVertical);
            if (MoveHorizontal < -0.001f) MovesLeft?.Invoke(MoveHorizontal);
            if (MoveHorizontal > 0.001f) MovesRight?.Invoke(MoveHorizontal);
        }
    }
}