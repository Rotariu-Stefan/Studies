<html>

<head>
<title>Register Form</title>
</head>

<body>

<?php	include("Antet.php");	?>

<form action="reg.php" method="post">
<p align="center">
First Name:<input type="text" size="11" maxlength="30" name="fn"><br/><br/>
Last Name:<input type="text" size="11" maxlength="30" name="ln"><br/><br/>
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