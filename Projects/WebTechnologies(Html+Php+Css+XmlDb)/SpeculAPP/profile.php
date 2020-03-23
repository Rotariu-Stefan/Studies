<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<title><?php echo $_COOKIE['username']."'s Profile"; ?></title>
<link rel="stylesheet" type="text/css" href="s.css" />
</head>

<body>

<?php	include("Antet.php");
		include("calcule.php");
		
		$users=simplexml_load_file('dbusers.xml');
		$i=GetUid($_COOKIE['username']);

echo "<p class=\"window\">
		First Name: ".$users->user[$i]->fname."<br/>
		Last Name: ".$users->user[$i]->lname."<br/>
		Email: ".$users->user[$i]->email."<br/>
		Age: ".$users->user[$i]->age."<br/>";
if(strcmp($users->user[$i]->sex,'m')==0)
	echo "Man<br/>";
else
	echo "Woman<br/>";
echo	"Total Sum: ".$users->user[$i]->total."</p>";

?>
</body>

</html>