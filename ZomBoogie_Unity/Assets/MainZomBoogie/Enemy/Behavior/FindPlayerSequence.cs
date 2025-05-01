using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Player", category: "Flow", id: "e26a24370e0f3232da120bef5bac1c13")]
public partial class FindPlayerSequence : Composite
{
    [SerializeReference] public Node Success;
    [SerializeReference] public Node Failed;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
            return Status.Failure;
    
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

