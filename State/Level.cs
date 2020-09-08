using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Clkd.Assets;
using Clkd.State;

namespace CloakedTetris.State
{
    public class Level : AbstractGameState
    {
        public Block[,] Grid { get; set; }
        public long Delay { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public long Goal { get; set; }

        Level(Block[,] grid, long delay, long goal)
        {
            Grid = grid;
            Delay = delay;
            Goal = goal;
            BoardHeight = grid.GetLength(0);
            BoardWidth = grid.GetLength(1);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Block b in Grid)
            {
                if (b != null)
                {
                    b.Update(gameTime);
                }
            }
        }

        public override List<Renderable> GetRenderables(RenderableCoordinate? renderableCoordinate)
        {
            //Iterate through grid and return RenderablObjects for each one.

            List<Renderable> renderables = new List<Renderable>();

            foreach (Block b in Grid)
            {
                if (b != null)
                {
                    renderables.AddRange(b.GetRenderables(renderableCoordinate));
                }
            }

            return renderables;
        }

        public static Level GetLevel(int levelNumber)
        {
            Block[,] array;
            long delay;
            long scoreG;

            switch (levelNumber)
            {
                case 1:
                    array = new Block[20, 10];
                    delay = 1_000_000_000L;
                    scoreG = 30_000L;
                    return new Level(array, delay, scoreG);

                case 2:
                    array = new Block[20, 10];
                    delay = 900_000_000L;
                    scoreG = 40_000L;
                    return new Level(array, delay, scoreG);

                case 3:
                    array = new Block[20, 10];
                    delay = 800_000_000L;
                    scoreG = 50_000L;
                    return new Level(array, delay, scoreG);

                case 4:
                    array = new Block[20, 10];
                    delay = 700_000_000L;
                    scoreG = 60_000L;
                    return new Level(array, delay, scoreG);

                case 5:
                    array = new Block[20, 10];
                    delay = 600_000_000L;
                    scoreG = 70_000L;
                    return new Level(array, delay, scoreG);

                case 6:
                    array = new Block[20, 10];
                    delay = 500_000_000L;
                    scoreG = 80_000L;
                    return new Level(array, delay, scoreG);

                case 7:
                    array = new Block[20, 10];
                    delay = 400_000_000L;
                    scoreG = 90_000L;
                    return new Level(array, delay, scoreG);

                case 8:
                    array = new Block[20, 10];
                    delay = 300_000_000L;
                    scoreG = 1_00_000L;
                    return new Level(array, delay, scoreG);

                case 9:
                    array = new Block[20, 10];
                    delay = 200_000_000L;
                    scoreG = 1_10_000L;
                    return new Level(array, delay, scoreG);

                case 10:
                    array = new Block[20, 10];
                    delay = 100_000_000L;
                    scoreG = 1_20_000L;
                    return new Level(array, delay, scoreG);

                default:
                    array = new Block[20, 10];
                    delay = 1_000_000_000L;
                    scoreG = 1000L;
                    return new Level(array, delay, scoreG);
            }

        }

        public void CheckRows()
        {
            for (int i = 0; i < BoardHeight; i++)
            {
                bool complete = true;
                for (int j = 0; j < BoardWidth; j++)
                {
                    if (Grid[i, j] == null || (!Grid[i, j].Fixed))
                    {
                        complete = false;
                    }
                }

                if (complete)
                {
                    for (int j = 0; j < BoardWidth; j++)
                    {
                        Grid[i, j].StartFlashAnimation();
                    }
                }
            }
        }
    }

}
