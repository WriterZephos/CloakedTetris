using System;
using System.Collections.Generic;

using Clkd.Main;
using Clkd.Managers;

using CloakedTetris.State;

namespace CloakedTetris.Shapes
{
    public abstract class AbstractShape : AbstractComponent
    {
        public string Type { get; set; }
        public int State { get; set; }
        public int Color { get; set; }
        public bool Active { get; set; }
        public Block[] Segments { get; set; }
        public bool Collided { get; set; } = false;

        public AbstractShape(string t, int col, Block[] seg)
        {
            Type = t;
            Color = col;
            Segments = seg;
        }

        public abstract void Rotate(Block[,] blocks);

        public void MoveRight(Block[,] blocks)
        {

            if (!Collided)
            {
                MoveSegments(1, 0, blocks);
            }

        }

        public void MoveLeft(Block[,] blocks)
        {

            if (!Collided)
            {
                MoveSegments(-1, 0, blocks);
            }

        }

        public bool Drop(Block[,] blocks)
        {
            if (!Collided)
            {
                Collided = MoveSegments(0, 1, blocks);
                if (Collided)
                {
                    foreach (Block b in Segments)
                    {
                        b.Fixed = true;
                    }
                }
            }

            Cloaked.GetContext("game").GetComponent<TriggerManager>().Publish("drop");

            return Collided;
        }

        protected bool MoveSegments(int x, int y, Block[,] blocks)
        {
            bool temp = false;

            foreach (Block b in Segments)
            {
                blocks[b.Y, b.X] = null;
                b.X += x;
                b.Y += y;
            }

            if (Collides(blocks))
            {

                temp = true;

                foreach (Block b in Segments)
                {
                    b.X -= x;
                    b.Y -= y;
                }
            }

            foreach (Block b in Segments)
            {
                blocks[b.Y, b.X] = b;
            }
            return temp;
        }

        /**
         * Method takes the 2D array of blocks, finds the shapes bock segments and set them to null
         * in order to not paint them during shape flipping
         * @param blocks
         */
        public void NullBlocks(Block[,] blocks)
        {
            foreach (Block b in Segments)
            {
                blocks[b.Y, b.X] = null;
            }
        }

        public void MoveBlock(Block block, int x, int y)
        {
            block.X = block.X + x;
            block.Y = block.Y + y;
        }

        public bool Collides(Block[,] blocks)
        {

            bool result = false;

            foreach (Block b in Segments)
            {
                if (b.Y > 19 || b.Y < 0 || b.X > 9 || b.X < 0 || blocks[b.Y, b.X] != null)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        /**
         * Method to toggle between a given number of states
         * @param numberOfStates
         * @return
         */
        public int ToggleState(int numberOfStates)
        {
            State++;
            if (State == numberOfStates)
            {
                State = 0;
            }
            return State;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            else if (this == obj)
                return true;
            else
                return Type == ((AbstractShape)obj).Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 753734788;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + Active.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Block[]>.Default.GetHashCode(Segments);
            hashCode = hashCode * -1521134295 + Collided.GetHashCode();
            return hashCode;
        }
    }
}
