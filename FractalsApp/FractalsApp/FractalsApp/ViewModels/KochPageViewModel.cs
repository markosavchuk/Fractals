using FractalsApp.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FractalsApp.ViewModels {
    public class KochPageViewModel : ViewModelBase {

        public ICommand PaintCommand { get; private set; }

        public KochPageViewModel ( INavigationService navigationService )
            : base( navigationService ) {
            Title = "Koch snowflake";

            PaintCommand = new DelegateCommand<SKPaintSurfaceEventArgs>( Draw );
        }

        void Draw ( SKPaintSurfaceEventArgs canvasArgs ) {

            List<SKPoint> points = new List<SKPoint>();

            points.AddRange( MathHelper.GetPointsCenteredEquilateralTrianlge(
                canvasArgs.Info.Width, canvasArgs.Info.Height ) );

            AddPointsForKochSnowflake( ref points, 6 );

            SKPath path = new SKPath();

            path.MoveTo( points[0] );

            for ( int i = 1; i < points.Count; i++ ) {
                path.LineTo( points[i] );
            }

            path.Close();

            SKPaint strokePaint = new SKPaint {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Magenta,
                StrokeWidth = 1
            };

            var canvas = canvasArgs.Surface.Canvas;
            canvas.Clear();
            canvas.DrawPath( path, strokePaint );
        }

        private void AddPointsForKochSnowflake ( ref List<SKPoint> points, int iteration ) 
        {
            for ( int i = 0; i < iteration; i++ ) 
            {
                var newList = new List<SKPoint>();

                for ( int j = 0; j < points.Count; j++ ) {
                    var x1 = points[j].X;
                    var y1 = points[j].Y;

                    float x2;
                    float y2;

                    if ( j < points.Count - 1 ) {
                        x2 = points[j + 1].X;
                        y2 = points[j + 1].Y;
                    }
                    else {
                        x2 = points[0].X;
                        y2 = points[0].Y;
                    }


                    double x3 = ( 2 * x1 + x2 ) / 3;
                    double y3 = ( 2 * y1 + y2 ) / 3;
                    double x4 = ( x1 + 2 * x2 ) / 3;
                    double y4 = ( y1 + 2 * y2 ) / 3;

                    double xm = ( x1 + x2 ) / 2;
                    double ym = ( y1 + y2 ) / 2;
                    double d = 1 / Math.Sqrt( 3 );
                    double x5 = xm + d * ( ym - y1 );
                    double y5 = ym - d * ( xm - x1 );

                    newList.Add( points[j] );

                    newList.Add( new SKPoint( ( float )x3, ( float )y3 ) );

                    newList.Add( new SKPoint( ( float )x5, ( float )y5 ) );

                    newList.Add( new SKPoint( ( float )x4, ( float )y4 ) );

                }

                points = newList;
            }
        }

    }
}
