using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _project.Scripts.Services
{
    public class VRSwipe
    {
        private Vector3 _startPosition;
        private Vector3 _swipeVector;
        private bool _isSwiping;

        private ActionBasedController _leftController;
        private ActionBasedController _rightController;

        public VRSwipe(XRIDefaultInputActions input, ActionBasedController leftController, ActionBasedController rightController)
        {
            _leftController = leftController;
            _rightController = rightController;
            
            input.Enable();
            input.XRILeftHandInteraction.Activate.started += context => OnGripStarted(context, true);
            input.XRILeftHandInteraction.Activate.canceled += context => OnGripCanceled(context, true);
            input.XRIRightHandInteraction.Activate.started += context => OnGripStarted(context, false);
            input.XRIRightHandInteraction.Activate.canceled += context => OnGripCanceled(context, false);
        }

        private void OnGripStarted(InputAction.CallbackContext context, bool isLeftController)
        {
            if (!_isSwiping)
            {
                _isSwiping = true;
                _startPosition = isLeftController ? _leftController.transform.position : _rightController.transform.position;
            }
        }

        private void OnGripCanceled(InputAction.CallbackContext context, bool isLeftController)
        {
            if (_isSwiping)
            {
                _isSwiping = false;
                Vector3 endPosition = isLeftController ? _leftController.transform.position : _rightController.transform.position;
                _swipeVector = endPosition - _startPosition;
            }
        }

        public Vector3 GetSwipe()
        {
            Vector3 swipe = _swipeVector;
            _swipeVector = Vector3.zero;
            return swipe;
        }
    }
}
