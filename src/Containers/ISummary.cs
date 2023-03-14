﻿using AssetManager.Enums;

namespace AssetManager.Containers
{
    internal interface ISummary
    {
        string Name { get; }

        string Description { get; }

        string Effects { get; }

        Source Source { get; }
    }
}