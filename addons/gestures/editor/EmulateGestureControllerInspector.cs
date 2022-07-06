#if TOOLS

namespace Godot.Gestures.Editor
{
  public sealed class EmulateGestureControllerInspector : EditorInspectorPlugin
  {
    public override bool CanHandle(Object @object)
    {
      return base.CanHandle(@object);
    }

    public override bool ParseProperty(Object @object, int type, string path, int hint, string hintText, int usage)
    {
      return base.ParseProperty(@object, type, path, hint, hintText, usage);
    }

  }
}

#endif // TOOLS