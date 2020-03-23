#include <cstdlib>
#include "account.h"
#include "company.h"

//using namespace std;

class bank : public company{
	double curr_bal;
	account a1;
	account a2;
	account a3;
public:
	char* get_company_name();
	double get_curr_bal();
	void transfer(account a1, account a2, double amount);
	void disp_det_acc(account a1);
	void generateBalance();
};
