#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <string.h>
#include <fstream>

#include "glut.h"

#include <assert.h>
#include <float.h>

// dimensiunea ferestrei in pixeli
#define dim 300

unsigned char prevKey;
int nivel = 0;


class C2coord
{
public:
  C2coord() 
  {
    m.x = m.y = 0;
  }

  C2coord(double x, double y) 
  {
    m.x = x;
    m.y = y;
  }

  C2coord(C2coord &p) 
  {
    m.x = p.m.x;
    m.y = p.m.y;
  }

  C2coord &operator=(C2coord &p)
  {
    m.x = p.m.x;
    m.y = p.m.y;
    return *this;
  }

  int operator==(C2coord &p)
  {
    return ((m.x == p.m.x) && (m.y == p.m.y));
  }

//protected:
  struct SDate
  {
    double x,y;
  } m;
};

class CPunct : public C2coord
{
public:
  CPunct() : C2coord(0.0, 0.0)
  {}

  CPunct(double x, double y) : C2coord(x, y)
  {}

  CPunct &operator=(CPunct &p)
  {
    m.x = p.m.x;
    m.y = p.m.y;
    return *this;
  }

  void getxy(double &x, double &y)
  {
    x = m.x;
    y = m.y;
  }

  int operator==(CPunct &p)
  {
    return ((m.x == p.m.x) && (m.y == p.m.y));
  }

  void marcheaza()
  {
    glBegin(GL_POINTS);
      glVertex2d(m.x, m.y);
    glEnd();
  }

  void print(FILE *fis)
  {
    fprintf(fis, "(%+f,%+f)", m.x, m.y);
  }

};

class CVector : public C2coord
{
public:
  CVector() : C2coord(0.0, 0.0)
  {
    normalizare();
  }

  CVector(double x, double y) : C2coord(x, y)
  {
    normalizare();
  }

  CVector &operator=(CVector &p)
  {
    m.x = p.m.x;
    m.y = p.m.y;
    return *this;
  }

  int operator==(CVector &p)
  {
    return ((m.x == p.m.x) && (m.y == p.m.y));
  }

  CPunct getDest(CPunct &orig, double lungime)
  {
    double x, y;
    orig.getxy(x, y);
    CPunct p(x + m.x * lungime, y + m.y * lungime);
    return p;
  }

  void rotatie(double grade)
  {
    double x = m.x;
    double y = m.y;
    double t = 2 * (4.0 * atan((double)1)) * grade / 360.0;
    m.x = x * cos(t) - y * sin(t);
    m.y = x * sin(t) + y * cos(t);
    normalizare();
  }

  void deseneaza(CPunct p, double lungime) 
  {
    double x, y;
    p.getxy(x, y);
    glColor3f(1.0, 0.1, 0.1);
    glBegin(GL_LINE_STRIP);
      glVertex2d(x, y);
      glVertex2d(x + m.x * lungime, y + m.y * lungime);
    glEnd();
  }

  void print(FILE *fis)
  {
    fprintf(fis, "%+fi %+fj", C2coord::m.x, C2coord::m.y);
  }

private:
  void normalizare()
  {
    double d = sqrt(C2coord::m.x * C2coord::m.x + C2coord::m.y * C2coord::m.y);
    if (d != 0.0) 
    {
      C2coord::m.x = C2coord::m.x * 1.0 / d;
      C2coord::m.y = C2coord::m.y * 1.0 / d;
    }
  }
};

class CCurbaKoch
{
public:
  void segmentKoch(double lungime, int nivel, CPunct &p, CVector v)
  {
    CPunct p1;
    if (nivel == 0) 
    {
      v.deseneaza(p, lungime);
    }
    else
    {
  //    v.print(stderr);
  //    fprintf(stderr, "\n");
      segmentKoch(lungime / 3.0, nivel - 1, p, v);
      p1 = v.getDest(p, lungime / 3.0);
      v.rotatie(60);
  //    v.print(stderr);
  //    fprintf(stderr, "\n");
      segmentKoch(lungime / 3.0, nivel - 1, p1, v);
      p1 = v.getDest(p1, lungime / 3.0);
      v.rotatie(-120);
  //    v.print(stderr);
  //    fprintf(stderr, "\n");
      segmentKoch(lungime / 3.0, nivel - 1, p1, v);
      p1 = v.getDest(p1, lungime / 3.0);
      v.rotatie(60);
  //    v.print(stderr);
  //    fprintf(stderr, "\n");
      segmentKoch(lungime / 3.0, nivel - 1, p1, v);
    }
  }

  void afisare(double lungime, int nivel)
  {
    CVector v1(sqrt(3.0)/2.0, 0.5);
    CPunct p1(-1.0, 0.0);

    CVector v2(0.0, -1.0);
    CPunct p2(0.5, sqrt(3.0)/2.0);

    CVector v3(-sqrt(3.0)/2.0, 0.5);
    CPunct p3(0.5, -sqrt(3.0)/2.0);

    segmentKoch(lungime, nivel, p1, v1);
    segmentKoch(lungime, nivel, p2, v2);
    segmentKoch(lungime, nivel, p3, v3);
  }
};

class CArboreBinar
{
public:
  void arboreBinar(double lungime, int nivel, CPunct &p, CVector v)
  {
    CPunct p1;
    if (nivel == 0) 
    {
      v.deseneaza(p, lungime);
    }
    else
    {
      arboreBinar(lungime, nivel - 1, p, v);
      p1 = v.getDest(p, lungime);

      v.rotatie(-45);
      arboreBinar(lungime / 2.0, nivel - 1, p1, v);

      v.rotatie(90);
      arboreBinar(lungime / 2.0, nivel - 1, p1, v);
    }
  }

  void afisare(double lungime, int nivel)
  {
    CVector v(0.0, -1.0);
    CPunct p(0.0, 1.0);

    arboreBinar(lungime, nivel, p, v);
  }
};

class CArborePerron
{
public:
  void arborePerron(double lungime, 
                    int nivel, 
                    double factordiviziune, 
                    CPunct p, 
                    CVector v)
  {
    assert(factordiviziune != 0);
    CPunct p1, p2;
    if (nivel == 0) 
    {
    }
    else
    {
      v.rotatie(30);
      v.deseneaza(p, lungime);
      p1 = v.getDest(p, lungime);
      arborePerron(lungime * factordiviziune, nivel - 1, factordiviziune, p1, v);

      v.rotatie(-90);
      v.deseneaza(p, lungime);
      p1 = v.getDest(p, lungime);
      p2 = p1;

      v.rotatie(-30);
      v.deseneaza(p1, lungime);
      p1 = v.getDest(p1, lungime);
      arborePerron(lungime * factordiviziune, nivel - 1, factordiviziune, p1, v);

      p1 = p2;
      v.rotatie(90);
      v.deseneaza(p1, lungime);
      p1 = v.getDest(p1, lungime);
      p2 = p1;

      v.rotatie(30);
      v.deseneaza(p1, lungime);
      p1 = v.getDest(p1, lungime);
      arborePerron(lungime * factordiviziune, nivel - 1, factordiviziune, p1, v);

      p1 = p2;
      v.rotatie(-90);
      v.deseneaza(p1, lungime);
      p1 = v.getDest(p1, lungime);
      arborePerron(lungime * factordiviziune, nivel - 1, factordiviziune, p1, v);
    }
  }

  void afisare(double lungime, int nivel)
  {
    CVector v(0.0, 1.0);
    CPunct p(0.0, -1.0);

    v.deseneaza(p, 0.25);
    p = v.getDest(p, 0.25);
    arborePerron(lungime, nivel, 0.4, p, v);
  }
};

class CCurbaHilbert
{
public:
  void curbaHilbert(double lungime, int nivel, CPunct &p, CVector &v, int d)
  {
    if (nivel == 0) 
    {
    }
    else
    {
      v.rotatie(d * 90);
      curbaHilbert(lungime, nivel - 1, p, v, -d);

      v.deseneaza(p, lungime);
      p = v.getDest(p, lungime);

      v.rotatie(-d * 90);
      curbaHilbert(lungime, nivel - 1, p, v, d);

      v.deseneaza(p, lungime);
      p = v.getDest(p, lungime);

      curbaHilbert(lungime, nivel - 1, p, v, d);

      v.rotatie(-d * 90);
      v.deseneaza(p, lungime);
      p = v.getDest(p, lungime);
      
      curbaHilbert(lungime, nivel - 1, p, v, -d);

      v.rotatie(d * 90);
    }
  }

  void afisare(double lungime, int nivel)
  {
    CVector v(0.0, 1.0);
    CPunct p(0.0, 0.0);

    curbaHilbert(lungime, nivel, p, v, 1);
  }
};

class CImg1
{
public:
  void Im1(double lungime, int nivel, CPunct &p, CVector v)
  {
    CPunct p1;
    if (nivel == 0) 
    {
    }
    else
    {

      v.rotatie(-45);
	  v.deseneaza(p,lungime);
	  p1 = v.getDest(p, lungime);
      Im1(lungime/3, nivel - 1, p1, v);

      v.rotatie(90);
      v.deseneaza(p,lungime);
	  p=v.getDest(p,lungime);

	  v.rotatie(10);
	  v.deseneaza(p,lungime);
	  p1 = v.getDest(p, lungime);
	  Im1(lungime/3, nivel - 1, p1, v);

	  v.rotatie(-55);
	  v.deseneaza(p,lungime);
	  p=v.getDest(p,lungime);

	  v.rotatie(30);
	  v.deseneaza(p,lungime/2);
	  p1 = v.getDest(p, lungime/2);
	  Im1(lungime/3, nivel - 1, p1, v);

	  v.rotatie(-120);
	  v.deseneaza(p,lungime/2);
	  p1 = v.getDest(p, lungime/2);
	  Im1(lungime/3, nivel - 1, p1, v);
	}
  }

  void init(double lungime,CPunct p,CVector v)
  {
	  v.rotatie(180);
	  v.deseneaza(p,lungime/4);
  }

  void afisare(double lungime, int nivel)
  {
    CVector v(0.0, -1.0);
    CPunct p(-0.3, 0.25);

	init(lungime,p,v);
    Im1(lungime, nivel, p, v);
  }
};

class CImg2
{
public:

	int x;

  void Im2(double lungime, int nivel, CPunct p, CVector v)
  {
	  if(nivel==0)
		  v.deseneaza(p,lungime);
	  else
	  {

		  CPunct p1=v.getDest(p,lungime);
		  CVector v1(v);
		  v1.rotatie(-120);

		  CPunct p2=v1.getDest(p1,lungime/2);
		  CVector v2(v);
		  v2.rotatie(180);
		  p2=v2.getDest(p2,lungime/2);
		  v2.rotatie(180);

		  CVector v3(v);
		  v3.rotatie(120);

		  Im2(lungime/2, nivel-1, p1, v1);
		  Im2(lungime/2, nivel-1, p2, v2);
		  Im2(lungime/2, nivel-1, p2, v3);

	  }
  }

  void afisare(double lungime, int nivel)
  {
	CPunct p(-0.8,-0.8);
	CVector v(0,1);

	Im2(lungime, nivel, p, v);
  }
};

class CImg3
{
public:

	int x;

  void Im3(double lungime, int nivel, CPunct p)
  {
	  double x,y;
	  p.getxy(x,y);

	  CPunct p1(x-lungime/2,y+lungime/2);
	  CVector v(1,0);
	  v.deseneaza(p1,lungime);

	  p1=CPunct(x+lungime/2,y+lungime/2);
	  v=CVector(0,-1);
	  v.deseneaza(p1,lungime);

	  p1=CPunct(x+lungime/2,y-lungime/2);
	  v=CVector(-1,0);
	  v.deseneaza(p1,lungime);

	  p1=CPunct(x-lungime/2,y-lungime/2);
	  v=CVector(0,1);
	  v.deseneaza(p1,lungime);
	  
	  if(nivel!=0)
	  {
		  Im3(lungime/3,nivel-1,CPunct(x-lungime,y));
		  Im3(lungime/3,nivel-1,CPunct(x-lungime,y-lungime));
		  Im3(lungime/3,nivel-1,CPunct(x-lungime,y+lungime));
		  Im3(lungime/3,nivel-1,CPunct(x+lungime,y));
		  Im3(lungime/3,nivel-1,CPunct(x+lungime,y-lungime));
		  Im3(lungime/3,nivel-1,CPunct(x+lungime,y+lungime));
		  Im3(lungime/3,nivel-1,CPunct(x,y-lungime));
		  Im3(lungime/3,nivel-1,CPunct(x,y+lungime));

	  }
		  
  }

  void afisare(double lungime, int nivel)
  {
	CPunct p(0,0);

	Im3(lungime*3, 0, p);
	Im3(lungime, nivel, p);
  }
};



// afisare curba lui Koch "fulg de zapada"
void Display1() {
  CCurbaKoch cck;
  cck.afisare(sqrt(3.0), nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'c');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'u');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'u');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'K');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'o');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'c');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'h');

  nivel++;
}

// afisare arbore binar
void Display2() {
  CArboreBinar cab;
  cab.afisare(1, nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'o');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');

  nivel++;
}

// afisare arborele lui Perron
void Display3() {
  CArborePerron cap;

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,-0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'o');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'P');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'o');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');

  glPushMatrix();
  glLoadIdentity();
  glScaled(0.4, 0.4, 1);
  glTranslated(-0.5, -0.5, 0.0);
  cap.afisare(1, nivel);
  glPopMatrix();
  nivel++;
}

// afisare curba lui Hilbert
void Display4() {
  CCurbaHilbert cch;
  cch.afisare(0.05, nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,-0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'c');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'u');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'H');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 't');

  nivel++;
}

// afisare Imaginea 3
void Display7() {
  CImg2 im2;
  im2.afisare(1.6, nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'I');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'm');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'g');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '3');

  nivel++;
}

// afisare Imaginea 2
void Display6() {
  CImg1 im1;
  im1.afisare(0.4, nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'I');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'm');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'g');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '2');

  nivel++;
}

// afisare Imaginea 1
void Display5() {
  CImg3 im3;
  im3.afisare(0.35, nivel);

  char c[3];
  sprintf(c, "%2d", nivel);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'N');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'v');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '=');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, c[1]);

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'I');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'm');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'g');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '1');

  nivel++;
}

class Mandelbrot
{
public:

	double cx, cy;
	double zx, zy;
	int n;

	int ism;
	int iter;

	Mandelbrot(double px,double py,int pn)
	{
		cx=px;
		cy=py;
		zx=0;
		zy=0;
		n=pn;

		//printf("X= %.3f, Y= %.3f\n",0, 0);
		ism=0;
	}

	double modz()
	{
		return sqrt(zx*zx+zy*zy);
	}

	void step()
	{
		double xx=zx;
		double yy=zy;

		zx=(xx*xx-yy*yy) + cx;
		zy=(2*xx*yy) + cy;

		//printf("X= %.3f, Y= %.3f",zx, zy);
		//printf("\t|z|= %.3f\n", modz());
	}

	void trym()
	{
		double tx,ty;

		for(int i=0;i<n;i++)
		{
			tx=zx;
			ty=zy;

			step();

			if(modz()>2)
			{
				ism=0;
				iter=i;
				return;
			}
			if(tx==zx && ty==zy)
				break;
		}
		ism=1;
	}

	void draw()
	{
		glBegin(GL_POINTS);
			glVertex2d(cx/2, cy/2);
		glEnd();

	}

};

//afisare Mandelbrot
void Display8() {

	double x;
	double y;	
	Mandelbrot man(0,0,1);

	glBegin(GL_POINTS);
	for(x=-2;x<2;x+=0.01)
		for(y=-2;y<2;y+=0.01)
		{
			man=Mandelbrot(x,y,100);
			man.trym();
			if(man.ism==0)
			{
				if(man.iter<2)
					glColor3f(0,0,0);
				else if(man.iter<3)
					glColor3f(0.1,0,0);
				else if(man.iter<5)
					glColor3f(0.2,0,0);
				else if(man.iter<10)
					glColor3f(0.4,0,0);
				else if(man.iter<15)
					glColor3f(0.6,0,0);
				else if(man.iter<20)
					glColor3f(0.8,0,0);

				glVertex2d(x/2, y/2);
			}
			else
			{
				glColor3f(1,0.5,0.5);
				glVertex2d(x/2, y/2);
			}
		}
	glEnd();

	/*
  char cx[3];
  sprintf(cx, "%2d", x);
  char cy[3];
  sprintf(cy, "%2d", y);
  glRasterPos2d(-0.98,-0.98);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '(');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, cx[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, cx[1]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ',');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, cy[0]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, cy[1]);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ')');
  */

  glRasterPos2d(-1.0,0.9);
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'M');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'u');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'l');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 't');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'i');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'm');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, ' ');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'M');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'a');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'n');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'd');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'e');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, '1');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'b');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'r');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 'o');
  glutBitmapCharacter(GLUT_BITMAP_9_BY_15, 't');

}

void Init(void) {

   glClearColor(1.0,1.0,1.0,1.0);

   glLineWidth(1);

   glPointSize(3);

   glPolygonMode(GL_FRONT, GL_LINE);
}

void Display(void) 
{
  switch(prevKey) 
  {
    case '0':
      glClear(GL_COLOR_BUFFER_BIT);
      nivel = 0;
      fprintf(stderr, "nivel = %d\n", nivel);
      break;
    case '1':
      glClear(GL_COLOR_BUFFER_BIT);
      Display1();
      break;
    case '2':
      glClear(GL_COLOR_BUFFER_BIT);
      Display2();
      break;
    case '3':
      glClear(GL_COLOR_BUFFER_BIT);
      Display3();
      break;
    case '4':
      glClear(GL_COLOR_BUFFER_BIT);
      Display4();
      break;
    case '5':
      glClear(GL_COLOR_BUFFER_BIT);
      Display5();
      break;
    case '6':
      glClear(GL_COLOR_BUFFER_BIT);
      Display6();
      break;
    case '7':
      glClear(GL_COLOR_BUFFER_BIT);
      Display7();
      break;
    case '8':
      glClear(GL_COLOR_BUFFER_BIT);
      Display8();
      break;
    default:
      break;
  }

  glFlush();
}

void Reshape(int w, int h) 
{
   glViewport(0, 0, (GLsizei) w, (GLsizei) h);
}

void KeyboardFunc(unsigned char key, int x, int y) 
{
   prevKey = key;
   if (key == 27) // escape
      exit(0);
   glutPostRedisplay();
}

void MouseFunc(int button, int state, int x, int y) 
{
}

int main(int argc, char** argv) 
{
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


