<?php
//连接数据库
//$conn =new mysqli('localhost','root','','test') or die("连接失败<br/>");
//读取配置文件
$ini= parse_ini_file("test.ini");
$conn =new mysqli($ini["servername"],$ini["username"],$ini["password"],$ini["dbname"]) or die("连接失败<br/>");


//操作数据库
$result=$conn->query("select * from tb1;");

//输出数据
while($row=$result->fetch_assoc()){
    print_r($row);
    echo "<br/>";
}

//关闭数据库
$conn->close();
?>