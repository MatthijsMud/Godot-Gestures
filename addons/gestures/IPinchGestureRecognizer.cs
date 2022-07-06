using System;

namespace Godot.Gestures
{

  public class Pinch
  {
    public float Factor { get; }

    public Pinch(float factor)
    {
      Factor = factor;
    }
  }

  public interface IPinchGestureRecognizer
  {
    public IObservable<Pinch> Pinches { get; }
  }
}