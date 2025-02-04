#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#include "glut.h"

// dimensiunea ferestrei in pixeli
#define dim 300

unsigned char prevKey;
   double pi = 4 * atan(1.0);
   double e= 2.71828182845904523536;

// concoida lui Nicomede (concoida dreptei)
// $x = a + b \cdot cos(t), y = a \cdot tg(t) + b \cdot sin(t)$. sau
// $x = a - b \cdot cos(t), y = a \cdot tg(t) - b \cdot sin(t)$. unde
// $t \in (-\pi / 2, \pi / 2)$
void Display1() {
   double xmax, ymax, xmin, ymin;
   double a = 1, b = 2;
   //double pi = 4 * atan(1.0);
   double ratia = 0.05;
   double t;

   // calculul valorilor maxime/minime ptr. x si y
   // aceste valori vor fi folosite ulterior la scalare
   xmax = a - b - 1;
   xmin = a + b + 1;
   ymax = ymin = 0;
   for (t = - pi/2 + ratia; t < pi / 2; t += ratia) {
      double x1, y1, x2, y2;
      x1 = a + b * cos(t);
      xmax = (xmax < x1) ? x1 : xmax;
      xmin = (xmin > x1) ? x1 : xmin;

      x2 = a - b * cos(t);
      xmax = (xmax < x2) ? x2 : xmax;
      xmin = (xmin > x2) ? x2 : xmin;

      y1 = a * tan(t) + b * sin(t);
      ymax = (ymax < y1) ? y1 : ymax;
      ymin = (ymin > y1) ? y1 : ymin;

      y2 = a * tan(t) - b * sin(t);
      ymax = (ymax < y2) ? y2 : ymax;
      ymin = (ymin > y2) ? y2 : ymin;
   }

   xmax = (fabs(xmax) > fabs(xmin)) ? fabs(xmax) : fabs(xmin);
   ymax = (fabs(ymax) > fabs(ymin)) ? fabs(ymax) : fabs(ymin);

   // afisarea punctelor propriu-zise precedata de scalare
   glColor3f(1,0.1,0.1); // rosu
   glBegin(GL_LINE_STRIP); 
   for (t = - pi/2 + ratia; t < pi / 2; t += ratia) {
      double x1, y1, x2, y2;
      x1 = (a + b * cos(t)) / xmax;
      x2 = (a - b * cos(t)) / xmax;
      y1 = (a * tan(t) + b * sin(t)) / ymax;
      y2 = (a * tan(t) - b * sin(t)) / ymax;

      glVertex2f(x1,y1);
   }
   glEnd();

   glBegin(GL_LINE_STRIP); 
   for (t = - pi/2 + ratia; t < pi / 2; t += ratia) {
      double x1, y1, x2, y2;
      x1 = (a + b * cos(t)) / xmax;
      x2 = (a - b * cos(t)) / xmax;
      y1 = (a * tan(t) + b * sin(t)) / ymax;
      y2 = (a * tan(t) - b * sin(t)) / ymax;

      glVertex2f(x2,y2);
   }
   glEnd();
}

// graficul functiei 
// $f(x) = \bar sin(x) \bar \cdot e^{-sin(x)}, x \in \langle 0, 8 \cdot \pi \rangle$, 
void Display2() {
   double pi = 4 * atan(1.0);
   double xmax = 8 * pi;
   double ymax = exp(1.1);
   double ratia = 0.05;

   // afisarea punctelor propriu-zise precedata de scalare
   glColor3f(1,0.1,0.1); // rosu
   glBegin(GL_LINE_STRIP); 
   for (double x = 0; x < xmax; x += ratia) {
      double x1, y1;
      x1 = x / xmax;
      y1 = (fabs(sin(x)) * exp(-sin(x))) / ymax;

      glVertex2f(x1,y1);
   }
   glEnd();
}

double p1d(double x)
{
	int y=(int)x;
	int y1=(int)x+1;
	if(x-y<y1-x)
		return x-y;
	else
		return y1-x;
}
double p1f(double x)
{
	if(x==0)
		return 1;
	else
		return p1d(x)/x;
}
void Display3()
{
	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double i=0;i<=100;i+=0.05)
	{
		printf("\nX: %f \n%f", i/25,p1f(i));
		glVertex2f(i/25,p1f(i));		//pe 25 nu 100
	}
	glEnd();
	printf("\n--------------");

}

double p2x(double a,double b,double t)
{
	return 2*(a*cos(t)+b)*cos(t);
}
double p2y(double a,double b,double t)
{
	return 2*(a*cos(t)+b)*sin(t);
}
void Display4()
{
	double a=0.3;
	double b=0.2;

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=-pi+0.05;t<pi;t+=0.05)
	{
		printf("\nX: %f, \nY: %f",p2x(a,b,t),p2y(a,b,t));
		glVertex2f(p2x(a,b,t),p2y(a,b,t));
	}
	glEnd();
	printf("\n--------------");

}

double p3x(double a, double t)
{
	return a/(4*cos(t)*cos(t)-3);
}
double p3y(double a, double t)
{
	return (a*tan(t))/(4*cos(t)*cos(t)-3);
}
void Display5()
{
	double a=0.2;
	double r=pi/100;

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=-pi/2+r;t<pi/2;t+=r)
	{
		if(abs(t)!=pi/6)
		{
			printf("\nX: %f, \nY: %f",p3x(a,t),p3y(a,t));
			glVertex2f(p3x(a,t),p3y(a,t));
		}
	}
	glEnd();
	printf("\n--------------");

}

double p4x(double a,double b,double t)
{
	return a*t-b*sin(t);
}
double p4y(double a,double b,double t)
{
	return a-b*cos(t);
}
void Display6()
{
	double a=0.1;
	double b=0.2;		//25 nu 2

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=-10;t<=10;t+=0.1)
	{
		printf("\nX: %f, \nY: %f",p4x(a,b,t),p4y(a,b,t));
		glVertex2f(p4x(a,b,t),p4y(a,b,t));
	}
	glEnd();
	printf("\n--------------");

}

double p5x(double R,double r,double t)
{
	return (R+r)*cos((r/R)*t)-r*cos(t+(r/R)*t);
}
double p5y(double R,double r,double t)
{
	return (R+r)*sin((r/R)*t)-r*sin(t+(r/R)*t);
}
void Display7()
{
	double r=0.3;
	double R=0.1;		

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=0;t<=2*pi;t+=0.05)
	{
		printf("\nX: %f, \nY: %f",p5x(R,r,t),p5y(R,r,t));
		glVertex2f(p5x(R,r,t),p5y(R,r,t));
	}
	glEnd();
	printf("\n--------------");

}

double p6x(double R,double r,double t)
{
	return (R-r)*cos((r/R)*t)-r*cos(t-(r/R)*t);
}
double p6y(double R,double r,double t)
{
	return (R-r)*sin((r/R)*t)-r*sin(t-(r/R)*t);
}
void Display8()
{
	double r=0.3;
	double R=0.1;		

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=0;t<=2*pi;t+=0.05)
	{
		printf("\nX: %f, \nY: %f",p6x(R,r,t),p6y(R,r,t));
		glVertex2f(p6x(R,r,t),p6y(R,r,t));
	}
	glEnd();
	printf("\n--------------");

}

double r1(double a,double t)
{
	return a*sqrt(2*cos(2*t));
}
double r2(double a,double t)
{
	return -r1(a,t);
}
void Display9()
{
	double a=0.4;

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=-pi/4+0.05;t<pi/4;t+=0.0005)
	{
		printf("\nX: %f, \nY: %f",r2(a,t)*cos(t),r2(a,t)*sin(t));
		glVertex2f(r2(a,t)*cos(t),r2(a,t)*sin(t));
		//glVertex2f(r1(a,t)*cos(t),r1(a,t)*sin(t));
	}
	glEnd();
	glBegin(GL_LINE_STRIP);
	for(double t=-pi/4+0.05;t<pi/4;t+=0.05)
	{
		printf("\nX: %f, \nY: %f",r1(a,t)*cos(t),r1(a,t)*sin(t));
		glVertex2f(r1(a,t)*cos(t),r1(a,t)*sin(t));

	}
	glEnd();

	printf("\n--------------");

}

double r(double a,double t)
{
	return a*pow(e,1+t);
}
void Display10()
{
	double a=0.02;

	glColor3f(1,0,0);
	glBegin(GL_LINE_STRIP);
	for(double t=0.05;t<pi;t+=0.05)
	{
		printf("\nX: %f, \nY: %f",r(a,t)*cos(t),r(a,t)*sin(t));
		glVertex2f(r(a,t)*cos(t),r(a,t)*sin(t));
	}
	glEnd();

	printf("\n--------------");
}


void Init(void) {

   glClearColor(1.0,1.0,1.0,1.0);

   glLineWidth(1);

//   glPointSize(4);

   glPolygonMode(GL_FRONT, GL_LINE);
}

void Display(void) {
   glClear(GL_COLOR_BUFFER_BIT);

   switch(prevKey) {
   case '1':
      Display1();
      break;
   case '2':
      Display2();
      break;
   case '3':
	   Display3();
	   break;
   case '4':
	   Display4();
	   break;
   case '5':
	   Display5();
	   break;
   case '6':
	   Display6();
	   break;
   case '7':
	   Display7();
	   break;
   case '8':
	   Display8();
	   break;
   case '9':
	   Display9();
	   break;
   case '0':
	   Display10();
	   break;
   default:
      break;
   }

   glFlush();
}

void Reshape(int w, int h) {
   glViewport(0, 0, (GLsizei) w, (GLsizei) h);
}

void KeyboardFunc(unsigned char key, int x, int y) {
   prevKey = key;
   if (key == 27) // escape
      exit(0);
   glutPostRedisplay();
}

void MouseFunc(int button, int state, int x, int y) {
}

int main(int argc, char** argv) {

   glutInit(&argc, argv);
   
   glutInitWindowSize(dim, dim);

   glutInitWindowPosition(100, 100);

   glutInitDisplayMode (GLUT_SINGLE | GLUT_RGB);

   glutCreateWindow (argv[0]);

   Init();

   glutReshapeFunc(Reshape);
   
   glutKeyboardFunc(KeyboardFunc);
   
   glutMouseFunc(MouseFunc);

   glutDisplayFunc(Display);
   
   glutMainLoop();

   return 0;
}
