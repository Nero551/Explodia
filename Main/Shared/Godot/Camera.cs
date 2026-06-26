using System;
using Godot;

public partial class Camera : SpringArm3D
{
    [Export] public float MaxSpringLength = 6;
    [Export] public float MinSpringLength = 1;
    [Export] public float MouseSensitivity = 0.002f;
}
