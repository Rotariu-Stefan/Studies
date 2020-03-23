<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head >
<title>Home</title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>

<?php
include("Antet.php");
include("calcule.php");

$acom=$_POST['acom'];

if($acom=="rmu")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"rmu\">
		User:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}	
if($acom=="bu")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"bu\">
		User:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
		Hours:	<input type=\"text\" size=\"5\" maxlength=\"5\" name=\"hrs\">	
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}		
if($acom=="ubu")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"ubu\">
		User:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="adc")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"adc\">
	   Currency:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\"><br/>
		Rmin:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"rmin\">
		Rmax:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"rmax\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="setc")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"setc\">
	    Name:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\"><br/>
		Rmin:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"rmin\">
		Rmax:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"rmax\"><br/>
		Amin:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"amin\">
		Amax:	<input type=\"text\" size=\"6\" maxlength=\"6\" name=\"amax\"><br/>
		
		Curr:   <input type=\"text\" size=\"6\" maxlength=\"6\" name=\"curr\">
	  Seconds:	<input type=\"text\" size=\"11\" maxlength=\"10\" name=\"time\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="adt")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"adt\">
	Currency:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\"><br/>
	Seconds:	<input type=\"text\" size=\"11\" maxlength=\"10\" name=\"time\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="smark")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"smark\">
		Start Sum:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="wmark")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"wmark\">
		Win Sum:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}
if($acom=="lmark")
{
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"lmark\">
		Loss Sum:	<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\">
			<input type=\"submit\" value=\"Submit\"> </p>
			</form>";
}

$com=$_POST['com'];
if($com=="trans")
{
	$status=simplexml_load_file('dbstatus.xml');
	echo 	"<form action=\"ECommands.php\" method=\"post\"> <p class=\"window\">
			<input type=\"hidden\" name=\"cnr\" value=\"trans\">
			<select name=\"c1\">
			<option value=\"\">Start Currency...</option>";
	foreach($status->value as $val)
		echo "<option value=\"".$val->name."\">".$val->name."</option>";
		echo "</select>
			To
			<select name=\"c2\">
			<option value=\"\">Start Currency...</option>";
	foreach($status->value as $val)
		echo "<option value=\"".$val->name."\">".$val->name."</option>";
		echo "</select>
			<br/><input type=\"text\" size=\"6\" maxlenght=\"6\" name=\"nr\">
			<input type=\"submit\" value=\"Change\">
			</form>";
}
if($com=="ref")
{
	$t=time();
	$fl=0;
	$status=simplexml_load_file('dbstatus.xml');
	foreach($status->value as $val)
	{
		if($t>0)//($status->lref+($val->time)))
		{
			$fl=1;
			if(strcmp($val->name,"RON")!=0)
			{
				$c=RandFl((float)$val->rmin,(float)$val->rmax);
			//	$c = number_format($c, 2, '.', ''); 
				$val->curr=$c;
			}
		}
	}
	if($fl==1)
	{
		$status->lref=$t;
		$status->asXML('dbstatus.xml');
	}
	echo "<meta http-equiv='refresh' content='1; url=comm.php'>";
}
?>


</body>
</html>