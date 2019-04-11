<?php
$HOST=$_POST['HOST'];
$DBNAME=$_POST['DBNAME'];
$USER=$_POST['USER'];
$PASS=$_POST['PASS'];

/*
 define('HOST','localhost');
 define('DBNAME','cms');
 define('USER','root');
 define('PASS','123456');
 */
$str='define(\'HOST\',\''.$HOST.'\');'.'define(\'DBNAME\',\''.$DBNAME.'\');'.'define(\'USER\',\''.$USER.'\');'.'define(\'PASS\',\''.$PASS.'\');';
echo $str;
file_put_contents("dbconfig.php", $str);
file_put_contents("check.php", $str);
?>