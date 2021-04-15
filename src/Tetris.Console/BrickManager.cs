using System;
using System.Collections.Generic;

namespace Tetris.Console
{
    public class BrickManager
    {
        private static readonly Brick[] bricks =
        {
            new Brick( // O
                0b_0000_0110_0110_0000),
            new Brick( // I
                0b_0000_1111_0000_0000,
                0b_0010_0010_0010_0010),
        };

        private readonly Random random = new Random();

        public Brick GetRandomBrick()
        {
            return bricks[random.Next(0, bricks.Length)];
        }

        public IReadOnlyList<Brick> AllBricks => bricks;
    }
}
