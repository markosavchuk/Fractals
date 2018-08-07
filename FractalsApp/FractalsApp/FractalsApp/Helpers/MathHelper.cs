using System;
using System.Collections.Generic;
using SkiaSharp;

namespace FractalsApp.Helpers {
    public static class MathHelper {
        public static List<SKPoint> GetPointsCenteredEquilateralTrianlge(double width, double height)
        {
            // radius
            var r = width / 2;

            // centered point
            var x0 = width / 2;
            var y0 = height / 2;

            var ao = r / 2 * Math.Sqrt( 3 );

            var xb = x0;
            var yb = y0 - r;

            var xa = x0 - ao;
            var ya = y0 + r / 2;

            var xc = x0 + ao;
            var yc = y0 + r / 2;

            return new List<SKPoint>()
            {
                {new SKPoint( ( float )xb, ( float )yb )},
                {new SKPoint( ( float )xc, ( float )yc )},
                {new SKPoint( ( float )xa, ( float )ya )}
            };
        }
    }
}
