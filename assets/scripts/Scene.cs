using Godot;

public partial class Scene : Node3D
{
	private Label controls;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		controls = GetNode<Label>("Controls");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
            if (keyEvent.Keycode == Key.Escape)
            {
                GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
                GetTree().Quit();
            }
            if (keyEvent.Keycode == Key.U)
            {
                controls.Visible = !controls.Visible;
            }
            if (keyEvent.Keycode == Key.Key1)
            {
                GetTree().ChangeSceneToFile("res://assets/scenes/Scene1.tscn");
            }
            if (keyEvent.Keycode == Key.Key2)
            {
                GetTree().ChangeSceneToFile("res://assets/scenes/Scene2.tscn");
            }
        }
	}
}
