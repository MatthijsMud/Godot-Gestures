using Godot;
using System;

namespace Godot.Gestures
{
  public class InputEventScreenTwist : InputEventAction
  {
    public Vector2 Position { get; set; }
    public float Relative { get; set; }

    public float Distance { get; set; }

    public int Fingers { get; set; }
  }
}