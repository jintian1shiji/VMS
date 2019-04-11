<?php
header("content-type:text/html; charset=GB2312");
$str=file_get_contents("dbconfig.php");

$pattern="/define\('(.*)','(.*)'\)/U"; //正则匹配，取消贪婪模式
$arr = array();
preg_match_all($pattern,$str,$arr);

echo '<form action="post.php" method="post">';
foreach ($arr[1] as $key=>$value) {
    
    echo $value.'<input type="text" value='.$arr[2][$key].' name='.$value.'></br>';
}
echo '<input type="submit" value="提交"/>';
echo "</form>";
?>