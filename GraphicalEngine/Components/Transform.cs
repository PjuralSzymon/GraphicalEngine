using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.Components
{
    public class Transform
    {
        public Vector3 position = new Vector3(0,0,0);
        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 size = new Vector3(50,50,50);
        public Vector3 pivot = new Vector3(0.5f,0.5f,0.5f);


        public Point translate3Dto2D(Vector4 v, int w, int h, Transform cameraTransform=null)
        {
            v = Vector4.Transform(v, Rotate(RotateYMatrix(rotation.Y)));
            v = Vector4.Transform(v, Rotate(RotateXMatrix(rotation.X)));
            v = Vector4.Transform(v, Rotate(RotateZMatrix(rotation.Z)));
            v = Vector4.Transform(v, Rotate(MoveMatrix(position.X, position.Y, position.Z)));
            if(cameraTransform!=null)
            {
                v = Vector4.Transform(v, Rotate(MoveMatrix(-cameraTransform.position.X, -cameraTransform.position.Y, -cameraTransform.position.Z)));
                v = Vector4.Transform(v, Rotate(RotateYMatrix(-cameraTransform.rotation.Y)));
                v = Vector4.Transform(v, Rotate(RotateXMatrix(-cameraTransform.rotation.X)));
                v = Vector4.Transform(v, Rotate(RotateZMatrix(-cameraTransform.rotation.Z)));
            }
            v = Vector4.Transform(v, Rotate(ProjectionMatrix(w, h)));

                Vector4 Vn = new Vector4(v.X / v.W, v.Y / v.W, v.Z / v.W, 1);
                return new Point((int)(((float)w / 2) * (1 + Vn.X)), (int)(((float)h / 2) * (1 - Vn.Y)));

        }

        public float getZValue(Vector4 v, int w, int h, Transform cameraTransform = null)
        {
            return 0;
        }
        private Matrix4x4 ProjectionMatrix(int w, int h)
        {
            return new Matrix4x4(
                ((float)h / (float)w), 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 0, 1,
                0, 0, -1, 0);
        }
        private Matrix4x4 MoveMatrix(float x, float y, float z)
        {
            return new Matrix4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);
        }
        private Matrix4x4 RotateYMatrix(float degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0,
                0, 1, 0, 0,
                (float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0,
                0, 0, 0, 1);
        }

        private Matrix4x4 RotateXMatrix(float degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                1, 0, 0, 0,
                0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0,
                0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0,
                0, 0, 0, 1);
        }

        private Matrix4x4 RotateZMatrix(float degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Matrix4x4(
                (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0,
                (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0,
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

    }
}
