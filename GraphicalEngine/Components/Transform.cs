﻿using GraphicalEngine.Components.Meshes;
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
        public Transform Parent = null;
        public Vector3 position = new Vector3(0,0,0);
        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 size = new Vector3(100,100,100);
        public Vector3 pivot = new Vector3(0.5f,0.5f,0.5f);


        public bool ObjectIsIn(Transform intruder)
        {
            if (position.X + pivot.X * size.X >= intruder.position.X)
            if (position.X - pivot.X * size.X <= intruder.position.X)
            if (position.Y + pivot.Y * size.Y >= intruder.position.Y)
            if (position.Y - pivot.Y * size.Y <= intruder.position.Y)
            if (position.Z + pivot.Z * size.Z >= intruder.position.Z)
            if (position.Z - pivot.Z * size.Z <= intruder.position.Z)
            {
                return true;
            }
            return false;
        }
        public Vector4 translate(Vector4 v)
        {
            v = Vector4.Transform(v, Rotate(RotateYMatrix(rotation.Y)));
            v = Vector4.Transform(v, Rotate(RotateXMatrix(rotation.X)));
            v = Vector4.Transform(v, Rotate(RotateZMatrix(rotation.Z)));
            v = Vector4.Transform(v, Rotate(MoveMatrix(position.X, position.Y, position.Z)));
            if (Parent != null) v = Parent.translate(v);
            return v;
        }
        public Vector4 project(Vector4 v, int w, int h, Transform cameraTransform = null)
        {
            //v must be already moved and rotated
            if (cameraTransform != null)
            {
                v = Vector4.Transform(v, Rotate(MoveMatrix(-cameraTransform.position.X, -cameraTransform.position.Y, -cameraTransform.position.Z)));
                v = Vector4.Transform(v, Rotate(RotateYMatrix(-cameraTransform.rotation.Y)));
                v = Vector4.Transform(v, Rotate(RotateXMatrix(-cameraTransform.rotation.X)));
                v = Vector4.Transform(v, Rotate(RotateZMatrix(-cameraTransform.rotation.Z)));
            }
            v = Vector4.Transform(v, Rotate(ProjectionMatrix(w, h)));
            if (v.W != 0 && v.Z>0)
            {
                Vector4 Vn = new Vector4(v.X / v.W, v.Y / v.W, v.Z / v.W, 1);
                return new Vector4((((float)w / 2) * (1 + Vn.X)), (((float)h / 2) * (1 - Vn.Y)), Vn.Z, 1);
            }
            else
                return new Vector4(v.X, v.Y, v.Z,1);

        }

        public Point translate3Dto2D(Vector4 v, int w, int h, Transform cameraTransform=null)
        {
            //v must be already moved and rotated
            if (cameraTransform != null)
            {
                v = Vector4.Transform(v, Rotate(MoveMatrix(-cameraTransform.position.X, -cameraTransform.position.Y, -cameraTransform.position.Z)));
                v = Vector4.Transform(v, Rotate(RotateYMatrix(-cameraTransform.rotation.Y)));
                v = Vector4.Transform(v, Rotate(RotateXMatrix(-cameraTransform.rotation.X)));
                v = Vector4.Transform(v, Rotate(RotateZMatrix(-cameraTransform.rotation.Z)));
            }
            v = Vector4.Transform(v, Rotate(ProjectionMatrix(w, h)));
            if (v.W!=0)
            {
                Vector4 Vn = new Vector4(v.X / v.W, v.Y / v.W, v.Z / v.W, 1);
                return new Point((int)(((float)w / 2) * (1 + Vn.X)), (int)(((float)h / 2) * (1 - Vn.Y)),Vn.Z);
            }
            else
                return new Point((int)v.X, (int)v.Y, v.Z);

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
                0, (float)Math.Sin(angle),  (float)Math.Cos(angle), 0,
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
