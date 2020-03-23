#include "bank.h"

//member functions of bank class
char* bank :: get_company_name()
{
	return company_name;
}

double bank :: get_curr_bal()
{
	//...
	return 0;
}


void bank :: transfer(account a1, account a2, double amount)
{
	a1.withdraw();
	a2.deposit();
}

void bank :: disp_det_acc (account a1)
{
	cout << "Account info: " << endl;
	cout << "Owner: " << a1.get_accOwner()[0];
	int i = 1;
	while (a1.get_accOwner()[i] != '\0'){
		cout << a1.get_accOwner()[i];
		i++;
	}
	cout << endl;
	cout << "Type: " << a1.get_accType()[0];
	i = 1;
	while (a1.get_accType()[i] != '\0') {
		cout << a1.get_accType()[i];
		i++;
	}
	cout << endl;
	cout << "Balance: " << a1.get_balance() << endl;
}

//main function 
int main () 
{	
	account obj;
	obj.init();

	bank bobj;
	company cobj;
	
	int choice = 1;
	while (choice != 0 )
	{
		cout<<"Enter 0 to exit"<<endl;
		cout<<"1. Initialize a new account"<<endl;
		cout<<"2. Deposit"<<endl;
		cout<<"3. Withdraw"<<endl;
		cout<<"4. See A/c Status"<<endl;
		cout<<"5. See Account Info"<<endl;
		cin>>choice;
	switch(choice)
	{
		case 0 :obj.disp_det();
			cout<<"EXIT";
			break;
		case 1 : obj.init();
			break;
		case 2: obj.deposit();
			break;
		case 3 : obj.withdraw();
			break;
		case 4: obj.disp_det();
			break;
		case 5: bobj.disp_det_acc(obj);
			break;
		default: cout<<"Invalid Choice"<<endl;
	}
}

}

//END C++ CODE