<?php
//�������ݿ�
//$conn =new mysqli('localhost','root','','test') or die("����ʧ��<br/>");
//��ȡ�����ļ�
$ini= parse_ini_file("test.ini");
$conn =new mysqli($ini["servername"],$ini["username"],$ini["password"],$ini["dbname"]) or die("����ʧ��<br/>");


//�������ݿ�
$result=$conn->query("select * from tb1;");

//�������
while($row=$result->fetch_assoc()){
    print_r($row);
    echo "<br/>";
}

//�ر����ݿ�
$conn->close();
?>