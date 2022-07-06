using System;

namespace Godot.Gestures
{
  public sealed class LongPress
  {

  }

  public interface ILongPressRecognizer
  {
    IObservable<LongPress> LongPresses { get; }
  }
}