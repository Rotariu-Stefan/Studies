<div id="menu">
<h2><u>SpeculAPP</u></h2>
<?php	include('calcule.php');

if(isset($_COOKIE['username']))
{
	$users=simplexml_load_file('dbusers.xml');
	$i=GetUid($_COOKIE['username']);
	$m=$users->user[$i]->rank;
		
	echo "<strong>".$m." ".$_COOKIE['username']."</strong><br/>
		<a href=\"profile.php\" target=\"_self\"><b>|	  Profile    |</b></a>
		<a href=\"comm.php\" target=\"_self\"><b>|   Commands   |</b></a>
		<a href=\"log.php\" target=\"_self\"><b>|   Logout   |</b></a>";
}	
else
	echo "<a href=\"Home.php\" target=\"_self\"><b>|	Home |</b></a>
		<a href=\"Login.php\" target=\"_self\"><b>| Login |</b></a>
		<a href=\"Register.php\" target=\"_self\"><b>|	Register |</b></a>";
?>
</div>

<div id="left">
<pre>
<a href="Home.php" target="_self">Home</a>
<a href="http://www.infoiasi.ro/bin/Main/" target="_blank">College</a><br/><br/>
<a href="http://profs.info.uaic.ro/~busaco/teach/courses/web/projects/xml-transform.php?pag=projects" target="_blank">The Game</a>
<a href="Rules.php" target="_self">Rules</a>
<a href="Contacts.php" target="_self">Contacts</a>
<a href="feed.xml"target="_blank">Feed</a><br/><br/>
<a href="Top.php" target="_self">Top Players</a>
<a href="" target="_self">Recent History</a><br/><br/><br/><br/>
<?php 
	$s=simplexml_load_file('dbstatus.xml');
	echo "Win mark: ".$s->wmark."\n";
	echo "Loss mark: ".$s->lmark."\n";
?>
		
</pre>
</div>

<div id="right">
<h5><u>Current Status</u></h5>
<pre>

<?php

$status=simplexml_load_file('dbstatus.xml');
foreach($status->value as $val)
{
	echo "1 ".$val->name." = ".$val->curr."roni\n";
	echo "--------------\n";
}

if(isset($_COOKIE['username']))
{
	include('calcule.php');
	$users=simplexml_load_file('dbusers.xml');
	$i=GetUid($_COOKIE['username']);	

	echo "<h5><u>Your currency</u></h5>\n";
	foreach($users->user[$i]->uvalue as $uv)
		echo $uv['name'].": ".$uv."\n";
}
?>
</pre>
</div>