#pragma once
#include "cagenda.h"


// DA dialog

class DA : public CDialogEx
{
	DECLARE_DYNAMIC(DA)

public:
	DA(CWnd* pParent = NULL);   // standard constructor
	virtual ~DA();

// Dialog Data
	enum { IDD = IDD_DIALOG2 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
};
