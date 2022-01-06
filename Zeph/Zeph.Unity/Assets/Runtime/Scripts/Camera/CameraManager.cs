using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// This camera manager will follow behind the player, moving smoothly to look to where the player is looking.
    /// BUT, if the right mouse button (inputManager.cameraInputFreeLook) is pressed down, the camera will be able to free look. 
    /// And free look will then be enabled until the player moves again.
    /// </remarks>
    public class CameraManager : MonoBehaviour {
        /// <summary>
        /// The object the camera will follow
        /// </summary>
        public Transform targetTransform;
        /// <summary>
        /// The object the camera uses to pivot
        /// </summary>
        public Transform cameraPivotTransform;
        public InputManager inputManager;
        /// <summary>
        /// The transform of the actual camera object
        /// </summary>
        public Transform cameraTransform;
        /// <summary>
        /// The layers we want our camera to collide with
        /// </summary>
        public LayerMask collisionLayers;

        /// <summary>
        /// How much the camera will jump off of objects its colliding with
        /// </summary>
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        public float cameraCollisionRadius = 0.2f;
        public float cameraFollowSpeed = 0.2f;
        public float cameraLookSpeed = 0.5f;
        public float cameraPivotSpeed = 0.5f;
        public float cameraRotateSpeed = 2f;

        public float lookAngle; //camera look up and down
        public float pivotAngle; //camera look left and right
        public float minimumPivotAngle = -35f;
        public float maximumPivotAngle = 35f;

        float distanceFromPlayer;
        public float cameraMovementInOutSpeed = 5f;
        public float minDistanceFromPlayer = 1f;
        public float maxDistanceFromPlayer = 10f;

        //public CameraManagerLookMode lookMode = CameraManagerLookMode.FollowPlayer;

        private Vector3 cameraFollowVelocity = Vector3.zero;
        //private float defaultPosition;
        //private Vector3 cameraVectorPosition;

        private void Awake() {
            //defaultPosition = cameraTransform.localPosition.z;

            distanceFromPlayer = Vector3.Distance(cameraTransform.position, cameraPivotTransform.position);
        }

        public void HandleAllCameraMovement() {
            FollowTarget();
            MoveCamera();
            RotateCamera();
            HandleCameraCollisions();
        }

        void FollowTarget() {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed); //move something softly between 1 location to another

            transform.position = targetPosition;
        }

        void MoveCamera() {
            distanceFromPlayer = Mathf.Clamp(distanceFromPlayer - Mathf.Clamp(inputManager.cameraMoveInOut, -1, 1), minDistanceFromPlayer, maxDistanceFromPlayer);
            var currentDistance = Vector3.Distance(cameraTransform.position, cameraPivotTransform.position);

            var distanceDifference = (distanceFromPlayer - currentDistance);

            if (distanceDifference != 0f) {
                var distanceToMove = Mathf.Min(cameraMovementInOutSpeed, Mathf.Abs(distanceDifference));
                cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, cameraPivotTransform.position, -Mathf.Sign(distanceDifference) * distanceToMove * Time.deltaTime);
            }
        }

        void RotateCamera() {
            Vector3 rotation;
            Quaternion targetRotation;

            if (inputManager.cameraInputFreeLook) {
                //lookMode = CameraManagerLookMode.FreeLook;
                lookAngle = lookAngle + (inputManager.cameraInputHorizontal * cameraLookSpeed);
                pivotAngle = pivotAngle - (inputManager.cameraInputVertical * cameraPivotSpeed);
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);
            } else /*if (lookMode != CameraManagerLookMode.FreeLook) This is commented out cos I was going to do the thing where the camera resets to behind where the player is moving, but I feel this is a tad more natural? Hopefully anyway...*/{
                lookAngle = lookAngle + (inputManager.cameraInputRotate * cameraRotateSpeed);
            }

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        void HandleCameraCollisions() {
            //float targetPosition = defaultPosition;

            //RaycastHit hit;
            //Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            //direction.Normalize();

            //if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) {
            //    float distance = Vector3.Distance(cameraPivotTransform.position, hit.point); //distance between pivot and thing we hit
            //    targetPosition = -(distance - cameraCollisionOffset);
            //}

            //if (Mathf.Abs(targetPosition) < minimumCollisionOffset) {
            //    targetPosition = targetPosition - minimumCollisionOffset;
            //}

            //cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f); //0.2f is time
            //cameraTransform.localPosition = cameraVectorPosition;
        }
    }

    //public enum CameraManagerLookMode {
    //    FollowPlayer = 1,
    //    FreeLook = 2
    //}
}