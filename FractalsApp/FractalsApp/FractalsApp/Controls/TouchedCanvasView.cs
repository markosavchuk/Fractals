using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace FractalsApp.Controls {
    public class TouchedCanvasView : SKCanvasView {
        
        private SKPoint _firstTouchPoint;
        //private SKPoint _lastTouchPoint;
        private SKPoint _moveToPoint;

        public TouchedCanvasView(){
            EnableTouchEvents = true;
            Touch += HandleTouch;

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += HandlePinch;
            GestureRecognizers.Add( pinchGesture );
        }

        private void HandleTouch ( object sender, SKTouchEventArgs e ) {
            e.Handled = true;
            switch ( e.ActionType ) {
                case SKTouchAction.Moved:
                    _moveToPoint = new SKPoint( e.Location.X - _firstTouchPoint.X, 
                                               e.Location.Y - _firstTouchPoint.Y );
                    InvalidateSurface();

                    break;

                case SKTouchAction.Pressed:
                    if ( _firstTouchPoint.Equals( SKPoint.Empty )){
                        _firstTouchPoint = e.Location;
                    }
                    else {
                        _firstTouchPoint = new SKPoint( e.Location.X - _moveToPoint.X,
                                                       e.Location.Y - _moveToPoint.Y );
                    }
                    break;
                case SKTouchAction.Released:
                case SKTouchAction.Cancelled:
                    break;
            }
        }

        private void HandlePinch ( object sender, PinchGestureUpdatedEventArgs e ) {
            switch ( e.Status ) {
                case GestureStatus.Running:
                    InvalidateSurface();
                    break;
                case GestureStatus.Started:
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    break;
            }
        }

        protected override void OnPaintSurface ( SKPaintSurfaceEventArgs e ) {
            var canvas = e.Surface.Canvas;
            canvas.Translate( _moveToPoint );

            Console.WriteLine(_moveToPoint.ToString());

            base.OnPaintSurface( e );
        }

    }
}
