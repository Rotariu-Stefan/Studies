#pragma once
#include "glut.h"
class GrilaCarteziana
{
public:

	static const int n=19;
	double pr;
	bool g[n+1][n+1];

	GrilaCarteziana(void)
	{
		for(int i=0;i<n;i++)
			for(int j=0;j<n;j++)
				g[i][j]=false;

		pr=0.3*(2/(double)n);
	}

	void drawGrid()
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

	void writePixel(int i,int j)
	{
		double pi = 4 * atan(1.0);

		if(!(0<=i && i<=n && 0<=j && j<=n))
			printf("Invalid coords: ( %d, %d ) !",i,j);
		else
		{
			if(g[i][j]==true)
			{
				printf("\nPixel already was drawn at ( %d, %d ).",i,j);
				return;
			}

			double cx=2*i/(double)n-1;
			double cy=2*j/(double)n-1;

			glColor3f(1,0,0);
			glBegin(GL_POLYGON);
				for(double a=2*pi;a>0;a-=0.1)
					glVertex2f(cx+pr*cos(a),cy+pr*sin(a));
			glEnd();

			g[i][j]=true;
			printf("\nPixel drawn at ( %d, %d ).",i,j);
		}
	}
	void cleanPixel(int i,int j)
	{
		double pi = 4 * atan(1.0);

		if(!(0<=i && i<=n && 0<=j && j<=n))
			printf("Invalid coords: ( %d, %d ) !",i,j);
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

	void drawLine(int x0,int y0,int xn,int yn)
	{
		printf("\nDrawing line...\n from ( %d, %d ) to ( %d, %d )",x0,y0,xn,yn);

		double m=(double)(yn-y0)/(xn-x0);
		//if(abs(m)<=1)
		{
			double k=(double)y0-m*x0;

			for(int x=x0;x<=xn;x++)
			{
				double y=m*x+k;
				int yy=floor(y+0.5);
				writePixel(x,yy);
			}
		}
		//else
			//drawLine(xn,yn,x0,y0);
	}



	void activ(int i,int j)
	{
		if(!(0<=i && i<=15 && 0<=j && j<=15))
			printf("Invalid coords !");
		else
			g[i][j]=true;
	}
	void deactiv(int i,int j)
	{
		if(!(0<=i && i<=15 && 0<=j && j<=15))
			printf("Invalid coords !");
		else
			g[i][j]=false;
	}

	~GrilaCarteziana(void)
	{
	}
};

