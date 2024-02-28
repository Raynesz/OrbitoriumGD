using Godot;

public partial class L2_Sun : RigidBody3D
{
	private Vector3 orbitCenter = new Vector3(0, 0, 0);  // Center of the orbit
	private float orbitRadius = 15.0f;  // Radius of the orbit
	private float orbitSpeed = 1.0f;  // Speed of rotation
	private float angle = 0.0f;  // Current angle of rotation
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{		
		// Update the angle based on the time passed and speed
		angle += orbitSpeed * (float)delta;
		
		// Calculate the new position using trigonometry
		float x = orbitCenter.X + orbitRadius * Mathf.Cos(angle);
		float z = orbitCenter.Z + orbitRadius * Mathf.Sin(angle);
		
		// Set the new position
		this.Position = new Vector3(x, orbitCenter.Y, z);
	}
}
