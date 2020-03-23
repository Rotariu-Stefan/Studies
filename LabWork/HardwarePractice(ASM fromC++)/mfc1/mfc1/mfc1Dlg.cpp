
// mfc1Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "mfc1.h"
#include "mfc1Dlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// Cmfc1Dlg dialog




Cmfc1Dlg::Cmfc1Dlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(Cmfc1Dlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void Cmfc1Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT2, tx1);
	DDX_Control(pDX, IDC_EDIT3, tx2);
	DDX_Control(pDX, IDC_BUTTON3, sum);
	DDX_Control(pDX, IDC_BUTTON1, n1);
	DDX_Control(pDX, IDC_BUTTON4, n3);
	DDX_Control(pDX, IDC_BUTTON5, n4);
	DDX_Control(pDX, IDC_BUTTON7, n5);
	DDX_Control(pDX, IDC_BUTTON8, n6);
	DDX_Control(pDX, IDC_BUTTON6, n7);
	DDX_Control(pDX, IDC_BUTTON9, n8);
	DDX_Control(pDX, IDC_BUTTON10, n9);
	DDX_Control(pDX, IDC_BUTTON15, n0);
	DDX_Control(pDX, IDC_BUTTON19, n22);
	DDX_Control(pDX, IDC_BUTTON16, egal);
	DDX_Control(pDX, IDC_BUTTON12, plus);
	DDX_Control(pDX, IDC_BUTTON14, minus);
	DDX_Control(pDX, IDC_BUTTON11, inm);
	DDX_Control(pDX, IDC_BUTTON13, imp);
	DDX_Control(pDX, IDC_BUTTON17, ce);
	DDX_Control(pDX, IDC_EDIT1, afis);
	DDX_Control(pDX, IDC_BUTTON20, vg);
}

BEGIN_MESSAGE_MAP(Cmfc1Dlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON3, &Cmfc1Dlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON1, &Cmfc1Dlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON4, &Cmfc1Dlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON5, &Cmfc1Dlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON7, &Cmfc1Dlg::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON8, &Cmfc1Dlg::OnBnClickedButton8)
	ON_BN_CLICKED(IDC_BUTTON6, &Cmfc1Dlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_BUTTON9, &Cmfc1Dlg::OnBnClickedButton9)
	ON_BN_CLICKED(IDC_BUTTON10, &Cmfc1Dlg::OnBnClickedButton10)
	ON_BN_CLICKED(IDC_BUTTON15, &Cmfc1Dlg::OnBnClickedButton15)
	ON_BN_CLICKED(IDC_BUTTON19, &Cmfc1Dlg::OnBnClickedButton19)
	ON_BN_CLICKED(IDC_BUTTON16, &Cmfc1Dlg::OnBnClickedButton16)
	ON_BN_CLICKED(IDC_BUTTON12, &Cmfc1Dlg::OnBnClickedButton12)
	ON_BN_CLICKED(IDC_BUTTON14, &Cmfc1Dlg::OnBnClickedButton14)
	ON_BN_CLICKED(IDC_BUTTON11, &Cmfc1Dlg::OnBnClickedButton11)
	ON_BN_CLICKED(IDC_BUTTON13, &Cmfc1Dlg::OnBnClickedButton13)
	ON_BN_CLICKED(IDC_BUTTON17, &Cmfc1Dlg::OnBnClickedButton17)
	ON_BN_CLICKED(IDC_BUTTON20, &Cmfc1Dlg::OnBnClickedButton20)
END_MESSAGE_MAP()


// Cmfc1Dlg message handlers

BOOL Cmfc1Dlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void Cmfc1Dlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR Cmfc1Dlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


//{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialogEx::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
//}

float x=0,y=0;
int op=0;
float v=0;
float r;

void Cmfc1Dlg::OnBnClickedButton3()
{
	char n1[10],n2[10],result[100];
	tx1.GetWindowTextA(n1,10);
	tx2.GetWindowTextA(n2,10);
	int rez=atoi(n1)+atoi(n2);
	sprintf(result,"Suma este: %d",rez);
	MessageBox(result);
}

/*void Cmfc1Dlg::afisr()
{
	char af[30],afr[30];
	itoa(x,af,10);
	strcat(afr,af);
	if(op==1)
		strcat(afr,"+");
	itoa(y,af,10);
	strcat(afr,af);
	afis.SetWindowTextA(afr);
}*/
void Cmfc1Dlg::OnBnClickedButton20()
{
	if(v==0)
		v=1;
}
float operr(float a)
{
	if(op!=0)
	{
		if(v==0)
			y=y*10+a;
		else
		{
			y=y+(a/(10*v));
			v++;
		}
		return y;
	}
	else
	{
		if(v==0)
			x=x*10+a;
		else
		{
			x=x+(a/(10*v));
			v++;
		}
		return x;
	}
}

void Cmfc1Dlg::OnBnClickedButton1()
{
	float t = operr(1);
	char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton19()
{
	float t=operr(2);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton4()
{
	float t=operr(3);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton5()
{
	float t=operr(4);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton7()
{
	float t=operr(5);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton8()
{
	float t=operr(6);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton6()
{
	float t=operr(7);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton9()
{
	float t=operr(8);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton10()
{
	float t=operr(9);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton15()
{
	float t=operr(0);
		char temp[128];
	sprintf(temp,"%f",t);
	afis.SetWindowText(temp);
}
void Cmfc1Dlg::OnBnClickedButton12()
{
	op=1;
	v=0;
}
void Cmfc1Dlg::OnBnClickedButton14()
{
	op=2;
	v=0;
}
void Cmfc1Dlg::OnBnClickedButton11()
{
	op=3;
	v=0;
}
void Cmfc1Dlg::OnBnClickedButton13()
{
	op=4;
	v=0;
}


void Cmfc1Dlg::OnBnClickedButton17()
{
	x=0,y=0;
	op=0;
	r=0;
	v=0;
}



void Cmfc1Dlg::OnBnClickedButton16()
{
	char da[100];
	if(op==1)
		r=x+y;
	if(op==2)
		r=x-y;
	if(op==3)
		r=x*y;
	if(op==4)
		if(y!=0)
			r=x/y;
		else
			r=x;
	if(op==0)
		r=x;
	sprintf(da,"Rezultatul este: %f",r);
	afis.SetWindowTextA(da);
	v=0;
}


