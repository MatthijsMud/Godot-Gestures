using System;

namespace Godot.Gestures
{
  public sealed class Tap
  {

  }

  public interface ITapGestureRecognizer
  {
    IObservable<Tap> Taps { get; }
  }
}