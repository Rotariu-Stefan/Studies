<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<title>Register Form</title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>

<?php	include("Antet.php");	?>

<form action="reg.php" method="post">
<p class="window">
First Name:<input type="text" size="11" maxlength="30" name="fn"><br/><br/>
Last Name:<input type="text" size="11" maxlength="30" name="ln"><br/><br/>
Age:<input type="text" size="11" maxlength="3" name="age"><br/><br/>
Sex:<input type="radio" name="sex" value="m" checked="yes">Male
	<input type="radio" name="sex" value="f">Female<br/><br/>

E-mail:<input type="text" size="11" maxlength="30" name="email"><br/><br/>

Username:<input type="text" size="11" maxlength="30" name="name"><br/><br/>
Password:<input type="password" size="11" maxlength="30" name="pass"><br/><br/>
Retype Password:<input type="password" size="11" maxlength="30" name="rpass"><br/><br/>

<input type="submit" value="Register">
</p>
</form>



</body>

</html>