#include <iostream> 

using namespace std; 
 
class account
{
	int accId;
	char accOwner[25];
	char accType[2];
	float balance;
	int interest;
	int calc_intr();
public:
	int get_accId();
    char* get_accOwner();
	char* get_accType();
	float get_balance();
	void upd_balance();
	void init();
	void deposit();
	void withdraw();
	void disp_det();
	account (char *acct)
	{
	*accType[2]=acct[2];
	}
};