#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

namespace Godot.Gestures.Editor
{
  [Tool]
  public sealed class Plugin : EditorPlugin
  {
    private readonly IList<EditorInspectorPlugin> customInspectors;

    public Plugin()
    {
      customInspectors = new List<EditorInspectorPlugin>
      {
        new TouchGestureControllerInspector(),
      };
    }

    public override void _EnterTree()
    {
      base._EnterTree();
      GD.Print("C# Plugin");
      foreach(var inspector in customInspectors)
      {
        AddInspectorPlugin(inspector);
      }
    }

    public override void _ExitTree()
    {
      base._ExitTree();
      foreach(var inspector in customInspectors)
      {
        RemoveInspectorPlugin(inspector);
      }
    }
  }
}

#endif