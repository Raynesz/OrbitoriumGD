using Godot;

public partial class Sun_S2 : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position += new Vector3(20.0f * (float)delta, 0, 0);
    }
}
