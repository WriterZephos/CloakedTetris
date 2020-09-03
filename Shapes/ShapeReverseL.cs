namespace CloakedTetris
{
    public class ShapeReverseL : AbstractShape
    {

        public ShapeReverseL() : base("RL", 2, new Block[] { new Block(4, 0, "blue"), new Block(5, 0, "blue"), new Block(6, 0, "blue"), new Block(6, 1, "blue") })
        {
        }

        public override void Rotate(Block[,] blocks)
        {
            //Make Segments Available
            Block a = Segments[0];
            Block c = Segments[2];
            Block d = Segments[3];


            //Toggle switch between States
            ToggleState(4);

            //Null all blocks
            NullBlocks(blocks);

            //Switch for the 4 States of a shape
            switch (State)
            {
                case 0:
                    MoveBlock(a, -1, -1);
                    MoveBlock(c, +1, +1);
                    MoveBlock(d, 0, 2);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, 1, 1);
                        MoveBlock(c, -1, -1);
                        MoveBlock(d, 0, -2);
                        State--;
                    }
                    break;
                case 1:
                    MoveBlock(a, 1, -1);
                    MoveBlock(c, -1, 1);
                    MoveBlock(d, -2, 0);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, -1, 1);
                        MoveBlock(c, 1, -1);
                        MoveBlock(d, 2, 0);
                        State--;
                    }
                    break;
                case 2:
                    MoveBlock(a, 1, 1);
                    MoveBlock(c, -1, -1);
                    MoveBlock(d, 0, -2);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, -1, -1);
                        MoveBlock(c, 1, 1);
                        MoveBlock(d, 0, 2);
                        State--;
                    }
                    break;
                case 3:
                    MoveBlock(a, -1, 1);
                    MoveBlock(c, +1, -1);
                    MoveBlock(d, 2, 0);
                    if (Collides(blocks))
                    {
                        MoveBlock(a, 1, -1);
                        MoveBlock(c, -1, 1);
                        MoveBlock(d, -2, 0);
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
