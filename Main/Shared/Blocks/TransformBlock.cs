using System;
using Godot;

namespace Blocks;

public class TransformBlock : Block
{
    [Replicated(ReplicationMode.Unreliable)] public Basis Basis = Basis.Identity;
    [Replicated(ReplicationMode.Unreliable)] public Vector3 Position;
    [Replicated(ReplicationMode.Unreliable)] public Vector3 EulerRotation;
    [Replicated(ReplicationMode.Unreliable)] public Quaternion QuaternionRotation => Basis.GetRotationQuaternion();
}
