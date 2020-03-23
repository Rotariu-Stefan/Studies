<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head >
<title>Home</title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>
<?php include("Antet.php"); ?>

<br/>
<br/>

<div class="comm">
<form action="Commands.php" method="post">
<b>Commands: </b><br/>
		<input type="radio" name="com" value="trans" checked="yes">Transfer
		<input type="radio" name="com" value="ref">Refresh<br/><br/>
<input type="submit" value="Do!">
</form>
</div>

<?php

if($_COOKIE['admin']==1)
	include("acomm.php");
?>

</body>

</html>
