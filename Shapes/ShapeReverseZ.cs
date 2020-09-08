using CloakedTetris.State;

namespace CloakedTetris.Shapes
{
    public class ShapeReverseZ : AbstractShape
    {

        public ShapeReverseZ() : base("RZ", 2, new Block[] { new Block(4, 1, "lightblue"), new Block(5, 1, "lightblue"), new Block(5, 0, "lightblue"), new Block(6, 0, "lightblue") })
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
                MoveBlock(d, -2, -1);
                MoveBlock(a, 0, -1);

                if (Collides(blocks))
                {
                    MoveBlock(d, 2, 1);
                    MoveBlock(a, 0, 1);
                    State--;
                }

            }
            else
            {
                MoveBlock(d, 2, 1);
                MoveBlock(a, 0, 1);

                if (Collides(blocks))
                {
                    MoveBlock(d, -2, -1);
                    MoveBlock(a, 0, -1);
                    State--;
                }
            }

            foreach (Block b in Segments)
            {
                blocks[b.Y, b.X] = b;
            }

        }

    }
}
