using System;
using Godot;

namespace Processors;

public class ClientEffectsProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.ConnectedNode != null;
    }

    public override void Start()
    {
        base.Start();
        EventService.Subscribe<RemoteEvents.ClientVFX>(OnClientVFX);
        EventService.Subscribe<RemoteEvents.ClientSound>(OnClientSound);
    }

    public override void Process(double delta)
    {
        base.Process(delta);

    }

    void OnClientSound(RemoteEvents.ClientSound evnt)
    {
        if (evnt.SpawnTarget.ConnectedNode != null)
        {
            AudioService.PlaySpatialSound(evnt.FilePath, evnt.SpawnTarget.GetNode<Node>());

        }
    }

    void OnClientVFX(RemoteEvents.ClientVFX evnt)
    {
        if (evnt.SpawnTarget.ConnectedNode != null)
        {
            if (evnt.AttachmentPoint != null)
            {
                VisualService.Spawn(
                    evnt.FilePath,
                    evnt.SpawnTarget.GetNode<Node>().GetNode<Attachment3D>(evnt.AttachmentPoint.ToString())
                    );
            }
            else
            {
                VisualService.Spawn(evnt.FilePath, evnt.SpawnTarget.GetNode<Node>());
            }
        }
    }
}

