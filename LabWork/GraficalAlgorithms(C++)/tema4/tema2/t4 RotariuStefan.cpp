#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <string.h>
#include <fstream>
#include "glut.h"

using namespace std;
#define dim 300

class varf
{
public:
	int x;
	int y;

	varf()
	{
		x=-1;
		y=-1;
	}
	varf(int px, int py)
	{
		x=px;
		y=py;
	}
};
class muchie
{
public:
	varf vi;
	varf vf;
	
	muchie()
	{
		vi=varf();
		vf=varf();
	}
	muchie(varf pi,varf pf)
	{
		vi.x=pi.x;
		vi.y=pi.y;
		vf.x=pf.x;
		vf.y=pf.y;
	}
	muchie(int  pix,int piy,int pfx,int pfy)
	{
		vi.x=pix;
		vi.y=piy;
		vf.x=pfx;
		vf.y=pfy;
	}
};
class poligon
{
public:
	int n;
	muchie ml[100];

	poligon()
	{
		n=0;
	}
	poligon(char* str)
	{
		ifstream in(str);

		in>>n;
		if(n<3)
		{
			printf("\nn<3 !");
			n=0;
		}
		
		else
		{
			in>>ml[0].vi.x;
			in>>ml[0].vi.y;
			for(int i=1;i<n;i++)
			{
				in>>ml[i-1].vf.x;
				in>>ml[i-1].vf.y;

				ml[i].vi.x=ml[i-1].vf.x;
				ml[i].vi.y=ml[i-1].vf.y;
			}
			ml[n-1].vf.x=ml[0].vi.x;
			ml[n-1].vf.y=ml[0].vi.y;
		}
	}

	muchie m(int i)
	{
		if(i<n)
			return ml[i];
		else
		{
			printf("\nInvalid muchie index !");
			return muchie();
		}
	}
	varf v(int i)
	{
		if(i<n)
			return ml[i].vi;
		else
		{
			printf("\nInvalid varf index !");
			return varf();
		}
	}

	void afis()
	{
		printf("\nPoligon -> n=%d :",n);
		for(int i=0;i<n;i++)
			printf("\nMuchie %d : (%d,%d), (%d,%d)",i,m(i).vi.x,m(i).vi.y,m(i).vf.x,m(i).vf.y);
		printf("\n");
	}
};

class inter
{
public:
	int ymax;
	double xmin;
	double ratia;

	inter()
	{
		ymax=-1;
		xmin=-1;
		ratia=-1;
	}
	inter(int p_ymax,double p_xmin, double p_ratia)
	{
		ymax=p_ymax;
		xmin=p_xmin;
		ratia=p_ratia;
	}
	void setxmin(double x)
	{
		xmin=x;
	}
};
class interline
{
public:
	int n;
	inter in[100];

	interline()
	{
		n=0;
	}
	inter I(int i)
	{
		if(i<n)
			return in[i];
		else
		{
			printf("\nInvalid inter index ! (on the line)");
			return inter();
		}
	}
	void addI(inter p_inter)
	{
		//in[n]=inter(p_inter.ymax,p_inter.xmin,p_inter.ratia);
		in[n].ymax=p_inter.ymax;
		in[n].xmin=p_inter.xmin;
		in[n].ratia=p_inter.ratia;
		n=n+1;
	}
	void delI(int i)
	{
		for(int x=i;x<n;x++)
			in[x]=in[x+1];
		n=n-1;
	}
};
class inters
{
public:
	int n;
	interline inl[100];

	inters()
	{
		n=0;
	}
	inters(int ny)
	{
		n=ny;
	}
	interline IL(int y)
	{
		if(y<n)
			return inl[y];
		else
		{
			printf("\nInvalid inter line index !");
			return interline();
		}
	}
	inter I(int y, int i)
	{
		return IL(y).I(i);
	}
	void addI(int y,inter p_inter)
	{
		if(y<n)
			inl[y].addI(p_inter);
		else
			printf("\nInvalid inter line index !");
	}
};

class xmarks
{
public:
	int n;
	double x[100];

	xmarks()
	{
		n=0;
	}
	void add(double px)
	{
		x[n]=px;
		n++;
	}
};

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

	void writePixelC(double cx,double cy)
	{
		double pi = 4 * atan(1.0);

		if(!(-1<=cx && cx<=1 && -1<=cy && cy<=1))
			printf("\nInvalid coords: ( %d, %d ) !",cx,cy);
		else
		{
			int i=(1+cx)*n/2;
			int j=(1+cy)*n/2;

			if(g[i][j]==true)
			{
				printf("\nPixel already was drawn at ( %d, %d ).",i,j);			//nu redeseneaza un pixel existent
				return;
			}

			glColor3f(0.5,0.5,0.5);
			glBegin(GL_POLYGON);
				for(double a=2*pi;a>0;a-=0.1)
					glVertex2f(cx+pr*cos(a),cy+pr*sin(a));
			glEnd();

			g[i][j]=true;
			printf("\nPixel drawn at ( %d, %d ).",i,j);
		}
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
			for(double t=0;t<2*pi;t+=0.1)
				glVertex2f(cx+R*cos(t),cy+R*sin(t));
		glEnd();
	}
	void cleanNormalCircle(int x, int y, int r)		//sterge un cerc normal in glut
	{
		double pi = 4 * atan(1.0);

		double cx=2*x/(double)n-1;
		double cy=2*y/(double)n-1;
		double R=(double)2*r/n;
	
		glColor3f(1,1,1);
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
		{
			writePixel(y,x);
			writePixel(x,y);
		}
	}
	void cleanCircleSim(int x,int y,int x0,int y0)	//folosit la stergerea octalelor cercului prin simetrie.Este relativ la centrul (x0,y0)
	{
		cleanPixel(x,y);
		cleanPixel(2*x0-x,y);
		cleanPixel(x,2*y0-y);
		cleanPixel(2*x0-x,2*y0-y);

		if(x!=y)
		{
			cleanPixel(y,x);
			cleanPixel(2*x0-y,x);
			cleanPixel(y,2*y0-x);
			cleanPixel(2*x0-y,2*y0-x);
		}
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
	void cleanPixelCircle(int x0, int y0, int r)			//deseneaza un intreg cerc
	{
		int x=0;
		int y=r;

		int d=1-r;
		int dE=3;
		int dSE=-2*r+5;
		cleanCircleSim(x+x0,y+y0,x0,y0);
		cleanCircleSim(x+x0,y+y0+1,x0,y0);
		cleanCircleSim(x+x0,y+y0-1,x0,y0);
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
			cleanCircleSim(x+x0,y+y0,x0,y0);
			cleanCircleSim(x+x0,y+y0+1,x0,y0);
			cleanCircleSim(x+x0,y+y0-1,x0,y0);
		}
	}

	void drawNormalElipse(int x,int y, int a, int b)		//desenare elipsa normala in glut
	{
		double pi = 4 * atan(1.0);

		double cx=2*x/(double)n-1;
		double cy=2*y/(double)n-1;
		double A=(double)2*a/n;
		double B=(double)2*b/n;

		glColor3f(1,0,0);
		glBegin(GL_POLYGON);
			for(double t=0;t<2*pi;t+=0.1)
				glVertex2f(cx+A*cos(t),cy+B*sin(t));

		glEnd();
	}
	void cleanNormalElipse(int x,int y, int a, int b)		//desenare elipsa normala in glut
	{
		double pi = 4 * atan(1.0);

		double cx=2*x/(double)n-1;
		double cy=2*y/(double)n-1;
		double A=(double)2*a/n;
		double B=(double)2*b/n;

		glColor3f(1,1,1);
		glBegin(GL_POLYGON);
			for(double t=0;t<2*pi;t+=0.1)
				glVertex2f(cx+A*cos(t),cy+B*sin(t));

		glEnd();
	}

	void fillElipseLineSim(double y, double x0, double x, double y0)	//adaugarea unei linii in elipsa in C1 si prin simetrie in celelalte(ca la cerc)
	{
		for(int xi=x0;xi<=x;xi++)
		{
			writePixel(xi,y);
			writePixel(2*x0-xi,y);
			writePixel(xi,2*y0-y);
			writePixel(2*x0-xi,2*y0-y);
		}
	}
	void fillElipseLineC3(double y, double x0, double x, double y0)		//adaugarea unei linii in elipsa doar pentru C3
	{
		for(int xi=x0;xi<=x;xi++)
			writePixel(2*x0-xi,2*y0-y);
	}	
	void cleanElipseLineSim(double y, double x0, double x, double y0)	//adaugarea unei linii in elipsa in C1 si prin simetrie in celelalte(ca la cerc)
	{
		for(int xi=x0;xi<=x;xi++)
		{
			cleanPixel(xi,y);
			cleanPixel(2*x0-xi,y);
			cleanPixel(xi,2*y0-y);
			cleanPixel(2*x0-xi,2*y0-y);
		}
	}

	void fillPixelElipse(double x0,double y0,double a,double b)			//umplerea elipsei cu pixeli
	{
		double pi=4*atan(1.0);

		double x=0;	double y=b;
		double fx=0;
		double dE,dSE,dS;

		fillElipseLineSim(y0+y,x0,x0+x,y0);

		while(a*a*(y-0.5)>b*b*(x+1))
		{
			dE=b*b*(2*x+1);
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			if(fx+dE<=0)
			{
				fx+=dE;
				x+=1;;
				fillElipseLineSim(y0+y,x0,x0+x,y0);
			}
			else
				if(fx+dSE<=0)
				{
					fx+=dSE;
					x+=1;
					y-=1;
					fillElipseLineSim(y0+y,x0,x0+x,y0);
				}
		}
		printf("\n===================");
		while(y>0)
		{
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			dS=a*a*(-2*y+1);
			if(fx+dSE<=0)
			{
				fx+=dSE;
				x+=1;
				y-=1;
			}
			else
			{
				fx+=dS;
				y-=1;
			}
			fillElipseLineSim(y0+y,x0,x0+x,y0);
		}
	}
	void fillPixelElipseC3(double x0,double y0,double a,double b)			//umplerea elipsei cu pixeli
	{
		double pi=4*atan(1.0);

		double x=0;	double y=b;
		double fx=0;
		double dE,dSE,dS;

		fillElipseLineC3(y0+y,x0,x0+x,y0);

		while(a*a*(y-0.5)>b*b*(x+1))
		{
			dE=b*b*(2*x+1);
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			if(fx+dE<=0)
			{
				fx+=dE;
				x+=1;;
				fillElipseLineC3(y0+y,x0,x0+x,y0);
			}
			else
				if(fx+dSE<=0)
				{
					fx+=dSE;
					x+=1;
					y-=1;
					fillElipseLineC3(y0+y,x0,x0+x,y0);
				}
		}
		printf("\n===================");
		while(y>0)
		{
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			dS=a*a*(-2*y+1);
			if(fx+dSE<=0)
			{
				fx+=dSE;
				x+=1;
				y-=1;
			}
			else
			{
				fx+=dS;
				y-=1;
			}
			fillElipseLineC3(y0+y,x0,x0+x,y0);
		}
	}
	void cleanPixelElipse(double x0,double y0,double a,double b)			//umplerea elipsei cu pixeli
	{
		double pi=4*atan(1.0);

		double x=0;	double y=b;
		double fx=0;
		double dE,dSE,dS;

		cleanElipseLineSim(y0+y,x0,x0+x,y0);

		while(a*a*(y-0.5)>b*b*(x+1))
		{
			dE=b*b*(2*x+1);
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			if(fx+dE<=0)
			{
				fx+=dE;
				x+=1;;
				cleanElipseLineSim(y0+y,x0,x0+x,y0);
			}
			else
				if(fx+dSE<=0)
				{
					fx+=dSE;
					x+=1;
					y-=1;
					cleanElipseLineSim(y0+y,x0,x0+x,y0);
				}
		}
		printf("\n===================");
		while(y>0)
		{
			dSE=b*b*(2*x+1)+a*a*(-2*y+1);
			dS=a*a*(-2*y+1);
			if(fx+dSE<=0)
			{
				fx+=dSE;
				x+=1;
				y-=1;
			}
			else
			{
				fx+=dS;
				y-=1;
			}
			cleanElipseLineSim(y0+y,x0,x0+x,y0);
		}
	}

	void drawNormalPoligon(poligon pol)			//functie pt. desenarea poligon normal in glut
	{
		double cx;
		double cy;

		glColor3f(1,0,0);
		glBegin(GL_LINES);
			cx=2*pol.v(0).x/(double)n-1;
			cy=2*pol.v(0).y/(double)n-1;
			glVertex2f(cx,cy);
			for(int i=1;i<pol.n;i++)
			{
				cx=2*pol.v(i).x/(double)n-1;
				cy=2*pol.v(i).y/(double)n-1;
				glVertex2f(cx,cy);
				glVertex2f(cx,cy);
			}
			cx=2*pol.v(0).x/(double)n-1;
			cy=2*pol.v(0).y/(double)n-1;
			glVertex2f(cx,cy);
		glEnd();
	}
	void fillPoligon()						//incercare de scriere din curs
	{
		poligon pol("poligon.txt");
		pol.afis();
		drawNormalPoligon(pol);
		inters et(n+1);

		int xm,ym,xM,yM;
		bool change;

		for(int i=0;i<pol.n;i++)
		{
			if(pol.m(i).vi.y!=pol.m(i).vf.y)
			{
				ym=min(pol.m(i).vi.y,pol.m(i).vf.y);
				yM=max(pol.m(i).vi.y,pol.m(i).vf.y);
				xm=(ym==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;
				xM=(yM==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;

				et.addI(ym,inter(yM,xm,(xm-xM)/(double)(ym-yM)));
			}
		}

		for(int i=0;i<=n;i++)
		{
			do
			{
				change=false;
				if(et.IL(i).n==0)
					break;
				for(int j=0;j<et.IL(i).n-1;j++)
					if(et.I(i,j).xmin>et.I(i,j+1).xmin)
					{
						inter aux=et.I(i,j);
						et.I(i,j)=et.I(i,j+1);
						et.I(i,j+1)=aux;
						change=true;
					}
			}while(change==true);
		}

		int y,k;
		interline aet;

		
		y=-1;
		for(int i=0;i<=n;i++)
		{
			if(et.IL(i).n!=0)
			{
				y=i;
				break;
			}
		}
		if(y==-1)	return;
		
		do
		{
			for(int i=0;i<et.IL(y).n;i++)
				aet.addI(et.I(y,i));

			for(int i=0;i<aet.n;i++)
				if(aet.I(i).ymax==y)
					aet.delI(i);

			k=aet.n;
			while(k>=2)
			{
				for(int i=0;i<k;i++)
					if(aet.I(i).xmin>aet.I(i+1).xmin)
					{
						inter aux=aet.I(i);
						aet.I(i)=aet.I(i+1);
						aet.I(i+1)=aux;
					}
					k--;
			}

			//ssm(y)=aet;

			y++;

			for(int i=0;i<aet.n;i++)
				if(aet.I(i).ratia!=0)
					aet.I(i).setxmin(aet.I(i).xmin+aet.I(i).ratia);
		}while(aet.n!=0 ||et.IL(y).n!=0);

	}
	void fillPoligonS()				//umplere poligon cu date din fisierul "poligon.txt"
	{
		poligon pol("poligon.txt");		//preluare/construire poligon din fisier (logic)
		pol.afis();

		drawNormalPoligon(pol);			//desenare poligon normal in glut

		double mp;						//pt. memorare panta
		xmarks ix[n];					//liste de coord x ale pctelor de intersectie (pentru fiecare y=linie)

		int ym,yM,xm,xM;				//gasirea/memorarea intersectiilor
		for(int i=0;i<pol.n;i++)
		{
			ym=min(pol.m(i).vi.y,pol.m(i).vf.y);
			yM=max(pol.m(i).vi.y,pol.m(i).vf.y);
			xm=(ym==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;
			xM=(yM==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;

			if(xm==xM)
				mp=0;
			else
				mp=(ym-yM)/double(xm-xM);

			for(int j=ym;j<=yM;j++)
				if(mp==0)
					ix[j].add(xm);
				else
					ix[j].add( ((j-ym)/mp)+xm );
		}

		bool change;						//ordonarea intersectiilor
		for(int i=0;i<=n;i++)
			do
			{
				change=false;
				if(ix[i].n==0)
					break;
				for(int j=0;j<ix[i].n-1;j++)
					if(ix[i].x[j]>ix[i].x[j+1])
					{
						double aux=ix[i].x[j];
						ix[i].x[j]=ix[i].x[j+1];
						ix[i].x[j+1]=aux;
						change=true;
					}
			}while(change==true);

		for(int i=0;i<=n;i++)				//afisarea intersectii
		{
			printf("\nLinia %d: ",i);
			for(int j=0;j<ix[i].n;j++)
			{
				printf("%f, ",ix[i].x[j]);
			}
		}
		printf("\n");

		for(int i=0;i<=n;i++)				//umprerea Strict interiorului
		{
			for(int j=0;j<ix[i].n;j+=2)
			{
				if(ix[i].x[j]==ix[i].x[j+1])
					if(ix[i].n%2>0)
						j++;

				for(int t=ix[i].x[j]+1;t<ix[i].x[j+1];t++)
					writePixel(t,i);
			}
		}
		for(int i=0;i<pol.n;i++)				//eliminarea liniilor orizontale din nord
		{
			if(pol.m(i).vi.y==pol.m(i).vf.y)
				for(int j=min(pol.m(i).vi.x,pol.m(i).vf.x);j<=max(pol.m(i).vi.x,pol.m(i).vf.x);j++)
					if(g[j][pol.m(i).vi.y+1]==false)
						cleanPixel(j,pol.m(i).vi.y);
		}

		int X;								//adaugarea punctelor din marginea Vest plus Sud
		for(int i=0;i<=n;i++)
			for(int j=ix[i].n-1;j>=0;j--)
			{
				if(ix[i].x[j]==(int)ix[i].x[j])
				{
					X=(int)ix[i].x[j];
					if(X!=n)
						if(g[X+1][i]==true)
							writePixel(X,i);
						else
							if(X==0)
								writePixel(X,i);
							else
								if(g[X-1][i]==false)
									if(g[X][i+1]==true)
										writePixel(X,i);
				}
			}

	}
	void cleanPoligonS()				//umplere poligon cu date din fisierul "poligon.txt"
	{
		poligon pol("poligon.txt");		//preluare/construire poligon din fisier (logic)
		pol.afis();

		double mp;						//pt. memorare panta
		xmarks ix[n];					//liste de coord x ale pctelor de intersectie (pentru fiecare y=linie)

		int ym,yM,xm,xM;				//gasirea/memorarea intersectiilor
		for(int i=0;i<pol.n;i++)
		{
			ym=min(pol.m(i).vi.y,pol.m(i).vf.y);
			yM=max(pol.m(i).vi.y,pol.m(i).vf.y);
			xm=(ym==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;
			xM=(yM==pol.m(i).vi.y) ? pol.m(i).vi.x : pol.m(i).vf.x;

			if(xm==xM)
				mp=0;
			else
				mp=(ym-yM)/double(xm-xM);

			for(int j=ym;j<=yM;j++)
				if(mp==0)
					ix[j].add(xm);
				else
					ix[j].add( ((j-ym)/mp)+xm );
		}

		bool change;						//ordonarea intersectiilor
		for(int i=0;i<=n;i++)
			do
			{
				change=false;
				if(ix[i].n==0)
					break;
				for(int j=0;j<ix[i].n-1;j++)
					if(ix[i].x[j]>ix[i].x[j+1])
					{
						double aux=ix[i].x[j];
						ix[i].x[j]=ix[i].x[j+1];
						ix[i].x[j+1]=aux;
						change=true;
					}
			}while(change==true);

		for(int i=0;i<=n;i++)				//afisarea intersectii
		{
			printf("\nLinia %d: ",i);
			for(int j=0;j<ix[i].n;j++)
			{
				printf("%f, ",ix[i].x[j]);
			}
		}
		printf("\n");

		for(int i=0;i<=n;i++)				//stergerea Strict interiorului
		{
			for(int j=0;j<ix[i].n;j+=2)
			{
				if(ix[i].x[j]==ix[i].x[j+1])
					if(ix[i].n%2>0)
						j++;

				for(int t=ix[i].x[j];t<ix[i].x[j+1];t++)
					cleanPixel(t,i);
			}
		}

		int X;								//stergerea punctelor din marginea Vest plus Sud
		for(int i=0;i<=n;i++)
			for(int j=ix[i].n-1;j>=0;j--)
			{
				if(ix[i].x[j]==(int)ix[i].x[j])
				{
					X=(int)ix[i].x[j];
					cleanPixel(X,i);
				}
			}

	}

	~GrilaCarteziana(void){}

};

unsigned char prevKey;
GrilaCarteziana* gc=new GrilaCarteziana();

void Display1()		//afisare oct1 cerc
{
	gc->drawNormalCircle(10,10,8);
	gc->drawPixelCircleOct1(10,10,8);

}

void Display2()		//stergere oct1 cerc
{
	//gc->cleanNormalCircle(10,10,8);
	gc->cleanPixelCircle(10,10,8);
}

void Display3()		//afisare c3 elipsa
{
	gc->drawNormalElipse(10,10,10,5);
	gc->fillPixelElipseC3(10,10,10,5);
}

void Display4()		//stergere c3 elipsa
{
	//gc->cleanNormalElipse(10,10,10,5);
	gc->cleanPixelElipse(10,10,10,5);
}

void Display5()		//afisare poligon
{
/*6 7 1 13 5 13 11 7 7 2 9 2 3						//teste de poligon
4 3 3 10 3 10 7 3 7
7 5 3 10 7 15 5 15 15 11 10 8 18 5 13
6 3 3 10 3 10 15 7 15 7 7 3 7*/

	gc->fillPoligonS();
}

void Display6()		//stergere poligon
{
	gc->cleanPoligonS();
}

void Display7()
{
	gc->drawPixelLine(10,10,20,10);

	gc->drawPixelLine(10,10,20,15);		//q1
	gc->drawPixelLine(10,10,20,20);
	gc->drawPixelLine(10,10,15,20);

	gc->drawPixelLine(10,10,10,20);

	gc->drawPixelLine(10,10,5,20);		//q2
	gc->drawPixelLine(10,10,0,20);
	gc->drawPixelLine(10,10,0,15);

	gc->drawPixelLine(10,10,0,10);

	gc->drawPixelLine(10,10,0,5);		//q3
	gc->drawPixelLine(10,10,0,0);
	gc->drawPixelLine(10,10,5,0);

	gc->drawPixelLine(10,10,10,0);

	gc->drawPixelLine(10,10,15,0);		//q4
	gc->drawPixelLine(10,10,20,0);
	gc->drawPixelLine(10,10,20,5);
}

void Display8()
{
	gc->cleanPixelLine(10,10,20,10);

	gc->cleanPixelLine(10,10,20,15);	//q1
	gc->cleanPixelLine(10,10,20,20);
	gc->cleanPixelLine(10,10,15,20);

	gc->cleanPixelLine(10,10,10,20);

	gc->cleanPixelLine(10,10,5,20);		//q2
	gc->cleanPixelLine(10,10,0,20);
	gc->cleanPixelLine(10,10,0,15);

	gc->cleanPixelLine(10,10,0,10);

	gc->cleanPixelLine(10,10,0,5);		//q3
	gc->cleanPixelLine(10,10,0,0);
	gc->cleanPixelLine(10,10,5,0);

	gc->cleanPixelLine(10,10,10,0);

	gc->cleanPixelLine(10,10,15,0);		//q4
	gc->cleanPixelLine(10,10,20,0);
	gc->cleanPixelLine(10,10,20,5);
}

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
