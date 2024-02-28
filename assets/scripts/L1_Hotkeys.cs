using Godot;

public partial class L1_Hotkeys : Godot.Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.U && keyEvent.Pressed)
            {
                this.Visible = !this.Visible;
            }
        }
    }
}
