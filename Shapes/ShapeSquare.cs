namespace CloakedTetris
{
    public class ShapeSquare : AbstractShape
    {

        public ShapeSquare() : base("S", 2, new Block[] { new Block(4, 0, "purple"), new Block(5, 0, "purple"), new Block(4, 1, "purple"), new Block(5, 1, "purple") })
        {
        }

        public override void Rotate(Block[,] blocks)
        {
  
        }

    }
}
