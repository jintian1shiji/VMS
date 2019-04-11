<?php
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
error_reporting(E_ALL ^ E_DEPRECATED);
include_once "dbconfig.php";
$ckdb = new dbconfig();
// 假定数据库用户名：root，密码：123456，数据库：nbkss
$con=mysqli_connect($ckdb->bcHost(),$ckdb->bcUser(),$ckdb->bcPass(),$ckdb->bcDbName());
if (mysqli_connect_errno($con))
{
    echo "连接 MySQL 失败: " . mysqli_connect_error();
}
//增
if($_REQUEST['action']=="submit_cm")
{
    $cm_Id = $_REQUEST['cm_Id'];
    $cm_Name = $_REQUEST['cm_Name'];
    $cm_Type = $_REQUEST['cm_Type'];
    $cm_hw = $_REQUEST['cm_hw'];
    
    $parentposX = $_REQUEST['parentposX'];
    $parentposY = $_REQUEST['parentposY'];
    $parentposZ = $_REQUEST['parentposZ'];
    $parentrotX = $_REQUEST['parentrotX'];
    $parentrotY = $_REQUEST['parentrotY'];
    $parentrotZ = $_REQUEST['parentrotZ'];
    $parentscaleX = $_REQUEST['parentscaleX'];
    $parentscaleY = $_REQUEST['parentscaleY'];
    $parentscaleZ = $_REQUEST['parentscaleZ'];
    $cmposX = $_REQUEST['cmposX'];
    $cmposY = $_REQUEST['cmposY'];
    $cmposZ = $_REQUEST['cmposZ'];
    $cmrotX = $_REQUEST['cmrotX'];
    $cmrotY = $_REQUEST['cmrotY'];
    $cmrotZ = $_REQUEST['cmrotZ'];
    $cmscaleX = $_REQUEST['cmscaleX'];
    $cmscaleY = $_REQUEST['cmscaleY'];
    $cmscaleZ = $_REQUEST['cmscaleZ'];
    
    
    $hposX = $_REQUEST['hposX'];
    $hposY = $_REQUEST['hposY'];
    $hposZ = $_REQUEST['hposZ'];
    $hrotX = $_REQUEST['hrotX'];
    $hrotY = $_REQUEST['hrotY'];
    $hrotZ = $_REQUEST['hrotZ'];
    $hscalX = $_REQUEST['hscalX'];
    $hscalY = $_REQUEST['hscalY'];
    $hscalZ = $_REQUEST['hscalZ'];
    $wposX = $_REQUEST['wposX'];
    $wposY = $_REQUEST['wposY'];
    $wposZ = $_REQUEST['wposZ'];
    $wrotX = $_REQUEST['wrotX'];
    $wrotY = $_REQUEST['wrotY'];
    $wrotZ = $_REQUEST['wrotZ'];
    $wscalX = $_REQUEST['wscalX'];
    $wscalY = $_REQUEST['wscalY'];
    $wscalZ = $_REQUEST['wscalZ'];
    
    
    $query = "INSERT INTO `com_zy_camera` (`cm_Id`,`cm_Name`,`cm_Type`,`cm_hw`,`parentposX`,`parentposY`,`parentposZ`,`parentrotX`,`parentrotY`,
`parentrotZ`,`parentscaleX`,`parentscaleY`,`parentscaleZ`,`cmposX`,`cmposY`,`cmposZ`,`cmrotX`,`cmrotY`,`cmrotZ`,`cmscaleX`,`cmscaleY`,`cmscaleZ`,
`hposX`,`hposY`,`hposZ`, `hrotX`,`hrotY`,`hrotZ`,`hscalX`,`hscalY`,`hscalZ`,`wposX`,`wposY`,`wposZ`,`wrotX`,`wrotY`,`wrotZ`,`wscalX`,`wscalY`,`wscalZ`) 
  VALUES ('$cm_Id','$cm_Name','$cm_Type','$cm_hw','$parentposX','$parentposY','$parentposZ','$parentrotX','$parentrotY','$parentrotZ','$parentscaleX',
'$parentscaleY','$parentscaleZ','$cmposX','$cmposY','$cmposZ','$cmrotX','$cmrotY','$cmrotZ','$cmscaleX','$cmscaleY','$cmscaleZ','$hposX','$hposY','$hposZ','$hrotX',
'$hrotY','$hrotZ','$hscalX','$hscalY','$hscalZ','$wposX','$wposY','$wposZ','$wrotX','$wrotY','$wrotZ','$wscalX','$wscalY','$wscalZ')";
    mysqli_query($con,$query);
    echo "Insert " . $cm_Id . " " . $cm_Name;
}

//删，全部
if($_REQUEST['action']=="delete_cmAll")
{
    $query = "DELETE FROM `com_zy_camera`";
    mysqli_query($con,$query);
    echo "Delete All!";
}

//删，指定
if($_REQUEST['action']=="delete_cm")
{
    $cm_Id = $_REQUEST['cm_Id'];
    $query = "DELETE FROM `com_zy_camera` WHERE `cm_Id` = '$cm_Id'";
    mysqli_query($con,$query)	or die(mysqli_error());
    echo "Delete " . $cm_Id;
}

//改，指定
if($_REQUEST['action']=="update_cm")
{
    $cm_Id = $_REQUEST['cm_Id'];
    $cm_Name = $_REQUEST['cm_Name'];
    $cm_Type = $_REQUEST['cm_Type'];
    $cm_hw = $_REQUEST['cm_hw'];
    
    $parentposX = $_REQUEST['parentposX'];
    $parentposY = $_REQUEST['parentposY'];
    $parentposZ = $_REQUEST['parentposZ'];
    $parentrotX = $_REQUEST['parentrotX'];
    $parentrotY = $_REQUEST['parentrotY'];
    $parentrotZ = $_REQUEST['parentrotZ'];
    $parentscaleX = $_REQUEST['parentscaleX'];
    $parentscaleY = $_REQUEST['parentscaleY'];
    $parentscaleZ = $_REQUEST['parentscaleZ'];
    $cmposX = $_REQUEST['cmposX'];
    $cmposY = $_REQUEST['cmposY'];
    $cmposZ = $_REQUEST['cmposZ'];
    $cmrotX = $_REQUEST['cmrotX'];
    $cmrotY = $_REQUEST['cmrotY'];
    $cmrotZ = $_REQUEST['cmrotZ'];
    $cmscaleX = $_REQUEST['cmscaleX'];
    $cmscaleY = $_REQUEST['cmscaleY'];
    $cmscaleZ = $_REQUEST['cmscaleZ'];
    
    $hposX = $_REQUEST['hposX'];
    $hposY = $_REQUEST['hposY'];
    $hposZ = $_REQUEST['hposZ'];
    $hrotX = $_REQUEST['hrotX'];
    $hrotY = $_REQUEST['hrotY'];
    $hrotZ = $_REQUEST['hrotZ'];
    $hscalX = $_REQUEST['hscalX'];
    $hscalY = $_REQUEST['hscalY'];
    $hscalZ = $_REQUEST['hscalZ'];
    $wposX = $_REQUEST['wposX'];
    $wposY = $_REQUEST['wposY'];
    $wposZ = $_REQUEST['wposZ'];
    $wrotX = $_REQUEST['wrotX'];
    $wrotY = $_REQUEST['wrotY'];
    $wrotZ = $_REQUEST['wrotZ'];
    $wscalX = $_REQUEST['wscalX'];
    $wscalY = $_REQUEST['wscalY'];
    $wscalZ = $_REQUEST['wscalZ'];
    
    $query = "UPDATE `com_zy_camera` SET `cm_Name` = '$cm_Name', `cm_Type` = '$cm_Type',`cm_hw` = '$cm_hw',`parentposX` = '$parentposX',`parentposY` = '$parentposY',`parentposZ` = '$parentposZ',
`parentrotX` = '$parentrotX',`parentrotY` = '$parentrotY',`parentrotZ` = '$parentrotZ',`parentscaleX` = '$parentscaleX',`parentscaleY` = '$parentscaleY',`parentscaleZ` = '$parentscaleZ',
`cmposX` = '$cmposX',`cmposY` = '$cmposY',`cmposZ` = '$cmposZ',`cmrotX` = '$cmrotX',`cmrotY` = '$cmrotY',`cmrotZ` = '$cmrotZ',`cmscaleX` = '$cmscaleX',`cmscaleY` = '$cmscaleY',`cmscaleZ` = '$cmscaleZ',`hposX` = '$hposX',`hposY` = '$hposY',`hposZ` = '$hposZ'
,`hrotX` = '$hrotX',`hrotY` = '$hrotY',`hrotZ` = '$hrotZ',`hscalX` = '$hscalX',`hscalY` = '$hscalY',`hscalZ` = '$hscalZ',`wposX` = '$wposX',
`wposY` = '$wposY',`wposZ` = '$wposZ',`wrotX` = '$wrotX',`wrotY` = '$wrotY',`wrotZ` = '$wrotZ',`wscalX` = '$wscalX',`wscalY` = '$wscalY',
`wscalZ` = '$wscalZ' WHERE `cm_Id` = '$cm_Id'";
    mysqli_query($con,$query)	or die(mysqli_error());
    echo "Update " . $cm_Id . " " . $cm_Name;
}

//查
if($_REQUEST['action']=="show_cm")
{
    $query = "SELECT * FROM `com_zy_camera` ORDER BY `cm_Id` ASC";//DESC降序
    $result = mysqli_query($con,$query);
    while($array = mysqli_fetch_array($result))
    {
        echo $array['cm_Id']."</next>";
        echo $array['cm_Name']."</next>";
        echo $array['cm_Type']."</next>";
        echo $array['cm_hw']."</next>";
        
        echo $array['parentposX']."</next>";
        echo $array['parentposY']."</next>";
        echo $array['parentposZ']."</next>";
        echo $array['parentrotX']."</next>";
        echo $array['parentrotY']."</next>";
        echo $array['parentrotZ']."</next>";
        echo $array['parentscaleX']."</next>";
        echo $array['parentscaleY']."</next>";
        echo $array['parentscaleZ']."</next>";
        echo $array['cmposX']."</next>";
        echo $array['cmposY']."</next>";
        echo $array['cmposZ']."</next>";
        echo $array['cmrotX']."</next>";
        echo $array['cmrotY']."</next>";
        echo $array['cmrotZ']."</next>";
        echo $array['cmscaleX']."</next>";
        echo $array['cmscaleY']."</next>";
        echo $array['cmscaleZ']."</next>";
        
        echo $array['hposX']."</next>";
        echo $array['hposY']."</next>";
        echo $array['hposZ']."</next>";
        echo $array['hrotX']."</next>";
        echo $array['hrotY']."</next>";
        echo $array['hrotZ']."</next>";
        echo $array['hscalX']."</next>";
        echo $array['hscalY']."</next>";
        echo $array['hscalZ']."</next>";
        echo $array['wposX']."</next>";
        echo $array['wposY']."</next>";
        echo $array['wposZ']."</next>";
        echo $array['wrotX']."</next>";
        echo $array['wrotY']."</next>";
        echo $array['wrotZ']."</next>";
        echo $array['wscalX']."</next>";
        echo $array['wscalY']."</next>";
        echo $array['wscalZ']."</next>";
        
    }
}
