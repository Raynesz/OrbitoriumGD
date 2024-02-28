using Godot;

public partial class L1_Planet : RigidBody3D
{
	private RigidBody3D Sun;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var random = new RandomNumberGenerator();
		random.Randomize();
		int randomRadius = random.RandiRange(10, 20);
		int randomAngle = random.RandiRange(1, 360);
		var randomY = random.RandiRange(0, 7);
		var randomX = randomRadius * Mathf.Cos(randomAngle);
		var randomZ = randomRadius * Mathf.Sin(randomAngle);
		
		Sun = GetNode<RigidBody3D>("/root/Node3D/WorldEnvironment/SunRigidBody3D");
		this.Position = new Vector3(Sun.Position.X+randomX, Sun.Position.Y+randomY, Sun.Position.Z+randomZ);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 SunGravity = Sun.Position - this.Position;
		this.ApplyCentralForce(30*SunGravity.Normalized());
	}
}
