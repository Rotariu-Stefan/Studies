#pragma once
#include "cagenda.h"

// SA dialog

class SA : public CDialogEx
{
	DECLARE_DYNAMIC(SA)

public:
	SA(CWnd* pParent = NULL);   // standard constructor
	virtual ~SA();

// Dialog Data
	enum { IDD = IDD_DIALOG3 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
};
