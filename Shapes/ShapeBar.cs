namespace CloakedTetris
{
    public class ShapeBar : AbstractShape
    {

        public ShapeBar() : base("Bar", 2, new Block[] { new Block(4, 0, "red"), new Block(5, 0, "red"), new Block(6, 0, "red"), new Block(7, 0, "red") })
        {
        }

        public override void Rotate(Block[,] blocks)
        {
            //Make Segments Available
            Block a = Segments[0];
            Block c = Segments[2];
            Block d = Segments[3];

            //toggle between 2 States
            ToggleState(2);
            //Clear blocks
            NullBlocks(blocks);

            //Dependent on State toggle between 2 possible bar States
            if (State == 1)
            {

                MoveBlock(a, 1, -1);
                MoveBlock(c, -1, 1);
                MoveBlock(d, -2, +2);

                if (Collides(blocks))
                {
                    MoveBlock(a, -1, 1);
                    MoveBlock(c, 1, -1);
                    MoveBlock(d, 2, -2);
                    State--;
                }

            }
            else
            {

                MoveBlock(a, -1, +1);
                MoveBlock(c, +1, -1);
                MoveBlock(d, +2, -2);

                if (Collides(blocks))
                {

                    MoveBlock(a, 1, -1);
                    MoveBlock(c, -1, 1);
                    MoveBlock(d, -2, 2);
                    State--;
                }
            }

            foreach(Block b in Segments)
            {
                blocks[b.Y,b.X] = b;
            }

        }

    }
}
