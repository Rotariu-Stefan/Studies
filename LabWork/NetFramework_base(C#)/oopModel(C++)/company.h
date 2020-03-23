#include <iostream>

class company {
public:
	char company_name[20];
	char manager_name[20];

//	virtual char* get_name() = 0;
	char* get_company_name();
	char* get_manager_name();
};