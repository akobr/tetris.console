using System;
using System.Drawing;

namespace Tetris.Console
{
    public class GameLogic
    {
        private readonly BrickManager brickManager;

        private GameState gameState;
        private Brick? brick;
        private int brickStateIndex;
        private short brickState;
        private Point brickPosition;
        private Brick nextBrick;

        public GameLogic()
        {
            brickManager = new BrickManager();
            gameState = new GameState();
            nextBrick = brickManager.GetRandomBrick();
        }

        public void ProcessGame()
        {
            if (brick == null)
            {
                SpawnNewBrick();
                return;
            }

            TryDrop();

            if (IsBrickSettled())
            {
                gameState.Fill(brickState, brickPosition, brick.Colour);
                brick = null;
            }
        }

        private bool IsBrickSettled()
        {
            short affectedView = gameState.GetBrickSizeView(new Point(brickPosition.X + 1, brickPosition.Y));
            return (brickState & affectedView) != 0;
        }

        private void TryRotate()
        {
            if (brick == null)
            {
                return;
            }

            short affectedView = gameState.GetBrickSizeView(brickPosition);
            int newBrickStateIndex = GetNextStateIndex(brick, brickStateIndex);
            short newBrickState = brick.States[newBrickStateIndex];

            if ((newBrickState & affectedView) == 0)
            {
                brickState = newBrickState;
            }
        }

        private void TryMoveRight()
        {
            TryMoveTo(new Point(brickPosition.X, brickPosition.Y + 1));
        }

        private void TryMoveLeft()
        {
            TryMoveTo(new Point(brickPosition.X, brickPosition.Y - 1));
        }

        private void TryDrop()
        {
            TryMoveTo(new Point(brickPosition.X + 1, brickPosition.Y));
        }

        private void TryMoveTo(Point newPosition)
        {
            if (!newPosition.IsValidPosition())
            {
                return;
            }

            short affectedView = gameState.GetBrickSizeView(newPosition);

            if ((brickState & affectedView) == 0)
            {
                brickPosition = newPosition;
            }
        }

        private void SpawnNewBrick()
        {
            brick = nextBrick;
            brickStateIndex = 0;
            brickState = brick.States[brickStateIndex];
            nextBrick = brickManager.GetRandomBrick();
        }

        private static int GetNextStateIndex(Brick brick, int brickStateIndex)
        {
            int newBrickStateIndex = brickStateIndex++;
            return newBrickStateIndex < brick.States.Count
                ? newBrickStateIndex
                : 0;
        }
    }
}
