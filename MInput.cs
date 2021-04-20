using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    /// <summary>
    /// Basic Mouse input class that handles mouse clicks and presses.
    /// </summary>
    public static class MInput
    {
        public static MouseState State { get; private set; }
        public static MouseState Previous { get; private set; }

        public static int X => State.Position.X;
        public static int Y => State.Position.Y;
        public static Point Position => State.Position;

        public static void Initialize()
        {
            State = Mouse.GetState();
        }

        public static void Update()
        {
            Previous = State;
            State = Mouse.GetState();
        }

        public static bool LeftClick()
        {
            return State.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftPressed()
        {
            return LeftClick() && Previous.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return State.RightButton == ButtonState.Pressed;
        }

        public static bool RightPressed()
        {
            return RightClick() && Previous.RightButton == ButtonState.Released;
        }

        public static int MouseWheelDelta()
        {
            return Math.Sign(State.ScrollWheelValue - Previous.ScrollWheelValue);
        }
    }
}
