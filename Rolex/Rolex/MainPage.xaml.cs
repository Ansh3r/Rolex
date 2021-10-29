using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;

namespace Rolex
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 60f), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });
        }

        private SKPaint GetPaintColor(SKPaintStyle style, string hexColor, float strokeWidth = 0, SKStrokeCap cap = SKStrokeCap.Round, bool IsAntialias = true)
        {
            return new SKPaint
            {
                Style = style,
                StrokeWidth = strokeWidth,
                Color = string.IsNullOrWhiteSpace(hexColor) ? SKColors.White : SKColor.Parse(hexColor),
                StrokeCap = cap,
                IsAntialias = IsAntialias
            };
        }

        private void canvas_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint strokePaint = GetPaintColor(SKPaintStyle.Stroke, null, 10, SKStrokeCap.Square);
            SKPaint hrPaint = GetPaintColor(SKPaintStyle.Stroke, "#262626", 4, SKStrokeCap.Square);
            SKPaint minPaint = GetPaintColor(SKPaintStyle.Stroke, "#DE0469", 2, SKStrokeCap.Square);
            SKPaint whiteStrokePaint = GetPaintColor(SKPaintStyle.Stroke, "#FFFFFF", 2, SKStrokeCap.Square);
            SKPaint bgfill = GetPaintColor(SKPaintStyle.Fill, "#000F23");
            SKPaint whiteFillPaint = GetPaintColor(SKPaintStyle.Fill, "#C99868");

            canvas.Clear();
            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(info.Width / 200f);

            canvas.Save();
            canvas.RotateDegrees(240);
            canvas.DrawCircle(0, 0, 100, bgfill);
            canvas.Restore();

            DateTime dateTime = DateTime.Now;

            //Draw hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            canvas.DrawLine(0, 5, 0, -60, hrPaint);
            canvas.Restore();

            //Draw minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            canvas.DrawLine(0, 10, 0, -90, minPaint);
            canvas.Restore();

            for (int marks = 0; marks < 360; marks += 6)
            {
                canvas.DrawCircle(0, -90, marks % 30 == 0 ? 2 : 1, whiteFillPaint);
                canvas.RotateDegrees(6);
            }

            //Draw second hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Second + dateTime.Millisecond / 1000f);
            canvas.DrawLine(0, 10, 0, -80, whiteStrokePaint);
            canvas.Restore();

            //canvas.DrawCircle(0, 0, 5, dotPaint);
        }
    }
}
