using System;
using Godot;

public static class ClientEffectsService
{
    public static void Start()
    {
        EventService.Subscribe<RemoteEvents.ClientVFX>(OnClientVFX);
        EventService.Subscribe<RemoteEvents.ClientSound>(OnClientSound);
    }

    static void OnClientSound(RemoteEvents.ClientSound evnt)
    {
        if (evnt.SpawnTarget.ConnectedNode != null)
        {
            AudioService.PlaySpatialSound(evnt.FilePath, evnt.SpawnTarget.GetNode<Node>());

        }
    }

    static void OnClientVFX(RemoteEvents.ClientVFX evnt)
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
