
// AgendaDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CAgendaDlg dialog
class CAgendaDlg : public CDialogEx
{
// Construction
public:
	CMenu menu1;
	CAgendaDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_AGENDA_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
	HACCEL accel;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton4();
	afx_msg void OnBnClickedButton5();
	afx_msg void OnBnClickedButton6();
	afx_msg void OnBnClickedButton7();
	afx_msg void OnBnClickedButton8();
	CComboBox list;
	afx_msg void OnCbnSelendokCombo1();
	CEdit tnume;
	CEdit tprenume;
	CEdit tadresa;
	CEdit tgrup;
	afx_msg void OnAgendacommandsLoad();
	afx_msg void OnAgendacommandsSave();
	afx_msg void OnAgendacommandsDelete();
	afx_msg void OnEntrycommandsInsert();
	afx_msg void OnEntrycommandsSave();
	afx_msg void OnEntrycommandsDelete();
	afx_msg void OnEntrycommandsCancel();
	afx_msg void OnAccelerator32780();
	afx_msg void OnUpdateAccelerator32780(CCmdUI *pCmdUI);
};
