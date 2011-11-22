﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MotionChallenge
{
    class Wall
    {
        private int[] textureId;
        private int position = 0;
        private int addPerUpdate = 5;
        private int wallCount;

        public static double wallWidth = 180;
        public static double wallHeight = 240;
        public static double wallDepth = 10;
        double initialWallY = -200;

        public Wall(int playerCount)
        {
            string[] wallsPath = Directory.GetFiles(@"..\..\..\..\Walls\" + playerCount + @"j\", "*.png");
            wallCount = wallsPath.Length;
            
            textureId = new int[wallCount];
            
            // Texture loading
            for (int i = 0; i < wallCount; i++)
            {
                textureId[i] = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureId[i]);
                Bitmap bitmap = new Bitmap(wallsPath[i]);
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
                bitmap.UnlockBits(bitmapData);
            }
        }

        public bool atEndOfLine()
        {
            return (position >= 1000); 
        }

        public int getPosition()
        {
            return position;
        }

        public void update(int elapsed)
        {
            if (!atEndOfLine())
            {
                // update wall position
                position += addPerUpdate;
            }
            else
            {
                position = 0;
            }
        }

        public void draw()
        {
            double wallY = initialWallY * getPosition() / 1000;

            // Mur qui avance
            GL.BindTexture(TextureTarget.Texture2D, textureId[0]);
            GL.Begin(BeginMode.Quads);
            // Face arriere
            GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY + wallDepth, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY + wallDepth, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY + wallDepth, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY + wallDepth, 0);

            // Face laterale gauche
            GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(-wallWidth, wallY + wallDepth, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(-wallWidth, wallY + wallDepth, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY, 0);

            // Face laterale droite
            GL.TexCoord2(0, 1); GL.Vertex3(wallWidth, wallY, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY + wallDepth, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY + wallDepth, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(wallWidth, wallY, 0);

            // Face interieure 1
            GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY + wallDepth * 1 / 4, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY + wallDepth * 1 / 4, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY + wallDepth * 1 / 4, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY + wallDepth * 1 / 4, 0);

            // Face interieure 2
            GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY + wallDepth / 2, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY + wallDepth / 2, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY + wallDepth / 2, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY + wallDepth / 2, 0);

            // Face interieure 3
            GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY + wallDepth * 3 / 4, wallHeight);
            GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY + wallDepth * 3 / 4, wallHeight);
            GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY + wallDepth * 3 / 4, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY + wallDepth * 3 / 4, 0);

            // Face frontale
            GL.Color3(Color.Green); GL.TexCoord2(0, 1); GL.Vertex3(-wallWidth, wallY, wallHeight);
            GL.Color3(Color.Red); GL.TexCoord2(1, 1); GL.Vertex3(wallWidth, wallY, wallHeight);
            GL.Color3(Color.Orange); GL.TexCoord2(1, 0); GL.Vertex3(wallWidth, wallY, 0);
            GL.Color3(Color.Blue); GL.TexCoord2(0, 0); GL.Vertex3(-wallWidth, wallY, 0);

            GL.Color3(Color.White);
            GL.End();
        }
    }
}
