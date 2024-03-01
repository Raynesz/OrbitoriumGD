using Godot;

public partial class FreeLookCamera : Camera3D
{
    // Modifier keys' speed multiplier
    private const float SHIFT_MULTIPLIER = 2.5f;
    private const float ALT_MULTIPLIER = 1.0f / SHIFT_MULTIPLIER;

    // Mouse state
    private Vector2 _mousePosition = new Vector2(0.0f, 0.0f);
    private float _totalPitch = 0.0f;

    // Movement state
    private Vector3 _direction = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private float _acceleration = 30f;
    private float _deceleration = -10f;
    private float _velMultiplier = 4f;

    // Keyboard state
    private bool _w = false;
    private bool _s = false;
    private bool _a = false;
    private bool _d = false;
    private bool _q = false;
    private bool _e = false;
    private bool _shift = false;
    private bool _alt = false;

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;

    public float Sensitivity { get; set; } = 0.25f;

    public override void _Ready()
    {
        defaultRotation = this.Rotation;
        defaultPosition = this.Position;
    }

    public override void _Input(InputEvent @event)
    {
        // Receives mouse motion
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _mousePosition = mouseMotion.Relative;
        }

        // Receives mouse button input
        if (@event is InputEventMouseButton mouseButton)
        {
            switch (mouseButton.ButtonIndex)
            {
                case MouseButton.Right:
                    Input.MouseMode= mouseButton.Pressed ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible;
                    break;
                case MouseButton.WheelUp:
                    _velMultiplier = Mathf.Clamp(_velMultiplier * 1.1f, 0.2f, 20f);
                    break;
                case MouseButton.WheelDown:
                    _velMultiplier = Mathf.Clamp(_velMultiplier / 1.1f, 0.2f, 20f);
                    break;
            }
        }

        // Receives key input
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.W)
            {
                _w = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.S)
            {
                _s = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.A)
            {
                _a = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.D)
            {
                _d = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.Q)
            {
                _q = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.E)
            {
                _e = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.Shift)
            {
                _shift = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.Alt)
            {
                _alt = keyEvent.Pressed;
            }
            else if (keyEvent.Keycode == Key.F && keyEvent.Pressed)
            {
                resetCameraRotation();
                this.Rotation = defaultRotation;
                this.Position = defaultPosition;
                //this.RotateX(-Mathf.Pi / 14.0f);
                //this.Position = new Vector3(this.Position.X, 5, 30);
            }
            else if (keyEvent.Keycode == Key.Y && keyEvent.Pressed)
            {
                resetCameraRotation();
                this.RotateX(-Mathf.Pi / 2.0f);
                this.Position = new Vector3(this.Position.X, 30, 0);
            }
            else if (keyEvent.Keycode == Key.X && keyEvent.Pressed)
            {
                resetCameraRotation();
                this.Position = new Vector3(this.Position.X, 0, 30);
            }
        }
    }

    public override void _Process(double delta)
    {
        _UpdateMouseLook();
        _UpdateMovement((float)delta);
    }

    private void _UpdateMovement(float delta)
    {
        // Computes desired direction from key states
        _direction = new Vector3((_d ? 1f : 0f) - (_a ? 1f : 0f), (_e ? 1f : 0f) - (_q ? 1f : 0f), (_s ? 1f : 0f) - (_w ? 1f : 0f));

        // Computes the change in velocity due to desired direction and "drag"
        var offset = _direction.Normalized() * _acceleration * _velMultiplier * delta
            + _velocity.Normalized() * _deceleration * _velMultiplier * delta;

        // Compute modifiers' speed multiplier
        var speedMulti = 1f;
        if (_shift) speedMulti *= SHIFT_MULTIPLIER;
        if (_alt) speedMulti *= ALT_MULTIPLIER;

        // Checks if we should bother translating the camera
        if (_direction == Vector3.Zero && offset.LengthSquared() > _velocity.LengthSquared())
        {
            // Sets the velocity to 0 to prevent jittering due to imperfect deceleration
            _velocity = Vector3.Zero;
        }
        else
        {
            // Clamps speed to stay within maximum value (_velMultiplier)
            _velocity.X = Mathf.Clamp(_velocity.X + offset.X, -_velMultiplier, _velMultiplier);
            _velocity.Y = Mathf.Clamp(_velocity.Y + offset.Y, -_velMultiplier, _velMultiplier);
            _velocity.Z = Mathf.Clamp(_velocity.Z + offset.Z, -_velMultiplier, _velMultiplier);

            Translate(_velocity * delta * speedMulti);
        }
    }

    private void _UpdateMouseLook()
    {
        // Only rotates mouse if the mouse is captured
        if (Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            _mousePosition *= Sensitivity;
            var yaw = _mousePosition.X;
            var pitch = _mousePosition.Y;
            _mousePosition = new Vector2(0f, 0f);

            // Prevents looking up/down too far
            pitch = Mathf.Clamp(pitch, -90 - _totalPitch, 90 - _totalPitch);
            _totalPitch += pitch;

            RotateY(Mathf.DegToRad(-yaw));
            RotateObjectLocal(new Vector3(1f, 0f, 0f), Mathf.DegToRad(-pitch));
        }
    }

    private void resetCameraRotation()
    {
        this.Position = new Vector3(0, 0, 0);
        this.Rotation = new Vector3(0, 0, 0);
    }
}