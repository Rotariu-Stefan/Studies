<?php

include('calcule.php');

if(!isset($_COOKIE['username']))
{
	$user=$_POST['name'];
	$pass=$_POST['pass'];

	$users=simplexml_load_file('dbusers.xml');
	$i=GetUid($user);
	$fl=0;

	foreach($users->user as $usr)
		if($usr['uname']==$user)
			if($usr->pass==$pass)
				$fl=1;
			else
				$fl=2;
	if($fl==0)
	{
		setcookie('username','');
		echo "<meta http-equiv='refresh' content='1; url=Login.php'>";
		include("Home.php");
		echo "<p class=\"window\"> Username not found </p>";
	}
	if($fl==2)
	{
		setcookie('username','');
		echo "<meta http-equiv='refresh' content='1; url=Login.php'>";
		include("Home.php");
		echo "<p class=\"window\"> Wrong password </p>";
	}
	if($fl==1)
	{
		if($users->user[$i]->timeout<=time())
		{
			ConCook($user);		
			echo "<meta http-equiv='refresh' content='1; url=profile.php'>";
			include("Home.php");
			echo "<p class=\"window\"> Login Succesfull! </p>";
		}
		else
		{
			include("Home.php");
			echo "<p class=\"window\"> You are Banned for ".$users->user[$i]->timeout." hours. </p>";
		}
	}
}
else
{
	setcookie('username','');
	echo "<meta http-equiv='refresh' content='1; url=Home.php'>";
	include("Home.php");
	echo "<p class=\"window\"> Logout Succesfull! </p>";
}
?>