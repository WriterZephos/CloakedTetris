using System;

namespace CloakedTetris.Shapes
{
    public static class ShapeManager
    {
        private static Random Random = new Random();

        private static AbstractShape _nextShape;
        public static AbstractShape NextShape
        {
            get
            {
                var shape = _nextShape;
                _nextShape = GetRandomShape();
                return shape;

            }
        }

        static ShapeManager()
        {
            _nextShape = GetRandomShape();
        }

        private static AbstractShape GetRandomShape()
        {
            switch (Random.Next(7))
            {
                case 0:
                    return new ShapeT();
                case 1:
                    return new ShapeZ();
                case 2:
                    return new ShapeL();
                case 3:
                    return new ShapeBar();
                case 4:
                    return new ShapeSquare();
                case 5:
                    return new ShapeReverseZ();
                case 6:
                    return new ShapeReverseL();
                default:
                    return new ShapeT();
            }
        }
    }
}
