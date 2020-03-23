#pragma once
#include "cagenda.h"


// CE dialog

class CE : public CDialogEx
{
	DECLARE_DYNAMIC(CE)

public:
	CE(CWnd* pParent = NULL);   // standard constructor
	virtual ~CE();

// Dialog Data
	enum { IDD = IDD_DIALOG7 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
};
