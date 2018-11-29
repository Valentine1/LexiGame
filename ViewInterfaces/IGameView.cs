using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public delegate void ThrowBegin(int xPos);
    public delegate void ThrowEnd();
    public interface IGameView : IView
    {
        IImageStack ImageStack
        {
            get;
        }
        void DrawBitmap(Bitmap b, int x, int y);
        void DrawPadAndBall();
        void ClearField();
        void AnimateBall(ThrowResultDT resultDT, int yPos);
        void SetLevels(int all, int current);
        void SetScores(int scores);
        void SetThemeName(string name);
        event ThrowBegin OnThrowBegin;
        event ThrowEnd OnThrowEnd;
    }
}
