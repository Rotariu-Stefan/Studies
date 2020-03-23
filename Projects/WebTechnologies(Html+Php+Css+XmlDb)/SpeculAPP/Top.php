<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<title>Top Players</title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>

<?php 	include("calcule.php");
		include("Antet.php");

$tusers=simplexml_load_file("dbusers.xml");
	
	foreach($tusers->user as $usr)
	{
		if(strcmp($usr->rank,'Player')==0)
			$us=(string)$usr['uname'];
		else
			$us=(string)$usr->rank." ".(string)$usr['uname'];
		$list[$us]=(string)$usr->total;
	}
	arsort($list);
	echo "<p class=\"window\">";
	foreach($list as $u => $t)
		echo $u." has ".$t." units (RONI)!<br/>";
	echo "</p>";
?>

</body>

</html>