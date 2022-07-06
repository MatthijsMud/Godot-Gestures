using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Godot.Gestures
{
  public sealed class GodotInputEventBroadcaster : Node
  {

    private readonly CompositeDisposable subscriptions;
    public GodotInputEventBroadcaster()
    {
      subscriptions = new CompositeDisposable();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        subscriptions.Dispose();
      }
      base.Dispose(disposing);
    }

    public override void _Ready()
    {
      base._Ready();
      var compound = new CompositeDisposable();
      foreach (var child in GetChildren())
      {
        var tapper = child as ITapGestureRecognizer;
        tapper?.Taps
        .Do(tap => { Input.ParseInputEvent(new InputEventSingleScreenTap()); })
        .Subscribe();

        var longPresser = child as ILongPressRecognizer;
        longPresser?.LongPresses
        .Do(longPress => {Input.ParseInputEvent(new InputEventSingleScreenLongPress()); })
        .Subscribe();

        var pincher = child as IPinchGestureRecognizer;
        var pinches = pincher?.Pinches
        .Do(pinch => {
          Input.ParseInputEvent(new InputEventScreenPinch{
            Factor = pinch.Factor,
          }); 
        })
        .Subscribe();
        if (pinches != null) compound.Add(pinches);

        var twister = child as ITwistGestureRecognizer;
        twister?.Twists
        .Do(twist => { Input.ParseInputEvent(new InputEventScreenTwist()); })
        .Subscribe();
      }
    }

  }

}