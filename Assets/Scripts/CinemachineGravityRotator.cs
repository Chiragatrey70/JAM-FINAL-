using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CinemachineGravityRotator : CinemachineExtension
{
    [Tooltip("Reference to the script on the player that manages gravity flipping.")]
    [SerializeField] private PlayerGravity playerGravityScript;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body && playerGravityScript != null)
        {
            // Get the player's current 'up' direction
            Vector3 playerUp = playerGravityScript.transform.up;

            // Smoothly interpolate the camera's reference up toward the player's up
            state.ReferenceUp = Vector3.Slerp(state.ReferenceUp, playerUp, deltaTime * 10f);
        }
    }
}
