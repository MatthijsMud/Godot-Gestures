using Godot;

namespace Godot.Gestures
{
  public class InputEventMultiScreenDrag : InputEventAction
  {
    public int Fingers { get; set; } = 2;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Relative { get; set; } = Vector2.Zero;

    public InputEventMultiScreenDrag()
    {

    }
  }
}