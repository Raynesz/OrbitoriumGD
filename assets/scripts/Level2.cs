using Godot;

public partial class Level2: Node3D
{
	private Camera3D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera  = GetNode<Camera3D>("/root/Node3D/WorldEnvironment/FreeLookCamera");
        resetCameraRotation();
        camera.RotateX(-Mathf.Pi / 4.0f);
        camera.Position = new Vector3(0, 15, 25);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Keycode == Key.Escape && keyEvent.Pressed)
			{
				GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
				GetTree().Quit();
			}
            if (keyEvent.Keycode == Key.F && keyEvent.Pressed)
            {
				resetCameraRotation();
                camera.RotateX(-Mathf.Pi / 4.0f);
                camera.Position = new Vector3(0, 15, 25);
            }
            if (keyEvent.Keycode == Key.Y && keyEvent.Pressed)
            {
				resetCameraRotation();
                camera.RotateX(-Mathf.Pi/2.0f);
                camera.Position = new Vector3(0, 30, 0);
            }
            if (keyEvent.Keycode == Key.X && keyEvent.Pressed)
            {
                resetCameraRotation();
                camera.RotateY(Mathf.Pi / 2.0f);
                camera.Position = new Vector3(30, 0, 0);
            }
        }
	}

	private void resetCameraRotation ()
	{
        camera.Position = new Vector3(0, 0, 0);
        camera.Rotation = new Vector3(0, 0, 0);
    }
}
