<?php
/****************************************************************************
 *    ËµÃ÷: µÇÂ¼
 ****************************************************************************/
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
include_once "dbconfig.php";

$dbcfg = new dbconfig();
$password_db = "";

if(isset($_POST["userId"]) && isset($_POST["password"]))
{
    $password = $_POST["password"];
    
    $sql = "select * from user where userId='".$_POST['userId']."'";
    if($dbcfg->do_query($sql, "login_callback") > 0)
    {
        if(md5(md5($password_db)) == $password)
        {
            //echo "µÇÂ¼³É¹¦...".$_POST["userId"].",".$_POST["password"].",".$password_db;
            echo "true";
        }
        else
        {
            //echo "µÇÂ¼Ê§°Ü1...".$_POST["userId"].",".$_POST["password"].",".$password_db;
            echo "false";
        }
    }
    else
    {
        //echo "µÇÂ¼Ê§°Ü2...".$_POST["userId"].",".$_POST["password"].",".$password_db;
        echo "false";
    }
}

function login_callback($row)
{
    global $password_db;
    $password_db = $row["password"];
}
?>
