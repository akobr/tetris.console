using System.Collections.Generic;
using System.Drawing;

namespace Tetris.Console
{
    public class Brick
    {
        public Brick(Color colour, params short[] states)
        {
            States = states;
        }

        public Color Colour { get; }

        public IReadOnlyList<short> States { get; }
    }
}
