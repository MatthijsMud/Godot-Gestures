using Godot;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Godot.Gestures
{
  public class EmulateGestureController : Node, 
  IPinchGestureRecognizer, 
  ITwistGestureRecognizer,
  ILongPressRecognizer,
  ITapGestureRecognizer
  {
    private readonly ISubject<InputEvent> gestures;
    public IObservable<Twist> Twists { get; }
    public IObservable<Pinch> Pinches { get; }
    public IObservable<LongPress> LongPresses { get; }
    public IObservable<Tap> Taps { get; }

    public EmulateGestureController()
    {
      gestures = new Subject<InputEvent>();

      Pinches = gestures
      .OfType<InputEventMouseButton>()
      .Where(@event => {
        // Scroll events in Godot are represented by "clicks" of two virtual
        // buttons; one for scrolling up, the other for scrolling down.
        // Each click also receives a syntethic release event.
        var button = (ButtonList)@event.ButtonIndex;
        return @event.Pressed 
        && (button == ButtonList.WheelUp || button == ButtonList.WheelDown); 
      })
      .Select(@event => new Pinch((ButtonList)@event.ButtonIndex switch 
      {
        ButtonList.WheelUp => 1f,
        ButtonList.WheelDown => -1f,
        _ => 0 // Should not happen based on the above filtering!
      }))
      .Publish()
      .RefCount();

      Twists = Observable.Empty<Twist>();
      Taps = Observable.Empty<Tap>();
      LongPresses = Observable.Empty<LongPress>();
    }

    public override void _Ready()
    {
    }

    public override void _UnhandledInput(InputEvent @event)
    {
      base._UnhandledInput(@event);
      gestures.OnNext(@event);
    }
  }
}
