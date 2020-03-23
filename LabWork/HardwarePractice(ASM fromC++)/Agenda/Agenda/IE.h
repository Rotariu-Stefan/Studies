#pragma once
#include "cagenda.h"
#include "afxwin.h"


// IE dialog

class IE : public CDialogEx
{
	DECLARE_DYNAMIC(IE)

public:
	IE(CWnd* pParent = NULL);   // standard constructor
	virtual ~IE();

// Dialog Data
	enum { IDD = IDD_DIALOG4 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
	CEdit ienume;
	CEdit ieprenume;
	CEdit ieadresa;
	CEdit iegrup;
	CEdit ieid;
};
