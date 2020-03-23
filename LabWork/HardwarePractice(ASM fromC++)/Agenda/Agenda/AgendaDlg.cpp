
// AgendaDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "AgendaDlg.h"
#include "afxdialogex.h"
#include "LA.h"
#include "DA.h"
#include "SA.h"
#include "IE.h"
#include "SE.h"
#include "DE.h"
#include "E.h"
#include "cagenda.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{

}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CAgendaDlg dialog




CAgendaDlg::CAgendaDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CAgendaDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CAgendaDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, list);
	DDX_Control(pDX, IDC_EDIT3, tnume);
	DDX_Control(pDX, IDC_EDIT4, tprenume);
	DDX_Control(pDX, IDC_EDIT5, tadresa);
	DDX_Control(pDX, IDC_EDIT6, tgrup);
}

BEGIN_MESSAGE_MAP(CAgendaDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON2, &CAgendaDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CAgendaDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CAgendaDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON5, &CAgendaDlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON6, &CAgendaDlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_BUTTON7, &CAgendaDlg::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON8, &CAgendaDlg::OnBnClickedButton8)
	ON_CBN_SELENDOK(IDC_COMBO1, &CAgendaDlg::OnCbnSelendokCombo1)
	ON_COMMAND(ID_AGENDACOMMANDS_LOAD, &CAgendaDlg::OnAgendacommandsLoad)
	ON_COMMAND(ID_AGENDACOMMANDS_SAVE, &CAgendaDlg::OnAgendacommandsSave)
	ON_COMMAND(ID_AGENDACOMMANDS_DELETE, &CAgendaDlg::OnAgendacommandsDelete)
	ON_COMMAND(ID_ENTRYCOMMANDS_INSERT, &CAgendaDlg::OnEntrycommandsInsert)
	ON_COMMAND(ID_ENTRYCOMMANDS_SAVE, &CAgendaDlg::OnEntrycommandsSave)
	ON_COMMAND(ID_ENTRYCOMMANDS_DELETE, &CAgendaDlg::OnEntrycommandsDelete)
	ON_COMMAND(ID_ENTRYCOMMANDS_CANCEL, &CAgendaDlg::OnEntrycommandsCancel)
	ON_COMMAND(ID_ACCELERATOR32780, &CAgendaDlg::OnAccelerator32780)
	ON_UPDATE_COMMAND_UI(ID_ACCELERATOR32780, &CAgendaDlg::OnUpdateAccelerator32780)
END_MESSAGE_MAP()


// CAgendaDlg message handlers

BOOL CAgendaDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
		accel =LoadAccelerators(AfxGetResourceHandle(),MAKEINTRESOURCE(IDR_ACCELERATOR1));
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

BOOL CAgendaDlg::PreTranslateMessage(MSG* pMsg)
{
	// utilizare acceleratori
	if(TranslateAccelerator(m_hWnd, accel, pMsg))
		return TRUE;
	return CDialog::PreTranslateMessage(pMsg);
}

void CAgendaDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CAgendaDlg::OnPaint()
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
HCURSOR CAgendaDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

//-------------------------------------------------------------------------------------
cagenda agm;
cagenda back;



void CAgendaDlg::OnBnClickedButton2()	//LA
{
	LA cLA;
	cLA.ag=agm;
	if(cLA.DoModal()==IDOK)
	{
		agm=cLA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());
	
		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	back=agm;
}


void CAgendaDlg::OnBnClickedButton3()		//SA
{
	SA cSA;
	cSA.ag=agm;
	if(cSA.DoModal()==IDOK)
	{
		agm=cSA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnBnClickedButton4()		//DA
{
	DA cDA;
	cDA.ag=agm;
	if(cDA.DoModal()==IDOK)
	{
		agm=cDA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnBnClickedButton5()		//IE
{
	IE cIE;
	cIE.ag=agm;
	if(cIE.DoModal()==IDOK)
	{
		agm=cIE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnBnClickedButton6()		//SE
{
	char c[15];
	int x=list.GetCurSel();
	list.GetLBText(x,c);

	if(c)
	{
	SE cSE;
	strcpy(cSE.index,c);
	cSE.ag=agm;
	if(cSE.DoModal()==IDOK)
	{
		agm=cSE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	}
	else
		MessageBox("Wrong Entry!!");
}


void CAgendaDlg::OnBnClickedButton7()		//DE
{
	char c[15];
	int x=list.GetCurSel();
	list.GetLBText(x,c);

	if(c)
	{
	DE cDE;
	strcpy(cDE.index,c);
	cDE.ag=agm;
	if(cDE.DoModal()==IDOK)
	{
		agm=cDE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	}
	else
		MessageBox("Wrong Entry!!");
}


void CAgendaDlg::OnBnClickedButton8()		//CE
{
	CE cCE;
	if(cCE.DoModal()==IDOK)
	{
		agm=back;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnCbnSelendokCombo1()		//Entry Combo Write
{
	char n[15];
	int x=list.GetCurSel();
	list.GetLBText(x,n);

	tnume.SetWindowTextA(agm.getintrare(n).getnume());
	tprenume.SetWindowTextA(agm.getintrare(n).getprenume());
	tadresa.SetWindowTextA(agm.getintrare(n).getadresa());
	tgrup.SetWindowTextA(agm.getintrare(n).getgrup());
}

void CAgendaDlg::OnAgendacommandsLoad()
{
	LA cLA;
	cLA.ag=agm;
	if(cLA.DoModal()==IDOK)
	{
		agm=cLA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	back=agm;
}


void CAgendaDlg::OnAgendacommandsSave()
{
	SA cSA;
	cSA.ag=agm;
	if(cSA.DoModal()==IDOK)
	{
		agm=cSA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnAgendacommandsDelete()
{
	DA cDA;
	cDA.ag=agm;
	if(cDA.DoModal()==IDOK)
	{
		agm=cDA.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}

void CAgendaDlg::OnEntrycommandsInsert()
{
	IE cIE;
	cIE.ag=agm;
	if(cIE.DoModal()==IDOK)
	{
		agm=cIE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnEntrycommandsSave()
{
	char c[15];
	int x=list.GetCurSel();
	list.GetLBText(x,c);

	if(c)
	{
	SE cSE;
	strcpy(cSE.index,c);
	cSE.ag=agm;
	if(cSE.DoModal()==IDOK)
	{
		agm=cSE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	}
	else
		MessageBox("Wrong Entry!!");
}


void CAgendaDlg::OnEntrycommandsDelete()
{
	char c[15];
	int x=list.GetCurSel();
	list.GetLBText(x,c);

	if(c)
	{
	DE cDE;
	strcpy(cDE.index,c);
	cDE.ag=agm;
	if(cDE.DoModal()==IDOK)
	{
		agm=cDE.ag;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
	}
	else
		MessageBox("Wrong Entry!!");
}


void CAgendaDlg::OnEntrycommandsCancel()
{
	CE cCE;
	if(cCE.DoModal()==IDOK)
	{
		agm=back;
		for(int i=0;i<20;i++)
			list.DeleteString(0);
		for(int i=0;i<agm.getcount();i++)
			list.AddString(agm.getintrare(i).getid());

		tnume.SetWindowTextA("");
		tprenume.SetWindowTextA("");
		tadresa.SetWindowTextA("");
		tgrup.SetWindowTextA("");
	}
}


void CAgendaDlg::OnAccelerator32780()
{
	MessageBox("AAAAAAAAAAA");
}


void CAgendaDlg::OnUpdateAccelerator32780(CCmdUI *pCmdUI)
{
	MessageBox("AAAAAAAAAAA");
}
