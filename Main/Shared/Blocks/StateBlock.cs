using System;
using System.Collections.Generic;
using Godot;


namespace Blocks;

public class StateBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public MainState MainState;
    public Dictionary<string, double> ActiveStates = [];
}

