#include "account.h"

//member functions of account class
int account :: get_accId()
{
	return accId;
}

char* account :: get_accOwner() 
{
	return accOwner;
}

char* account :: get_accType()
{
	return accType;
}

float account :: get_balance() 
{
	return balance;
}

int account :: calc_intr()
{
	return interest * balance;
}

void account :: upd_balance()
{
	balance += calc_intr();
}

void account :: init()
{
	cout<<"New Account";
	cout<<"Name of the depositor : ";
	cin>>accOwner;
	cout<<"Account Type : (DEBIT/CREDIT) ";
	cin>>accType;
	cout<<"Amount to Deposit : ";
	cin >>balance;
}

void account :: deposit()
{
	float more;
	cout <<"Depositing";
	cout<<"Amount to deposit : ";
	cin>>more;
	balance+=more;
}

void account :: withdraw()
{
	float amt;
	cout<<"Withdrwal";
	cout<<"Amount to withdraw : ";
	cin>>amt;
	balance-=amt;
}

void account :: disp_det()
{
	cout<<"Account Details";
	cout<<"Name of the depositor : "<<accOwner<<endl;
	cout<<"Account Type          : "<<accType<<endl;
	cout<<"Balance               : $"<<balance<<endl;
}
