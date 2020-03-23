#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <fcntl.h>
#include <pthread.h>

int receive_file(int cl,char fname[32])
	{ 
 	 int sum=0, csum=0, bytes=0, fdwr;
  	 char buffer[4];
	 bzero(buffer,sizeof(buffer));
  	 creat(fname,0777);
  	 if ((fdwr=open(fname, O_CREAT|O_WRONLY, 0777))<0) { printf("eroare la open"); return 0;}
  	 do
    		{
     		 if ((bytes=read(cl,buffer,sizeof(buffer)))<0) { printf("eroare la read"); break;}
     		 if (write(fdwr,buffer,bytes)<0) {printf("eroare la write"); break;}
     		 sum=sum+bytes;
    		}
  	 while (bytes==4); 
  	 close(fdwr);
	return sum;
	}

int send_file(int cl,char fname[32])
	{
	 int bytes=0, sum=0, fdrd;
	 if ((fdrd=open(fname,O_RDONLY))<0) {printf("eroare la open"); return 0;}
	 char buffer[4];
	 bzero(buffer,sizeof(buffer));
	 do
	    {
		if ((bytes=read(fdrd,buffer,sizeof(buffer)))<0) {printf("eroare la read"); break;}
		if (write(cl,buffer,bytes)<0) {printf("eroare la write"); break;}
		sum=sum+bytes;
		}
	 while (bytes==4);
	 close(fdrd);
	 return sum;
	}