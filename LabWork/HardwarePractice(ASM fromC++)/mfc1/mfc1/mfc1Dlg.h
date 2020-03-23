
// mfc1Dlg.h : header file
//

#pragma once
#include "afxwin.h"


// Cmfc1Dlg dialog
class Cmfc1Dlg : public CDialogEx
{
// Construction
public:
	Cmfc1Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MFC1_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CButton buz;
	afx_msg void OnBnClickedButton2();
	afx_msg void OnEnChangeEdit1();
	CEdit tx1;
	CEdit tx2;
	CButton sum;
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton1();
	CButton n1;
	CButton n2;
	CButton n3;
	CButton n4;
	CButton n5;
	CButton n6;
	CButton n7;
	CButton n8;
	CButton n9;
	CButton n0;
	afx_msg void OnBnClickedButton4();
	afx_msg void OnBnClickedButton5();
	afx_msg void OnBnClickedButton7();
	afx_msg void OnBnClickedButton8();
	afx_msg void OnBnClickedButton6();
	afx_msg void OnBnClickedButton9();
	afx_msg void OnBnClickedButton10();
	afx_msg void OnBnClickedButton15();
	afx_msg void OnBnClickedButton19();
	CButton n22;
	CButton egal;
	afx_msg void OnBnClickedButton16();
	CButton plus;
	afx_msg void OnBnClickedButton12();
	CButton minus;
	afx_msg void OnBnClickedButton14();
	CButton inm;
	afx_msg void OnBnClickedButton11();
	CButton imp;
	afx_msg void OnBnClickedButton13();
	CButton ce;
	afx_msg void OnBnClickedButton17();
	CEdit afis;
	CButton vg;
	afx_msg void OnBnClickedButton20();
};
