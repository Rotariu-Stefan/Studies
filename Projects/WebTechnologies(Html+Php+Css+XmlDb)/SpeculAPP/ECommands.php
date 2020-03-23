<?php
include('calcule.php');

$cnr=$_POST['cnr'];

if($cnr=="rmu")
{
	$user=$_POST['name'];
	
	$fl=GetUid($user);
	$users=simplexml_load_file('dbusers.xml');
	unset($users->user[$fl]);
	
	if($fl==-1)
		echo "<p class=\"window\">Wrong username!!</p>";
	else
		$users->asXML('dbusers.xml');
}

if($cnr=="bu")
{
	$user=$_POST['name'];
	$hours=$_POST['hrs'];
	
	$fl=GetUid($user);
	$users=simplexml_load_file('dbusers.xml');
	$users->user[$fl]->timeout=$hours*60*60+time();
	
	if($fl==-1)
		echo "<p class=\"window\">Wrong username!!</p>";
	else
		$users->asXML('dbusers.xml');
}

if($cnr=="ubu")
{
	$user=$_POST['name'];
	
	$fl=GetUid($user);
	$users=simplexml_load_file('dbusers.xml');
	$users->user[$fl]->timeout=0;
	
	if($fl==-1)
		echo "<p class=\"window\">Wrong username!!</p>";
	else
		$users->asXML('dbusers.xml');
}

if($cnr=="smark")
{
	$sm=$_POST['name'];
	$status=simplexml_load_file('dbstatus.xml');
	$status->start=$sm;
	$status->asXML('dbstatus.xml');
}
if($cnr=="wmark")
{
	$wm=$_POST['name'];
	$status=simplexml_load_file('dbstatus.xml');
	$status->wmark=$wm;
	$status->asXML('dbstatus.xml');
}
if($cnr=="lmark")
{
	$lm=$_POST['name'];
	$status=simplexml_load_file('dbstatus.xml');
	$status->lmark=$lm;
	$status->asXML('dbstatus.xml');
}

if($cnr=="setc")
{
	$fl=GetVid($cr);
	$status=simplexml_load_file('dbstatus.xml');

	$cr=$_POST['name'];
	$rmin=$_POST['rmin'];
	$rmax=$_POST['rmax'];
	$amin=$_POST['amin'];
	$amax=$_POST['amax'];
	$time=$_POST['time'];
	$curr=$_POST['curr'];
	
	if($fl==-1)
	{
		$i=0;
		foreach($status->value as $dafsf)
			$i++;
		
		$status->addChild('value','');
		$status->value[$i]->addChild('name',$cr);
		$status->value[$i]->name->addAttribute('full',$cr);
		$status->value[$i]->addChild('rmin',$rmin);
		$status->value[$i]->addChild('rmax',$rmax);
		$status->value[$i]->addChild('amin',$amin);
		$status->value[$i]->addChild('amax',$amax);
		$status->value[$i]->addChild('time',$time);
		$status->value[$i]->addChild('curr',$curr);
		$status->asXML('dbstatus.xml');
		
		$users=simplexml_load_file('dbusers.xml');
		foreach($users->user as $us)
		{
			$us->addChild('uvalue','0.00');
			$us->uvalue[$i]->addAttribute('name',$cr);
		}
		$users->asXML('dbusers.xml');
	}
	else
	{
		$status->value[$fl]->rmin=$rmin;
		$status->value[$fl]->rmax=$rmax;
		$status->value[$fl]->amin=$amin;
		$status->value[$fl]->amax=$amax;
		$status->value[$f1]->curr=$curr;
		$status->value[$fl]->time=$time;
		
		$status->asXML('dbstatus.xml');
	}
}

if($cnr=="adc")
{
	$cr=$_POST['name'];
	$rmin=$_POST['rmin'];
	$rmax=$_POST['rmax'];
	
	$fl=GetVid($cr);	
	$status=simplexml_load_file('dbstatus.xml');
	
	if($status->value[$fl]->amin>$rmin or $status->value[$fl]->amax<$rmax)
		echo "Out of limit!";
	else
	{
		$status->value[$fl]->rmin=$rmin;
		$status->value[$fl]->rmax=$rmax;
	
		if($fl==-1)
			echo "<p class=\"window\">Wrong value!!</p>";
		else
			$status->asXML('dbstatus.xml');
	}
}

if($cnr=="adt")
{
	$cr=$_POST['name'];
	$time=$_POST['time'];
	
	$fl=GetVid($cr);
	$status=simplexml_load_file('dbstatus.xml');
	$status->value[$fl]->time=$time;
	
	if($fl==-1)
		echo "<p class=\"window\">Wrong value!!</p>";
	else
		$status->asXML('dbstatus.xml');
}

if($cnr=="trans")
{
	$c1=$_POST['c1'];
	$c2=$_POST['c2'];
	$nr=$_POST['nr'];
	
	$fl1=GetVid($c1);
	$fl2=GetVid($c2);
	
	if(!($fl1>-1 AND $fl2>-1))
		echo "<p class=\"window\">Wrong value!!</p>";
	elseif($fl1!=$fl2)
	{
		$status=simplexml_load_file('dbstatus.xml');
		$users=simplexml_load_file('dbusers.xml');
		$i=GetUid($_COOKIE['username']);
		
		$cr1=(float)$status->value[$fl1]->curr;
		$cr2=(float)$status->value[$fl2]->curr;
		if($nr<$users->user[$i]->total and $nr>0)
		{
		$x=$users->user[$i]->uvalue[$fl1];
		$y=$users->user[$i]->uvalue[$fl2];
		
		$cc1=((float)$x)-$nr;
		$cc1 = number_format($cc1, 2, '.', '');
		$cc2=((float)$y)+(($nr*$cr1)/$cr2);
		$cc2 = number_format($cc2, 2, '.', '');
		

		$users->user[$i]->uvalue[$fl1]=$cc1;
		$users->user[$i]->uvalue[$fl2]=$cc2;
		$users->asXML('dbusers.xml');
		$stat=TotalSum($_COOKIE['username']);
		$users->user[$i]->total=$stat;
		$users->asXML('dbusers.xml');
		
		
		if($users->user[$i]->rank=='Player')
		{
			if($stat>=$status->wmark)
			{
				$users->user[$i]->rank="Champion";
				$users->asXML('dbusers.xml');
				echo "<script>alert(\"Congradulations, you've won!\");</script>";
			}
			if($stat<=$status->lmark)
			{
				$users->user[$i]->rank="Loser";
				$users->asXML('dbusers.xml');
				echo "<script>alert(\"You lost, better luck next time!.\");</script>";
			}
		}}
		else
			echo "<p class=\"window\">Wrong sum!!</p>";
		
	}
}
	echo "<meta http-equiv='refresh' content='1; url=comm.php'>";


?>
