using CloakedTetris.State;

namespace CloakedTetris.Shapes
{
    public class ShapeT : AbstractShape
    {

        public ShapeT() : base("T", 2, new Block[] { new Block(4, 0, "yellow"), new Block(5, 0, "yellow"), new Block(6, 0, "yellow"), new Block(5, 1, "yellow") })
        {
        }

        public override void Rotate(Block[,] blocks)
        {
            //Make Segments Available
            Block a = Segments[0];
            Block c = Segments[2];
            Block d = Segments[3];


            //Toggle switch between states
            ToggleState(4);

            //Null all blocks
            NullBlocks(blocks);

            //Switch for the 4 states of a shape
            switch (State)
            {
                case 0:
                    MoveBlock(a, -1, -1);
                    MoveBlock(c, +1, +1);
                    MoveBlock(d, -1, 1);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, 1, 1);
                        MoveBlock(c, -1, -1);
                        MoveBlock(d, 1, -1);
                        State--;
                    }
                    break;
                case 1:
                    MoveBlock(a, 1, -1);
                    MoveBlock(c, -1, 1);
                    MoveBlock(d, -1, -1);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, -1, 1);
                        MoveBlock(c, 1, -1);
                        MoveBlock(d, 1, 1);
                        State--;
                    }
                    break;
                case 2:
                    MoveBlock(a, 1, 1);
                    MoveBlock(c, -1, -1);
                    MoveBlock(d, 1, -1);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, -1, -1);
                        MoveBlock(c, 1, 1);
                        MoveBlock(d, -1, 1);
                        State--;
                    }
                    break;
                case 3:
                    MoveBlock(a, -1, 1);
                    MoveBlock(c, +1, -1);
                    MoveBlock(d, 1, 1);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, 1, -1);
                        MoveBlock(c, -1, 1);
                        MoveBlock(d, -1, -1);
                        State--;
                    }
                    break;
                default:
                    break;
            }

            foreach (Block b in Segments)
            {
                blocks[b.Y, b.X] = b;
            }
        }
    }
}
