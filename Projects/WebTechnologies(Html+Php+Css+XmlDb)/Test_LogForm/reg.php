<?php

include("Register.php");

$fn=$_POST['fn'];
$ln=$_POST['ln'];
$s=$_POST['sex'];
$em=$_POST['email'];
$user=$_POST['name'];
$pass=$_POST['pass'];
$rpass=$_POST['rpass'];

if(strlen($user)<4){
	echo "<p align=\"center\"> Retype username! --must have at least 4 characters </p>";}
elseif($pass!=$rpass){
	echo "<p align=\"center\"> Retype password! --mistype </p>";}
elseif(strlen($pass)<4){
	echo "<p align=\"center\"> Retype password! --must have at least 4 characters </p>";}
elseif($fn=="" or $ln==""){
	echo "<p align=\"center\"> Forgot to enter your name! </p>";}
else{
	if($em==""){	$em="NOEMAIL";	}
	
	$fr=fopen("Users.txt",'a') or die("Failed to write!");
	fwrite($fr,$user."\n".$pass."\n".$fn." ".$ln." ".$s." ".$em."\n");
	fclose($fr);	
	echo "<p align=\"center\"> Register process successful </p>";}
	
	
	
	
	
?>