using System;
using Godot;


namespace Blocks;

public class AttackBlock : Block
{
    public int SwingNumber = 0;
    public double LastSwingTime = 0;
    public double LastComboTime = 0;
}

