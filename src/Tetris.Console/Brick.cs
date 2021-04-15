using System.Collections.Generic;

namespace Tetris.Console
{
    public class Brick
    {
        public Brick(params short[] states)
        {
            States = states;
        }

        public IReadOnlyList<short> States { get; }
    }
}
