#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <fstream>

#include "glut.h"

using namespace std;

#define dim 300

class GrilaCarteziana
{
public:

	static const int n=20;		//dimensiunea grilei(nr linii/coloane)	Exista n+1*n+1 pozitii in care se pot desena pixeli
	double pr;					//raza pixelilor
	bool g[n+1][n+1];			//daca g[x][y]==true atunci exista un pixel desenat la (x,y)

	GrilaCarteziana(void)
	{
		for(int i=0;i<n;i++)
			for(int j=0;j<n;j++)
				g[i][j]=false;

		pr=0.3*(2/(double)n);	//pr e 0.3 din latimea unei coloane (pixeli disjuncti)
	}

	void drawGrid()				//desenarea grilei
	{
		printf("\nDrawing Grid...\n Intervals for n= %d :\n",n);
		glColor3f(0,0,0);
		glBegin(GL_LINES);
			for(double i=0;i<=n;i++)
			{
				double x=2*i/n-1;
				printf("%.0f-> %.3f\n",i,x);
				glVertex2f(x,1);
				glVertex2f(x,-1);
				glVertex2f(1,x);
				glVertex2f(-1,x);
			}
		glEnd();
	}
	void drawAllActive()		//folosit la redesenarea pixelilor cand se face refresh
	{
		for(int i=0;i<=n;i++)
			for(int j=0;j<=n;j++)
				if(g[i][j]==true)
					writePixelI(i,j);
	}

	void writePixel(int i,int j)		//deseneaza un pixel
	{
		double pi = 4 * atan(1.0);

		if(!(0<=i && i<=n && 0<=j && j<=n))
			printf("\nInvalid coords: ( %d, %d ) !",i,j);
		else
		{
			if(g[i][j]==true)
			{
				printf("\nPixel already was drawn at ( %d, %d ).",i,j);			//nu redeseneaza un pixel existent
				return;
			}

			double cx=2*i/(double)n-1;
			double cy=2*j/(double)n-1;

			glColor3f(0.5,0.5,0.5);
			glBegin(GL_POLYGON);
				for(double a=2*pi;a>0;a-=0.1)
					glVertex2f(cx+pr*cos(a),cy+pr*sin(a));
			glEnd();

			g[i][j]=true;
			printf("\nPixel drawn at ( %d, %d ).",i,j);
		}
	}
	void writePixelI(int i,int j)		//deseneaza un pixel Fara a verifica daca exista sau nu (pentru drawAllActive() )
	{
		double pi = 4 * atan(1.0);

		if(!(0<=i && i<=n && 0<=j && j<=n))
			printf("\nInvalid coords: ( %d, %d ) !",i,j);
		else
		{
			double cx=2*i/(double)n-1;
			double cy=2*j/(double)n-1;

			glColor3f(0.5,0.5,0.5);
			glBegin(GL_POLYGON);
				for(double a=2*pi;a>0;a-=0.1)
					glVertex2f(cx+pr*cos(a),cy+pr*sin(a));
			glEnd();

			g[i][j]=true;
			printf("\nPixel drawn at ( %d, %d ).",i,j);
		}
	}
	void cleanPixel(int i,int j)			//sterge un pixel
	{
		double pi = 4 * atan(1.0);

		if(!(0<=i && i<=n && 0<=j && j<=n))
			printf("\nInvalid coords: ( %d, %d ) !",i,j);
		else
		{
			if(g[i][j]==false)
			{
				printf("\nPixel is not drawn at ( %d, %d ).",i,j);
				return;
			}

			double cx=2*i/(double)n-1;
			double cy=2*j/(double)n-1;

			glColor3f(1,1,1);
			glBegin(GL_POLYGON);
				for(double a=2*pi;a>0;a-=0.1)
					glVertex2f(cx+pr*cos(a),cy+pr*sin(a));
			glEnd();

			glColor3f(0,0,0);
			glBegin(GL_LINES);
				glVertex2f(cx+pr,cy);
				glVertex2f(cx-pr,cy);
				glVertex2f(cx,cy-pr);
				glVertex2f(cx,cy+pr);
			glEnd();

			g[i][j]=false;
			printf("\nPixel cleaned at ( %d, %d ).",i,j);
		}

	}

	void drawNormalLine(int x0,int y0,int xn,int yn)		//deseneaza o linie normala in glut(GL_LINES)
	{
		double cx0=2*x0/(double)n-1;
		double cy0=2*y0/(double)n-1;
		double cxn=2*xn/(double)n-1;
		double cyn=2*yn/(double)n-1;
		
		glColor3f(1,0,0);
		glBegin(GL_LINES);
			glVertex2f(cx0,cy0);
			glVertex2f(cxn,cyn);
		glEnd();
	}
	void cleanNormalLine(int x0,int y0,int xn,int yn)		//sterge o linie normala in glut(GL_LINES)
	{
		double cx0=2*x0/(double)n-1;
		double cy0=2*y0/(double)n-1;
		double cxn=2*xn/(double)n-1;
		double cyn=2*yn/(double)n-1;
		
		glColor3f(1,1,1);
		glBegin(GL_LINES);
			glVertex2f(cx0,cy0);
			glVertex2f(cxn,cyn);
		glEnd();
	}

	void drawPixelLine(int x0,int y0,int xn,int yn)			//algoritmul(3) pentru deseneaza unui segment cu pixeli
	{
		drawNormalLine(x0,y0,xn,yn);

		printf("\nDrawing line...\n from ( %d, %d ) to ( %d, %d )",x0,y0,xn,yn);

		double mp=(double)(yn-y0)/(xn-x0);
		printf("\n		Panta: %f",mp);

		writePixel(x0,y0);

		int m=1;				
		if(abs(yn-y0)>abs(xn-x0))		//m=testeaza daca |panta|<=1 , reface extremitatile pentru <=1
		{
			int aux=x0;
			x0=y0;
			y0=aux;
			aux=xn;
			xn=yn;
			yn=aux;
			m=-1;
		}

		int ydir=1;			//ydir=indica directia coord y (spre sus sau jos)
		if(y0>yn)
			ydir=-1;

		int xdir=1;			//xdir=indica directia coord x (spre dreapta sau stanga)
		if(x0>xn)
			xdir=-1;

		int dx=abs(xn-x0);
		int dy=abs(yn-y0);
		int x=x0, y=y0;

		int d=2*dy-dx;
		int dE=2*dy;
		int dNE=2*(dy-dx);

		if(xdir>0)
			while(x<xn)
			{
				x++;
				if(d<=0)
					d+=dE;
				else
				{
					d+=dNE;
					if(ydir>0)
						y++;
					else
						y--;
				}

				if(m*dy<=m*dx)
					writePixel(x,y);
				else
					writePixel(y,x);
			}
		else
			while(x>xn)
			{
				x--;
				if(d<=0)
					d+=dE;
				else
				{
					d+=dNE;
					if(ydir>0)
						y++;
					else
						y--;
				}

				if(m*dy<=m*dx)
					writePixel(x,y);
				else
					writePixel(y,x);
			}
	}
	void cleanPixelLine(int x0,int y0,int xn,int yn)		//sterge un segment de pixeli
	{
		cleanNormalLine(x0,y0,xn,yn);

		printf("\nDrawing line...\n from ( %d, %d ) to ( %d, %d )",x0,y0,xn,yn);

		double mp=(double)(yn-y0)/(xn-x0);
		printf("\n		Panta: %f",mp);

		cleanPixel(x0,y0);

		int m=1;
		if(abs(yn-y0)>abs(xn-x0))
		{
			int aux=x0;
			x0=y0;
			y0=aux;
			aux=xn;
			xn=yn;
			yn=aux;
			m=-1;
		}

		int ydir=1;
		if(y0>yn)
			ydir=-1;

		int xdir=1;
		if(x0>xn)
			xdir=-1;

		int dx=abs(xn-x0);
		int dy=abs(yn-y0);
		int x=x0, y=y0;

		int d=2*dy-dx;
		int dE=2*dy;
		int dNE=2*(dy-dx);

		if(xdir>0)
			while(x<xn)
			{
				x++;
				if(d<=0)
					d+=dE;
				else
				{
					d+=dNE;
					if(ydir>0)
						y++;
					else
						y--;
				}

				if(m*dy<=m*dx)
					cleanPixel(x,y);
				else
					cleanPixel(y,x);
			}
		else
			while(x>xn)
			{
				x--;
				if(d<=0)
					d+=dE;
				else
				{
					d+=dNE;
					if(ydir>0)
						y++;
					else
						y--;
				}

				if(m*dy<=m*dx)
					cleanPixel(x,y);
				else
					cleanPixel(y,x);
			}
	}

	void drawNormalCircle(int x, int y, int r)		//deseneaza un cerc normal in glut
	{
		double pi = 4 * atan(1.0);

		double cx=2*x/(double)n-1;
		double cy=2*y/(double)n-1;
		double R=(double)2*r/n;
	
		glColor3f(1,0,0);
		glBegin(GL_POLYGON);
			for(double a=0;a<2*pi;a+=0.1)
				glVertex2f(cx+R*cos(a),cy+R*sin(a));
		glEnd();
	}

	void drawCircleSim(int x,int y,int x0,int y0)	//folosit la desenarea octalelor cercului prin simetrie.Este relativ la centrul (x0,y0)
	{
		writePixel(x,y);
		writePixel(2*x0-x,y);
		writePixel(x,2*y0-y);
		writePixel(2*x0-x,2*y0-y);

		if(x!=y)
		{
			writePixel(y,x);
			writePixel(2*x0-y,x);
			writePixel(y,2*y0-x);
			writePixel(2*x0-y,2*y0-x);
		}
	}
	void drawCircleOct1(int x,int y,int x0,int y0)		//deseneaza doar pixelul in primul octal (prin simetrie fata de bisectoarea din cadranul I)
	{
		if(x!=y)
			writePixel(y,x);
	}

	void drawPixelCircle(int x0, int y0, int r)			//deseneaza un intreg cerc
	{
		int x=0;
		int y=r;

		int d=1-r;
		int dE=3;
		int dSE=-2*r+5;
		drawCircleSim(x+x0,y+y0,x0,y0);
		drawCircleSim(x+x0,y+y0+1,x0,y0);
		drawCircleSim(x+x0,y+y0-1,x0,y0);
		while(y>x)
		{
			if(d<0)
			{
				d+=dE;
				dE+=2;
				dSE+=2;
			}
			else
			{
				d+=dSE;
				dE+=2;
				dSE+=4;
				y--;
			}
			x++;
			drawCircleSim(x+x0,y+y0,x0,y0);
			drawCircleSim(x+x0,y+y0+1,x0,y0);
			drawCircleSim(x+x0,y+y0-1,x0,y0);
		}
	}
	void drawPixelCircleOct1(int x0, int y0, int r)			//deseneaza doar partea cercului din octalul I
	{
		int x=0;
		int y=r;

		int d=1-r;
		int dE=3;
		int dSE=-2*r+5;
		drawCircleOct1(x+x0,y+y0,x0,y0);
		drawCircleOct1(x+x0,y+y0+1,x0,y0);
		drawCircleOct1(x+x0,y+y0-1,x0,y0);
		while(y>x)
		{
			if(d<0)
			{
				d+=dE;
				dE+=2;
				dSE+=2;
			}
			else
			{
				d+=dSE;
				dE+=2;
				dSE+=4;
				y--;
			}
			x++;
			drawCircleOct1(x+x0,y+y0,x0,y0);
			drawCircleOct1(x+x0,y+y0+1,x0,y0);
			drawCircleOct1(x+x0,y+y0-1,x0,y0);
		}
	}


void fillPixelElipse(double x0,double y0,double a,double b)			//incercare de umplere a elipsei (neterminat)
{
	double pi=4*atan(1.0);

	double x=0;	double y=b;
	double fx=0.0;
	double dE,dSE,dS;

	writePixel(x0+x, y0+y);

	while(a*a*(y-0.5)>b*b*(x+1))
	{
		dE=b*b*(2*x+1);
		dSE=b*b*(2*x+1)+a*a*(-2*y+1);
		if(fx+dE<=0.0)
		{
			fx+=dE;
			x++;
			writePixel(y0+y,x0+x);
		}
		else
			if(fx+dSE<=0.0)
			{
				fx+=dSE;
				x++;
				y--;
				writePixel(y+y0,x+x0);
			}
	}

	while(y>0)
	{
		dSE=b*b*(2*x+1)+a*a*(-2*y+1);
		dS=a*a*(-2*y+1);
		if(fx+dSE<=0.0)
		{
			fx+=dSE;
			x++;
			y--;
		}
		else
		{
			fx+=dS;
			y--;
		}
		writePixel(y0+y,x0+x);
	}
}

	~GrilaCarteziana(void){}

	void drawPolygon(char* s)			//inceput de procesare la poligon (neterminat)
	{
		ifstream in;
		in.open(s);

		int np=0;
		int xi,yi,xf,yf;
		int x0,y0;
		int peak=0;

		int peaks[20];

		in>>np;
		if(np!=0)
		{
			in>>x0;
			in>>y0;
		}
		for(int i=0;i<np-1;i++)
		{
			in>>xi;
			in>>yi;
			in>>xf;
			in>>yf;

			if(xi<xf)
				peak=1;
			if(xi>xf && peak==1)
			{
				peak=0;
				peaks[xi]=yf;
			}
		}
	}

};

unsigned char prevKey;
GrilaCarteziana* gc=new GrilaCarteziana();

void Display1()		
{

	gc->drawNormalCircle(10,10,8);
	gc->drawPixelCircleOct1(10,10,8);

}

void Display2() 
{
	gc->fillPixelElipse(10,10,5,5);
}
/*
void Display3()
{
	gc->drawPixelLine(10,10,0,10);

	gc->drawPixelLine(10,10,0,5);		//q3
	gc->drawPixelLine(10,10,0,0);
	gc->drawPixelLine(10,10,5,0);
}

void Display4()
{
	gc->drawPixelLine(10,10,10,0);

	gc->drawPixelLine(10,10,15,0);		//q4
	gc->drawPixelLine(10,10,20,0);
	gc->drawPixelLine(10,10,20,5);
}

void Display5()
{
	gc->cleanPixelLine(10,10,20,10);

	gc->cleanPixelLine(10,10,20,15);	//q1
	gc->cleanPixelLine(10,10,20,20);
	gc->cleanPixelLine(10,10,15,20);
}

void Display6()
{
	gc->cleanPixelLine(10,10,10,20);

	gc->cleanPixelLine(10,10,5,20);		//q2
	gc->cleanPixelLine(10,10,0,20);
	gc->cleanPixelLine(10,10,0,15);
}

void Display7()
{
	gc->cleanPixelLine(10,10,0,10);

	gc->cleanPixelLine(10,10,0,5);		//q3
	gc->cleanPixelLine(10,10,0,0);
	gc->cleanPixelLine(10,10,5,0);
}

void Display8()
{
	gc->cleanPixelLine(10,10,10,0);

	gc->cleanPixelLine(10,10,15,0);		//q4
	gc->cleanPixelLine(10,10,20,0);
	gc->cleanPixelLine(10,10,20,5);
}
*/

void Init(void) {

   glClearColor(1.0,1.0,1.0,1.0);

   glLineWidth(1);

//   glPointSize(4);

   glPolygonMode(GL_FRONT, GL_LINE);
}

void Display(void) {
   glClear(GL_COLOR_BUFFER_BIT);

   gc->drawGrid();
   gc->drawAllActive();

   switch(prevKey) {
   case '1':
      Display1();
      break;
   case '2':
      Display2();
      break;
/*   case '3':
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
	   break;*/
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
