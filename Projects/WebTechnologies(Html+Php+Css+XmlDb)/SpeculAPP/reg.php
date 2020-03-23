<?php

include("Register.php");

$fn=$_POST['fn'];
$ln=$_POST['ln'];
$age=$_POST['age'];
$s=$_POST['sex'];
$em=$_POST['email'];
$user=$_POST['name'];
$pass=$_POST['pass'];
$rpass=$_POST['rpass'];

if(strlen($user)<4)
{
	echo "<p class=\"window\"> Retype username! --must have at least 4 characters </p>";
}
elseif($pass!=$rpass)
{
	echo "<p class=\"window\"> Retype password! --mistype </p>";
}
elseif(strlen($pass)<4)
{
	echo "<p class=\"window\"> Retype password! --must have at least 4 characters </p>";
}
elseif($fn=="" or $ln=="")
{
	echo "<p class=\"window\"> Forgot to enter your name! </p>";
}
elseif(!($age>0 and $age<150))
{
	echo "<p class=\"window\"> Wrong age! </p>";
}
else
{
	$c=0;
	$cv=0;
	$users=simplexml_load_file('dbusers.xml');
	$status=simplexml_load_file('dbstatus.xml');
	foreach($users->user as $vsdfa)
		$c++;
	$users->addChild('user','');
	$users->user[$c]->addAttribute('uname',$user);
	$users->user[$c]->addChild('admin','0');
	$users->user[$c]->addChild('pass',$pass);
	$users->user[$c]->addChild('fname',$fn);
	$users->user[$c]->addChild('lname',$ln);
	$users->user[$c]->addChild('email',$em);
	$users->user[$c]->addChild('sex',$s);
	$users->user[$c]->addChild('age',(int)$age);
	$users->user[$c]->addChild('timeout','0');
	$users->user[$c]->addChild('total',$status->start);
	$users->user[$c]->addChild('rank','Player');
	
	foreach($status->value as $val)
	{
		$users->user[$c]->addChild('uvalue','0');
		$users->user[$c]->uvalue[$cv]->addAttribute('name',$val->name);
		$cv++;
	}
	$users->user[$c]->uvalue[0]='1000';
	$users->asXML('dbusers.xml');
	setcookie('username',$user,60*60*24+time());
	
	echo "<p class=\"window\"> Success!! </p>";
	echo "<meta http-equiv='refresh' content='1; url=Login.php'>";
	
}

?>