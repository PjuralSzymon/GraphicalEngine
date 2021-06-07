using System;
using GraphicalEngine.GameObject;
using GraphicalEngine.Engine;
using System.Numerics;

namespace GraphicalEngine.GameObject
{
    public class SkeletonCube : Object
    {
        int a = 50;
        int b = 50;
        int c = 50;


        public int rotX = 0;
        public int rotY = 0;
        public int rotZ = 0;
        public int d = 200;
        Vector4[,] points = new Vector4[4, 2];
        Line[] lines = new Line[12];
        public SkeletonCube()
        {
            points[0, 0] = new Vector4(-a, +b, -c, 1);
            points[1, 0] = new Vector4(-a, +b, +c, 1);
            points[2, 0] = new Vector4(-a, -b, +c, 1);
            points[3, 0] = new Vector4(-a, -b, -c, 1);

            points[0, 1] = new Vector4(+a, +b, -c, 1);
            points[1, 1] = new Vector4(+a, +b, +c, 1);
            points[2, 1] = new Vector4(+a, -b, +c, 1);
            points[3, 1] = new Vector4(+a, -b, -c, 1);

            lines[0] = new Line();
            lines[1] = new Line();
            lines[2] = new Line();
            lines[3] = new Line();
            lines[4] = new Line();
            lines[5] = new Line();
            lines[6] = new Line();
            lines[7] = new Line();
            lines[8] = new Line();
            lines[9] = new Line();
            lines[10] = new Line();
            lines[11] = new Line();
        }

        private Point translate3Dto2D(Vector4 v, int w, int h)
        {
            v = Vector4.Transform(v, Rotate(RotateYMatrix(rotY)));
            v = Vector4.Transform(v, Rotate(RotateXMatrix(rotX)));
            v = Vector4.Transform(v, Rotate(RotateZMatrix(rotZ)));
            v = Vector4.Transform(v, Rotate(MoveMatrix(0,0,d)));
            v = Vector4.Transform(v, Rotate(ProjectionMatrix(w, h)));
            Vector4 Vn = new Vector4(v.X / v.W, v.Y / v.W, v.Z / v.W, 1);
            return new Point((int)(((float)w / 2) * (1 + Vn.X)), (int)(((float)h / 2) * (1 - Vn.Y)));
        }

        private Matrix4x4 ProjectionMatrix(int w, int h)
        {
            return new Matrix4x4(
                ((float)h/(float)w),0, 0,0,
                0                  ,1, 0,0,
                0                  ,0, 0,1,
                0                  ,0,-1,0);
        }
        private Matrix4x4 MoveMatrix(int x,int y,int z)
        {
            return new Matrix4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);
        }
        private Matrix4x4 RotateYMatrix(int degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0,
                0,                      1, 0,                       0,
                (float)Math.Sin(angle), 0, (float)Math.Cos(angle),  0,
                0,                      0, 0,                       1);
        }

        private Matrix4x4 RotateXMatrix(int degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                1, 0, 0, 0,
                0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0,
                0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0,
                0, 0, 0, 1);
        }

        private Matrix4x4 RotateZMatrix(int degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0,
                (float)Math.Sin(angle),  (float)Math.Cos(angle), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        private Matrix4x4 Rotate(Matrix4x4 M)
        {
            return new Matrix4x4(
                M.M11, M.M21, M.M31, M.M41,
                M.M12, M.M22, M.M32, M.M42,
                M.M13, M.M23, M.M33, M.M43,
                M.M14, M.M24, M.M34, M.M44);
        }


        private void updateLines(int width, int height)
        {
            lines[0].setStartPos(translate3Dto2D(points[0, 0],width,height));
            lines[0].setEndingPos(translate3Dto2D(points[0, 1], width, height));

            lines[1].setStartPos(translate3Dto2D(points[1, 0], width, height));
            lines[1].setEndingPos(translate3Dto2D(points[1, 1], width, height));

            lines[2].setStartPos(translate3Dto2D(points[2, 0], width, height));
            lines[2].setEndingPos(translate3Dto2D(points[2, 1], width, height));

            lines[3].setStartPos(translate3Dto2D(points[3, 0], width, height));
            lines[3].setEndingPos(translate3Dto2D(points[3, 1], width, height));

            lines[4].setStartPos(translate3Dto2D(points[0, 0], width, height));
            lines[4].setEndingPos(translate3Dto2D(points[3, 0], width, height));

            lines[5].setStartPos(translate3Dto2D(points[0, 1], width, height));
            lines[5].setEndingPos(translate3Dto2D(points[3, 1], width, height));

            lines[6].setStartPos(translate3Dto2D(points[1, 0], width, height));
            lines[6].setEndingPos(translate3Dto2D(points[2, 0], width, height));

            lines[7].setStartPos(translate3Dto2D(points[1, 1], width, height));
            lines[7].setEndingPos(translate3Dto2D(points[2, 1], width, height));

            lines[8].setStartPos(translate3Dto2D(points[0, 0], width, height));
            lines[8].setEndingPos(translate3Dto2D(points[1, 0], width, height));

            lines[9].setStartPos(translate3Dto2D(points[0, 1], width, height));
            lines[9].setEndingPos(translate3Dto2D(points[1, 1], width, height));

            lines[10].setStartPos(translate3Dto2D(points[3, 0], width, height));
            lines[10].setEndingPos(translate3Dto2D(points[2, 0], width, height));

            lines[11].setStartPos(translate3Dto2D(points[3, 1], width, height));
            lines[11].setEndingPos(translate3Dto2D(points[2, 1], width, height));
        }

        public override void Draw(IntPtr BitMap, int width, int height)
        {
            updateLines(width, height);
            lines[0].Draw(BitMap, width, height);
            lines[1].Draw(BitMap, width, height);
            lines[2].Draw(BitMap, width, height);
            lines[3].Draw(BitMap, width, height);
            lines[4].Draw(BitMap, width, height);
            lines[5].Draw(BitMap, width, height);
            lines[6].Draw(BitMap, width, height);
            lines[7].Draw(BitMap, width, height);
            lines[8].Draw(BitMap, width, height);
            lines[9].Draw(BitMap, width, height);
            lines[10].Draw(BitMap, width, height);
            lines[11].Draw(BitMap, width, height);
        }
    }
}
