using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Clkd.Assets;
using Clkd.State;

namespace CloakedTetris.State
{
    public class Block : AbstractGameState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Fixed { get; set; } = false;
        public bool Flashed { get; set; } = false;
        public SpriteCoordinate SpriteCoordinate;
        public string Color { get; private set; }
        public SpriteAnimationState Animation { get; set; }

        public Block(int x, int y, string color)
        {
            X = x;
            Y = y;
            Color = color;

            switch (color)
            {
                case "red":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 0, 0, 32, 32);
                    break;
                case "green":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 32, 0, 32, 32);
                    break;
                case "blue":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 64, 0, 32, 32);
                    break;
                case "lightblue":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 96, 0, 32, 32);
                    break;
                case "yellow":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 128, 0, 32, 32);
                    break;
                case "orange":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 160, 0, 32, 32);
                    break;
                case "purple":
                    SpriteCoordinate = new SpriteCoordinate("blocks", 192, 0, 32, 32);
                    break;

            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Animation != null && !Animation.Completed)
            {
                Animation.Update(gameTime);
                Animation.RenderableCoordinate = new RenderableCoordinate(X * 32, Y * 32, 10, 32, 32);
            }
            else if (Animation != null && Animation.Completed && Fixed)
            {
                Flashed = true;
                Animation = null;
            }
        }

        public override List<Renderable> GetRenderables(RenderableCoordinate? renderableCoordinate)
        {
            if (Animation != null && !Animation.Completed)
            {

                return Animation.GetRenderables();
            }
            else
            {
                Renderable renderable = new Renderable(SpriteCoordinate, new RenderableCoordinate(X * 32, Y * 32, 10, 32, 32), false);
                return new List<Renderable>() { renderable };
            }
        }

        public void StartFlashAnimation()
        {
            var animation = new SpriteAnimation(
                frames: new List<SpriteCoordinate> {
                    new SpriteCoordinate("blocks", 224, 0, 32, 32),
                    SpriteCoordinate
                },
                interval: new TimeSpan(0, 0, 0, 0, 300),
                iterationLimit: 4
            );
            var animationState = new SpriteAnimationState(animation);
            Animation = animationState;
        }
    }
}
