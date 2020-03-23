<?php

$xml=simplexml_load_file('dbusers.xml');

echo 'LALALALALALALALALALALALALALALA:<br/>';

foreach($xml->user as $usr)
{
	echo '<p>'.$usr['uname'].'</p>';
	foreach($usr->uvalue as $uvl)
	{
		echo '<p>'.$uvl['name'].'    ';
		echo ($uvl+11).'</p>';
	}
}
?>