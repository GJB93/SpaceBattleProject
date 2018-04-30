using UnityEngine;

public class FollowCameraState : CameraState {

	public FollowCameraState(Camera camera):base(camera)
    {
        Debug.Log("Creating FollowYamato instance");
    }

    public override void Enter()
    {
        camera.enabled = true;
    }

    public override void Exit()
    {
        camera.enabled = false;
    }

    public override void Update()
    {
    }
}
