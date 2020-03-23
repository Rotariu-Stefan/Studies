<?php

include("Login.php");

$user=$_POST['name']."\n";
$pass=$_POST['pass']."\n";

$fo=fopen("Users.txt",'r') or die("Failed to open!");

while(0==0){
	$u=fgets($fo);
	$p=fgets($fo);
	$i=fgets($fo);
	if($user==$u){
		if($pass==$p){
			echo "<p align=\"center\"> Succes!<br/> Profil: $i </p>";
			break;}
		else{
			echo "<p align=\"center\"> Wrong password! </p>";
			break;}}
	if($u==""){
		echo "<p align=\"center\"> Username not found! </p>";
		break;}}	

fclose($fo);

	
?>