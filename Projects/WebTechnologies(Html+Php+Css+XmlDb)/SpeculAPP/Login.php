<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<title>Login Form</title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>

<?php	include("Antet.php");

	echo "
		<form action=\"log.php\" method=\"post\">
		<p class=\"window\">
		Username:<input type=\"text\" size=\"11\" maxlength=\"30\" name=\"name\"><br/><br/>
		Password:<input type=\"password\" size=\"11\" maxlength=\"30\" name=\"pass\"><br/><br/>
		<input type=\"submit\" value=\"Login\">
		</p>
		</form>";

?>

</body>

</html>