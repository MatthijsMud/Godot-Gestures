using Godot;
using System;

namespace Godot.Gestures
{
  public class InputEventScreenPinch : InputEventAction
  {
    public Vector2 Position { get; set; }
    public float Relative { get; set; }

    public float Factor { get; set; }

    public int Fingers { get; set; }
    

    public InputEventScreenPinch()
    {

    }
  }
}