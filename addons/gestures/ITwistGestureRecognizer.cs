using System;

namespace Godot.Gestures
{

  public class Twist
  {

  }

  public interface ITwistGestureRecognizer
  {
    IObservable<Twist> Twists { get; }
  }
}