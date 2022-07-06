using System;

namespace Godot.Gestures
{

  public class Twist
  {
    /// <summary>
    /// Angle (in radians) of the twist.
    /// </summary>
    public float Angle { get; }

    public Twist(float angle)
    {
      Angle = angle;
    }

  }

  public interface ITwistGestureRecognizer
  {
    IObservable<Twist> Twists { get; }
  }
}