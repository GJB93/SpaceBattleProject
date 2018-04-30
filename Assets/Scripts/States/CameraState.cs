using UnityEngine;

public abstract class CameraState : State {

    public Camera camera;

    public CameraState(Camera camera)
    {
        this.camera = camera;
    }
}
