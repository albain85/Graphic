using System;
using System.Collections.Generic;
using System.Windows.Forms;

//2
using System.Drawing;

namespace OpenGL
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;

        GLUquadric obj;

        void MakeShadowMatrix(float[,] points)
        {
            float[] planeCoeff = new float[4];
            float dot;

            // Find the plane equation coefficients
            // Find the first three coefficients the same way we
            // find a normal.
            calcNormal(points, planeCoeff);

            // Find the last coefficient by back substitutions
            planeCoeff[3] = -(
                (planeCoeff[0] * points[2, 0]) + (planeCoeff[1] * points[2, 1]) +
                (planeCoeff[2] * points[2, 2]));


            // Dot product of plane and light position
            dot = planeCoeff[0] * pos[0] +
                    planeCoeff[1] * pos[1] +
                    planeCoeff[2] * pos[2] +
                    planeCoeff[3];

            // Now do the projection
            // First column
            cubeXform[0] = dot - pos[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - pos[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - pos[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - pos[0] * planeCoeff[3];

            // Second column
            cubeXform[1] = 0.0f - pos[1] * planeCoeff[0];
            cubeXform[5] = dot - pos[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - pos[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - pos[1] * planeCoeff[3];

            // Third Column
            cubeXform[2] = 0.0f - pos[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - pos[2] * planeCoeff[1];
            cubeXform[10] = dot - pos[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - pos[2] * planeCoeff[3];

            // Fourth Column
            cubeXform[3] = 0.0f - pos[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - pos[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - pos[3] * planeCoeff[2];
            cubeXform[15] = dot - pos[3] * planeCoeff[3];
        }


        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();
            obj = GLU.gluNewQuadric(); //!!!
            PrepareLists();
        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj); //!!!
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }
        uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }


        void Missle()
        {
            GL.glPushMatrix();
            GL.glTranslated(1.3, 0.75, 0.75);
            GL.glRotated(-90, 0, 1, 0);
            GL.glColor3f(0.5f, 0.0f, 0.0f);
            GLU.gluCylinder(obj, 0.08, 0.08, 0.5, 20, 20);
            GL.glPopMatrix();
            GL.glPushMatrix();
            GL.glColor3f(0.5f, 0.0f, 0.0f);
            GL.glTranslated(1.3, 0.75, 0.75);
            GL.glRotated(-90, 0, 1, 0);
            GLU.gluDisk(obj, 0, 0.08, 40, 20);
            GL.glPopMatrix();

            //missle tail
            GL.glBegin(GL.GL_TRIANGLES);
            GL.glColor3f(0.0f, 0.5f, 0.0f);
            GL.glVertex3f(1.0f, 0.75f, 0.75f);
            GL.glVertex3f(1.3f, 0.85f, 0.55f);
            GL.glVertex3f(1.3f, 0.65f, 0.95f);


            GL.glVertex3f(1.0f, 0.75f, 0.75f);
            GL.glVertex3f(1.3f, 0.65f, 0.55f);
            GL.glVertex3f(1.3f, 0.85f, 0.95f);
            GL.glEnd();

            GL.glPushMatrix();
            GL.glColor3f(0.0f, 0.0f, 0.0f);
            GL.glTranslated(1.35, 0.75, 0.75);
            GL.glRotated(-90, 0, 1, 0);
            GLU.gluCylinder(obj, 0.06, 0.00, 0.5, 20, 20);
            GL.glPopMatrix();

            //missle head
            GL.glPushMatrix();
            GL.glTranslated(0.8, 0.75, 0.75);
            GL.glRotated(-90, 0, 1, 0);
            GL.glColor3f(0.5f, 0.0f, 0.0f);
            GLU.gluCylinder(obj, 0.08, 0.05, 0.2, 20, 20);
            GL.glPopMatrix();
            GL.glPushMatrix();
            GL.glTranslated(0.9, 0.75, 0.75);
            GL.glColor3f(0.5f, 0.5f, 0.0f);
            GLU.gluSphere(obj, 0.09, 20, 20);
            GL.glPopMatrix();
            GL.glPushMatrix();
            GL.glTranslated(0.6, 0.75, 0.75);
            GL.glColor3f(0.5f, 0.5f, 0.0f);
            GLU.gluSphere(obj, 0.05, 20, 20);
            GL.glPopMatrix();
        }

        void Canon()
        {
            GL.glPushMatrix();
            GL.glTranslated(0.0, 0.1, 0.0);
            GL.glRotated(-90, 0, 1, 0);
           // GL.glRotated(0, 1, 0, 0);
            GL.glColor3f(0.35f, 0.35f, 0.35f);
            GLU.gluCylinder(obj, 0.03, 0.01, 0.5, 20, 20);
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glTranslated(0.0, 0.1, -0.1);
            GL.glRotated(-90, 0, 1, 0);
            GL.glRotated(0, 1, 0, 0);
            GL.glColor3f(0.35f, 0.35f, 0.35f);
            GLU.gluCylinder(obj, 0.03, 0.01, 0.5, 20, 20);
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glTranslated(0.0, 0.1, 0.1);
            GL.glRotated(-90, 0, 1, 0);
            GL.glRotated(0, 1, 0, 0);
            GL.glColor3f(0.35f, 0.35f, 0.35f);
            GLU.gluCylinder(obj, 0.03, 0.01, 0.5, 20, 20);
            GL.glPopMatrix();

            //Canon Head
            GL.glPushMatrix();
            //GL.glTranslated(0, 0.5, 0.5);
            GL.glColor3f(0.4f, 0.4f, 0.4f);
            GLU.gluSphere(obj, 0.2, 20, 20);
            GL.glPopMatrix();

        }

        void DrawTopBort()
        {

            //middle top texture         
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[1]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glNormal3f(0.0f, 0.0f, 0.0f);        //check what it for
            GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
            GL.glVertex3f(2.0f, 0.5f, 0.0f);        // top right of quad
            GL.glNormal3f(0.0f, 0.0f, 0.0f);        //check what it for
            GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
            GL.glVertex3f(0.0f, 0.5f, 0.0f);        // top left of quad
            GL.glNormal3f(0.0f, 0.0f, 0.0f);        //check what it for
            GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
            GL.glVertex3f(0.0f, 0.5f, 1.0f);        // bottom left of quad
            GL.glNormal3f(0.0f, 0.0f, 0.0f);        //check what it for
            GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
            GL.glVertex3f(2.0f, 0.5f, 1.0f);        // bottom right of quad
            GL.glPopMatrix();
            GL.glEnd();

            //front top textute
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[2]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.5f, 0.0f);
            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.5f, 1.0f);
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(-1.0f, 0.6f, 0.5f);
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(-1.0f, 0.6f, 0.5f);
            GL.glPopMatrix();
            GL.glEnd();

            //back top texture
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[2]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glNormal3f(0.0f, 0.0f, 0.0f);
            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(2.0f, 0.5f, 0.0f);
            GL.glNormal3f(0.0f, 0.0f, 0.0f);
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(2.0f, 0.5f, 1.0f);
            GL.glNormal3f(0.0f, 0.0f, 0.0f);
            GL.glTexCoord2f(1.0f, 0.5f);
            GL.glVertex3f(2.5f, 0.55f, 0.5f);
            GL.glTexCoord2f(1.0f, 0.5f);
            GL.glVertex3f(2.5f, 0.55f, 0.5f);
            GL.glPopMatrix();
            GL.glEnd();
        }
        void DrawShipBodey()
        {
            {
                GL.glEnable(GL.GL_COLOR_MATERIAL);
                GL.glEnable(GL.GL_LIGHT0);
                GL.glEnable(GL.GL_LIGHTING);

                //axes
                GL.glBegin(GL.GL_LINES);
                //x RED
                GL.glColor3f(1.0f, 0.0f, 0.0f);
                GL.glVertex3f(-3.0f, 0.0f, 0.0f);
                GL.glVertex3f(3.0f, 0.0f, 0.0f);
                //y  GREEN 
                GL.glColor3f(0.0f, 1.0f, 0.0f);
                GL.glVertex3f(0.0f, -3.0f, 0.0f);
                GL.glVertex3f(0.0f, 3.0f, 0.0f);
                //z  BLUE
                GL.glColor3f(0.0f, 0.0f, 1.0f);
                GL.glVertex3f(0.0f, 0.0f, -3.0f);
                GL.glVertex3f(0.0f, 0.0f, 3.0f);

                GL.glEnd();

                //tower

                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[9]);
                GL.glBegin(GL.GL_QUADS);
                GL.glPushMatrix();
                //tower left side
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(0.0f, 0.0f);
                GL.glVertex3f(0.5f, 0.5f, 0.9f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(1.5f, 0.5f, 0.9f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(1.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.85f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(0.0f, 1.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.85f);
                GL.glPopMatrix();
                GL.glEnd();

                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[10]);
                GL.glBegin(GL.GL_QUADS);
                GL.glPushMatrix();
                //tower right side
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(0.0f, 0.0f, -1.0f);
                GL.glTexCoord2f(0.0f, 0.0f);
                GL.glVertex3f(0.5f, 0.5f, 0.1f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(0.0f, 0.0f, -1.0f);
                GL.glTexCoord2f(0.0f, 1.0f);
                GL.glVertex3f(1.5f, 0.5f, 0.1f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(0.0f, 0.0f, -1.0f);
                GL.glTexCoord2f(1.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.15f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(0.0f, 0.0f, -1.0f);
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.15f);

                //tower front side
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(-1.0f, 0.0f, 0.0f);
                GL.glTexCoord2f(0.0f, 0.0f);
                GL.glVertex3f(0.5f, 0.5f, 0.1f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(-1.0f, 0.0f, 0.0f);
                GL.glTexCoord2f(0.0f, 1.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.15f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(-1.0f, 0.0f, 0.0f);
                GL.glTexCoord2f(1.0f, 1.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.85f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(-1.0f, 0.0f, 0.0f);
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(0.5f, 0.5f, 0.9f);

                //tower rear side
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(1.5f, 0.5f, 0.1f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glTexCoord2f(1.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.15f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glTexCoord2f(0.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.85f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glTexCoord2f(0.0f, 0.0f);
                GL.glVertex3f(1.5f, 0.5f, 0.9f);

                //tower top
                GL.glNormal3f(0.0f, 1.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(0.0f, 0.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.15f);
                GL.glNormal3f(0.0f, 1.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(1.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.15f);
                GL.glNormal3f(0.0f, 1.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(0.0f, 1.0f);
                GL.glVertex3f(1.45f, 0.65f, 0.85f);
                GL.glNormal3f(0.0f, 1.0f, 1.0f);
                GL.glColor3f(0.3f, 0.3f, 0.3f);
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(0.55f, 0.65f, 0.85f);

                //Radar tower
                //right
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.65f, 0.2f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.65f, 0.2f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.8f, 0.2f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.8f, 0.2f);

                //back
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.65f, 0.2f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.65f, 0.4f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.8f, 0.4f);
                GL.glNormal3f(1.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.8f, 0.2f);

                //front
                GL.glNormal3f(-0.2f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.65f, 0.2f);
                GL.glNormal3f(-0.2f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.65f, 0.4f);
                GL.glNormal3f(-0.2f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.8f, 0.4f);
                GL.glNormal3f(-0.2f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.8f, 0.2f);

                //left
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.65f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.65f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.85f, 0.8f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glVertex3f(0.65f, 0.8f, 0.4f);

                //top
                GL.glNormal3f(0.0f, 0.0f, 0.0f);
                GL.glVertex3f(0.65f, 0.8f, 0.2f);
                GL.glVertex3f(0.85f, 0.8f, 0.2f);
                GL.glVertex3f(0.65f, 0.8f, 0.4f);
                GL.glVertex3f(0.85f, 0.8f, 0.4f);

                GL.glPopMatrix();
                GL.glEnd();

                //Radar
                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[0]);
                GL.glPushMatrix();
                GL.glEnable(GL.GL_LIGHTING);
                GL.glTranslated(0.75f, 1.0f, 0.3f);
                GL.glRotated(radar_angle, 0, 1, 0);
                GL.glNormal3f(-1.0f, -1.0f, -1.0f);
                GL.glColor3f(0.35f,0.35f, 0.35f);
                GLU.gluCylinder(obj, 0.0, 0.2, 0.1, 20, 20);
                GL.glPopMatrix();
                GL.glPushMatrix();
                GL.glTranslated(0.75f, 1.0f, 0.3f);
                GL.glRotated(radar_angle, 0, 1, 0);
                GL.glColor3f(0.0f, 0.0f, 0.0f);
                GLU.gluCylinder(obj, 0.02, 0.01, 0.1, 20, 20);
                GL.glPopMatrix();
                GL.glEnable(GL.GL_LIGHTING);

                //radar pole
                GL.glPushMatrix();
                GL.glTranslated(0.75f, 0.8f, 0.3f);
                GL.glRotated(-90, 1, 0, 0);
                GL.glColor3f(0.35f, 0.35f, 0.35f);
                GLU.gluCylinder(obj, 0.02, 0.02, 0.2, 20, 20);
                GL.glPopMatrix();
                GL.glPushMatrix();
                GL.glTranslated(0.75f, 1.0f, 0.3f);
                GL.glColor3f(0.35f, 0.35f, 0.35f);
                GLU.gluSphere(obj, 0.02, 20, 20);
                GL.glPopMatrix();


                GL.glPushMatrix();
                GL.glTranslated(0, 0.5, 0.5);
                GL.glRotatef(barrel_angle , 0 , 1 ,0);
                GL.glRotatef(canon_angle, 0, 0, 1);
                Canon();
                GL.glPopMatrix();



                GL.glPushMatrix();
                GL.glTranslated(launch, 0.0, 0.0);
                Missle();
                GL.glPopMatrix();

                //texture
                GL.glBegin(GL.GL_QUADS);
                //left texture
                GL.glPushMatrix();
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(2.0f, 0.5f, 1.0f);        // top right of quad
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(0.0f, 0.5f, 1.0f);        // top left of quad
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(0.0f, 0.0f, 0.5f);        // bottom left of quad
                GL.glColor3f(0.4f, 0.4f, 0.4f);
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        
                GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
                GL.glVertex3f(2.0f, 0.0f, 0.5f);        // bottom right of quad

                //right texture
                GL.glNormal3f(0.0f, 0.0f, -1.0f);        
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(0.0f, 0.5f, 0.0f);        // top right of quad
                GL.glNormal3f(0.0f, 0.0f, -1.0f);        
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(2.0f, 0.5f, 0.0f);        // top left of quad
                GL.glNormal3f(0.0f, 0.0f, -1.0f);        
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(2.0f, 0.0f, 0.5f);        // bottom left of quad
                GL.glNormal3f(0.0f, 0.0f, -1.0f);        
                GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
                GL.glVertex3f(0.0f, 0.0f, 0.5f);        // bottom right of quad
                GL.glPopMatrix();
                
                //nose left texture
                GL.glPushMatrix();
                GL.glNormal3f(-1.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(0.0f, 0.5f, 1.0f);        // top right of quad
                GL.glNormal3f(-1.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(-1.0f, 0.6f, 0.5f);        // top left of quad
                GL.glNormal3f(-1.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(0.0f, 0.0f, 0.5f);        // bottom left of quad
                GL.glNormal3f(-1.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
                GL.glVertex3f(0.0f, 0.0f, 0.5f);        // bottom right of quad

                //nose right texture
                GL.glNormal3f(-1.0f, 0.0f, -1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(-1.0f, 0.6f, 0.5f);
                GL.glNormal3f(-1.0f, 0.0f, -1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(0.0f, 0.5f, 0.0f);
                GL.glNormal3f(-1.0f, 0.0f, -1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(0.0f, 0.0f, 0.5f);
                GL.glNormal3f(-1.0f, 0.0f, -1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 0.0f);
                GL.glVertex3f(-1.0f, 0.6f, 0.5f);

                //back right texture
                GL.glNormal3f(0.0f, 0.5f, -1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(2.5f, 0.55f, 0.5f);         // top right of quad
                GL.glNormal3f(0.0f, 0.5f, -1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(2.0f, 0.5f, 0.0f);        // top left of quad
                GL.glNormal3f(0.0f, 0.5f, -1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(2.0f, 0.0f, 0.5f);        // bottom left of quad
                GL.glNormal3f(0.0f, 0.5f, -1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
                GL.glVertex3f(2.5f, 0.55f, 0.5f);        // bottom right of quad

                //back left texture
                GL.glNormal3f(0.0f, 0.0f, 0.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 1.0f);            // top right of texture
                GL.glVertex3f(2.5f, 0.55f, 0.5f);         // top right of quad
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 1.0f);            // top left of texture
                GL.glVertex3f(2.5f, 0.55f, 0.5f);        // top left of quad
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(0.0f, 0.0f);            // bottom left of texture
                GL.glVertex3f(2.0f, 0.0f, 0.5f);        // bottom left of quad
                GL.glNormal3f(0.0f, 0.0f, 1.0f);        //check what it for
                GL.glTexCoord2f(1.0f, 0.0f);            // bottom right of texture
                GL.glVertex3f(2.0f, 0.5f, 1.0f);        // bottom right of quad

                GL.glPopMatrix();
                GL.glEnd();

                GL.glDisable(GL.GL_LIGHTING);
                GL.glPushMatrix();
                GL.glTranslatef(0.0f, 0.2f, 0.0f);
                DrawSeaCubeMap();
                GL.glPopMatrix();
            }
        }

        void DrawSeaCubeMap()
        {

            //bottom cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[3]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glColor3f(1.0f, 1.00f, 1.00f); GL.glVertex3f(-6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glColor3f(1.0f, 1.00f, 1.00f); GL.glVertex3f(6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glColor3f(1.0f, 1.00f, 1.00f); GL.glVertex3f(6.0f, 0.0f, -6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glColor3f(1.0f, 1.00f, 1.00f); GL.glVertex3f(-6.0f, 0.0f, -6.0f);
            GL.glPopMatrix();
            GL.glEnd();

            //top cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[4]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-6.0f, 3.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(6.0f, 3.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(6.0f, 3.0f, 6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-6.0f, 3.0f, 6.0f);
            GL.glPopMatrix();
            GL.glEnd();



            //left cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[5]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-6.0f, 0.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-6.0f, 3.0f, -6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-6.0f, 3.0f, 6.0f);
            GL.glPopMatrix();
            GL.glEnd();

            //front cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[6]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-6.0f, 0.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(6.0f, 0.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(6.0f, 3.0f, -6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-6.0f, 3.0f, -6.0f);
            GL.glPopMatrix();
            GL.glEnd();

            //right cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[7]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(6.0f, 0.0f, -6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(6.0f, 3.0f, 6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(6.0f, 3.0f, -6.0f);
            GL.glPopMatrix();
            GL.glEnd();

            //back cubemap
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[8]);
            GL.glBegin(GL.GL_QUADS);
            GL.glPushMatrix();
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-6.0f, 0.0f, 6.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-6.0f, 3.0f, 6.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(6.0f, 3.0f, 6.0f);
            GL.glPopMatrix();
            GL.glEnd();
        }

        const int x = 0;
        const int y = 1;
        const int z = 2;
        void calcNormal(float[,] v, float[] outp)
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];

            // Calculate two vectors from the three points
            v1[x] = v[0, x] - v[1, x];
            v1[y] = v[0, y] - v[1, y];
            v1[z] = v[0, z] - v[1, z];

            v2[x] = v[1, x] - v[2, x];
            v2[y] = v[1, y] - v[2, y];
            v2[z] = v[1, z] - v[2, z];

            // Take the cross product of the two vectors to get
            // the normal vector which will be stored in out
            outp[x] = v1[y] * v2[z] - v1[z] * v2[y];
            outp[y] = v1[z] * v2[x] - v1[x] * v2[z];
            outp[z] = v1[x] * v2[y] - v1[y] * v2[x];

            // Normalize the vector (shorten length to one)
            ReduceToUnit(outp);
        }

        void ReduceToUnit(float[] vector)
        {
            float length;

            // Calculate the length of the vector		
            length = (float)Math.Sqrt((vector[0] * vector[0]) +
                                (vector[1] * vector[1]) +
                                (vector[2] * vector[2]));

            // Keep the program from blowing up by providing an exceptable
            // value for vectors that may calculated too close to zero.
            if (length == 0.0f)
                length = 1.0f;

            // Dividing each element by the length will result in a
            // unit normal vector.
            vector[0] /= length;
            vector[1] /= length;
            vector[2] /= length;
        }

        float[] cubeXform = new float[16];
        public float[] pos = new float[4];
        

        void DrawOldAxes()
        {
            //for this time
            //Lights positioning is here!!!
            float[] pos = new float[4];
            pos[0] = 10; pos[1] = 10; pos[2] = 10; pos[3] = 1;
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);
            GL.glDisable(GL.GL_LIGHTING);


            //INITIAL axes
            GL.glEnable(GL.GL_LINE_STIPPLE);
            GL.glLineStipple(1, 0xFF00);  //  dotted   
            GL.glBegin(GL.GL_LINES);
            //x  RED
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-3.0f, 0.0f, 0.0f);
            GL.glVertex3f(3.0f, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -3.0f, 0.0f);
            GL.glVertex3f(0.0f, 3.0f, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -3.0f);
            GL.glVertex3f(0.0f, 0.0f, 3.0f);
            GL.glEnd();
            GL.glDisable(GL.GL_LINE_STIPPLE);
        }
        void DrawAxes()
        {
            GL.glBegin(GL.GL_LINES);
            //x  RED
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-3.0f, 0.0f, 0.0f);
            GL.glVertex3f(3.0f, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -3.0f, 0.0f);
            GL.glVertex3f(0.0f, 3.0f, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -3.0f);
            GL.glVertex3f(0.0f, 0.0f, 3.0f);
            GL.glEnd();
        }
        void DrawFigures()
        {
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_LIGHTING);

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            GL.glRotatef(ROBOT_angle, 0, 0, 1);
            GL.glCallList(ROBOT_LIST);
	        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // saw 
            GL.glLineStipple(1, 0x1243);
            GL.glLineWidth(3);
            GL.glEnable(GL.GL_LINE_STIPPLE);

            //GL.glColor3f(1, 1, 0);//yellow
            //GL.glBegin(GL.GL_LINES);
            float angle;
            for (int i = 0; i <= 9; i++)
            {
                angle = alpha + i * 6.283f / 10;
                GL.glVertex3d(0.5f * r * Math.Cos(angle), 0.5f * r * Math.Sin(angle), 0.01f);
                GL.glVertex3d(1.5f * r * Math.Cos(angle + 0.6), 1.5f * r * Math.Sin(angle + 0.6), 0.01f);
            }
            GL.glEnd();
            GL.glLineWidth(1);
            GL.glDisable(GL.GL_LINE_STIPPLE);
        }

        public float[] ScrollValue = new float[10];
        public float zShift = 0.0f;
        public float yShift = 0.0f;
        public float xShift = 0.0f;
        public float zAngle = 0.0f;
        public float yAngle = 0.0f;
        public float xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];

        float[] planeCoeff = { 1, 1, 1, 1 };
        float[,] ground = new float[3, 3];//{ { 1, 1, -0.5 }, { 0, 1, -0.5 }, { 1, 0, -0.5 } };
        float[,] wall = new float[3, 3];//{ { -15, 3, 0 }, { 15, 3, 0 }, { 15, 3, 15 } };

        public void Draw()
        {

         
       

            GL.glEnable(GL.GL_TEXTURE_2D);
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            
            GL.glLoadIdentity();								

            // not trivial
            double []ModelVievMatrixBeforeSpecificTransforms=new double[16];
            double []CurrentRotationTraslation=new double[16];
                     
            GLU.gluLookAt (ScrollValue[0], ScrollValue[1], ScrollValue[2], 
	                   ScrollValue[3], ScrollValue[4], ScrollValue[5],
		               ScrollValue[6],ScrollValue[7],ScrollValue[8]);
            GL.glTranslatef(-0.5f, -1.5f, 5.0f);
            
            //DrawOldAxes();

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev (GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix
                     
            //make transformation in accordance to KeyCode
            float delta;
            if (intOptionC != 0)
            {
                delta = 5.0f * Math.Abs(intOptionC) / intOptionC; // signed 5

                switch (Math.Abs(intOptionC))
                {
                    case 1:
                        GL.glRotatef(delta, 1, 0, 0);
                        break;
                    case 2:
                        GL.glRotatef(delta, 0, 1, 0);
                        break;
                    case 3:
                        GL.glRotatef(delta, 0, 0, 1);
                        break;
                    case 4:
                        GL.glTranslatef(delta / 20, 0, 0);
                        break;
                    case 5:
                        GL.glTranslatef(0, delta / 20, 0);
                        break;
                    case 6:
                        GL.glTranslatef(0, 0, delta / 20);
                        break;
                }
            }
            //as result - the ModelView Matrix now is pure representation
            //of KeyCode transform and only it !!!

            //save current ModelView Matrix values
            //in CurrentRotationTraslation array
            //ModelView Matrix =======>>>>>>> CurrentRotationTraslation
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);

            //The GL.glLoadMatrix function replaces the current matrix with
            //the one specified in its argument.
            //The current matrix is the
            //projection matrix, modelview matrix, or texture matrix,
            //determined by the current matrix mode (now is ModelView mode)
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix

            //The GL.glMultMatrix function multiplies the current matrix by
            //the one specified in its argument.
            //That is, if M is the current matrix and T is the matrix passed to
            //GL.glMultMatrix, then M is replaced with M � T
            GL.glMultMatrixd(CurrentRotationTraslation);

            //save the matrix product in AccumulatedRotationsTraslations
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //replace ModelViev Matrix with stored ModelVievMatrixBeforeSpecificTransforms
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            //multiply it by KeyCode defined AccumulatedRotationsTraslations matrix
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            
            GL.glPushMatrix();
            GL.glScalef(1, -1, 1); //swap on Z axis
            GL.glEnable(GL.GL_CULL_FACE);
            GL.glCullFace(GL.GL_BACK);
            DrawShipBodey();
            GL.glCullFace(GL.GL_FRONT);
            // DrawFigures();
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();

            DrawShipBodey();
            DrawTopBort();
            
            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);

        }

		protected virtual void InitializeGL()
		{
			m_uint_HWND = (uint)p.Handle.ToInt32();
			m_uint_DC   = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
			// result in a failure to subsequently create the RC.
			WGL.wglSwapBuffers(m_uint_DC);

			WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
			WGL.ZeroPixelDescriptor(ref pfd);
			pfd.nVersion        = 1; 
			pfd.dwFlags         = (WGL.PFD_DRAW_TO_WINDOW |  WGL.PFD_SUPPORT_OPENGL |  WGL.PFD_DOUBLEBUFFER); 
			pfd.iPixelType      = (byte)(WGL.PFD_TYPE_RGBA);
			pfd.cColorBits      = 32;
			pfd.cDepthBits      = 32;
			pfd.iLayerType      = (byte)(WGL.PFD_MAIN_PLANE);

			int pixelFormatIndex = 0;
			pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
			if(pixelFormatIndex == 0)
			{
				MessageBox.Show("Unable to retrieve pixel format");
				return;
			}

			if(WGL.SetPixelFormat(m_uint_DC,pixelFormatIndex,ref pfd) == 0)
			{
				MessageBox.Show("Unable to set pixel format");
				return;
			}
			//Create rendering context
			m_uint_RC = WGL.wglCreateContext(m_uint_DC);
			if(m_uint_RC == 0)
			{
				MessageBox.Show("Unable to get rendering context");
				return;
			}
			if(WGL.wglMakeCurrent(m_uint_DC,m_uint_RC) == 0)
			{
				MessageBox.Show("Unable to make rendering context current");
				return;
			}


            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            Draw();
        }

        protected virtual void initRenderingGL()
		{
			if(m_uint_DC == 0 || m_uint_RC == 0)
				return;
			if(this.Width == 0 || this.Height == 0)
				return;
            GL.glClearColor(0.5f, 0.5f, 0.5f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
			GL.glMatrixMode ( GL.GL_PROJECTION );
			GL.glLoadIdentity();
            
            //nice 3D
			GLU.gluPerspective( 45.0,  1.0, 0.4,  100.0);

            
            GL.glMatrixMode ( GL.GL_MODELVIEW );
			GL.glLoadIdentity();

            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            //InitTexture("battleship texture.jpg");
            GenerateTextures();

        }

        public uint[] texture;		// texture

        void InitTexture(string imageBMPfile)
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            texture = new uint[2];		// storage for texture

            Bitmap image = new Bitmap(imageBMPfile);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glGenTextures(1, texture);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);


            //  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
            //  OpenGL.cs
            //  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
            //  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height, 0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering

            image.UnlockBits(bitmapdata);
            image.Dispose();
        }


        public uint[] Textures = new uint[11];

        void GenerateTextures()
        {
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glGenTextures(11, Textures);
            string[] imagesName ={"battleship texture.jpg","ship top.jpg", "bortfackfront.jpg","bottom.jpg", "top.jpg", "left.jpg", "front.jpg", "right.jpg", "back.jpg","door.jpg", "towers.jpg" };
            for (int i = 0; i < 11; i++)
            {
                Bitmap image = new Bitmap(imagesName[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[i]);
                //2D for XYZ
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                                                              0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
        }


        public float Cyl_angle;
            public float SHOULDER_angle;
            public float ROBOT_angle;
            public float barrel_angle;
            public float radar_angle;
            public float canon_angle;
            public float launch;
            public float alpha;




            uint ROBOT_LIST, ARM_LIST, SHOULDER_LIST;
            float r;

            void PrepareLists()
            {
                float Cyl_length, SHOULDER_length;
                Cyl_length = 1;
                Cyl_angle = 0;
                SHOULDER_length = 2.5f;
                SHOULDER_angle = 10;
                ROBOT_angle = 45;
                launch = 0;
                canon_angle = 0;
                barrel_angle = 0;
                radar_angle = 0;
                r = 0.03f;

                ROBOT_LIST = GL.glGenLists(3);
                ARM_LIST = ROBOT_LIST + 1;
                SHOULDER_LIST = ROBOT_LIST + 2;

                GL.glPushMatrix();
                GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                       //cone
                GL.glEndList();
             GL.glPopMatrix();
           
             CreateRobotList();
            }

            public void CreateRobotList()
            {

                GL.glPushMatrix();
                //
                // hierarchical list
                //
                GL.glNewList(ROBOT_LIST, GL.GL_COMPILE);
            	       
	                   // BASE : no rotations!!! Angle will be implemented in the CALL routine
	                   //                   before call to CreateRobotList()
                GL.glColor3f(0, 0, 0.5f);
                       GLU.gluCylinder (obj, 3 * r, 3 * r, r * 1.2, 40, 20);
                       GL.glTranslated( 0, 0, r * 1.2);
                       GLU.gluDisk (obj, 0, 3 * r, 40, 20);
                       GL.glColor3f (0, 0, 1);
                       GLU.gluSphere (obj, r * 1.2, 20, 20);
                       // end base

                       // transformations
                       GL.glRotatef (SHOULDER_angle, 1, 0, 0);

                       // call SHOULDER 
                       GL.glCallList (SHOULDER_LIST);
                       
                       // transformations
		               //no need in glTranslated 0, 0, SHOULDER_length
                       //it is located properly now !!!
                       GL.glRotatef (Cyl_angle, 1, 0, 0);
                       
                       // call ARM  
		               GL.glCallList (ARM_LIST);
                GL.glEndList();
             GL.glPopMatrix();
             }

    
    }

}


