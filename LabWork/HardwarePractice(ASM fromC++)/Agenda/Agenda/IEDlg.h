#pragma once


// IEDlg dialog

class IEDlg : public CDialogEx
{
	DECLARE_DYNAMIC(IEDlg)

public:
	IEDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~IEDlg();

// Dialog Data
	enum { IDD = IDD_DIALOG4 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
};
