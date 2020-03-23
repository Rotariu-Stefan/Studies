<?php

function GetUid($user)
{
	$users=simplexml_load_file('dbusers.xml');
	$index=0;
	$i=-1;
	foreach($users->user as $usr)
	{
		if(strcmp($usr['uname'],$user)==0)
			$i=$index;
		$index++;
	}
	return $i;
}
function GetVid($cr)
{
	$status=simplexml_load_file('dbstatus.xml');
	$index=0;
	$i=-1;
	foreach($status->value as $val)
	{
		if(strcmp($val->name,$cr)==0)
			$i=$index;
		$index++;
	}
	return $i;
}
function TotalSum($user)
{
	$users=simplexml_load_file('dbusers.xml');
	$status=simplexml_load_file('dbstatus.xml');
	$i=GetUid($user);	
	$s=0;
	if($i>=0)
	{
		foreach($status->value as $val)
			foreach($users->user[$i]->uvalue as $uval)
				if(strcmp($val->name,$uval['name'])==0)
					$s=$s+(($val->curr)*$uval);
	}
	else
		echo "Wrong User!A";
		
	return $s;
}
function ConCook($user)
{
	$users=simplexml_load_file('dbusers.xml');
	$i=GetUid($user);
	
	setcookie('username',$user,60*60*24+time());
	setcookie('admin',$users->user[$i]->admin,60*60*24+time());
}
function RandFl($min,$max)
{
	$min=100*$min;
	$max=100*$max;
	$r=mt_rand($min,$max);
	$r=(float)$r/100;
    return $r;
}
	
?>