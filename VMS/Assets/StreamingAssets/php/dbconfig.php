<?php
/****************************************************************************
 *    说明: 对数据库的封装
 ****************************************************************************/
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
define('HOST','localhost');
define('DBNAME','nbkss');
define('USER','root');
define('PASS','123456');

class dbconfig{

     //成员变量
    private $host = "";    //数据库地址
    private $user = "";            //用户名
    private $pwd = "";                //用户密码
    private $db_name = "";        //数据库
     private $mysqli = null; 
    function bcHost()
    {
        return $this->host;
    }
    
    function bcDbName()
    {
        return $this->db_name;
    }
    
    function bcUser()
    {
        return $this->user;
    }
    
    function bcPass()
    {
        return $this->pwd;
    }
    
    /**
    //构造函数
    function __construct()
    {
        if(!$this->mysqli = mysqli_connect($this->host, $this->user, $this->pwd))
        {
            die("Cant connect into database");
        }
        else
        {
            
            //echo  "连接数据库成功";
        }
        
        $this->select_db($this->db_name);
    } */
    
    //构造函数
    function __construct()
    {
        $this->host = constant("HOST");
        $this->user = constant("USER");
        $this->pwd = constant("PASS");
        $this->db_name = constant("DBNAME");
        
        if(!$this->mysqli = mysqli_connect($this->host, $this->user, $this->pwd))
        {
            die("Cant connect into database");
        }
        else
        {
            
            //echo  "连接数据库成功";
        }
        
        $this->select_db($this->db_name);
    }
    
    //析构函数
    function __destruct()
    {
        mysqli_close($this->mysqli);
    }
    
    /*
     *    说明:
     */
    public function get_mysql_handle()
    {
        return $this->mysqli;
    }
    
    /*
     *    说明:
     */
    public function select_db($_db)
    {
        if($this->mysqli != null)
        {
            if(mysqli_select_db($this->mysqli, $_db))
            {
                //echo  "连接数据库成功";
            }
            else
            {
                die("Cant connect into database");
            }
        }
    }
    
    /*
     *    说明:    执行一个sql无返回值
     */
    public function execute($_sql)
    {
        if(empty($_sql))
        {
            //echo "参数不能为空";
            return;
        }
        
        if(!mysqli_query($this->mysqli, $_sql))
        {
            
            //echo  "执行失败";
        }
    }
    
    /*
     *    说明: 执行一个查询语句，并执行回调函数
     */
    public function do_query($_sql, $query_callback = "")
    {
        if(empty($_sql))
        {
            //echo  "参数不能为空";
            
            return;
        }
        
        if($result = mysqli_query($this->mysqli, $_sql))
        {
            $num_rows = $result->num_rows;
            if($num_rows > 0)
            {
                while($row = $result->fetch_assoc())
                {
                    if(!empty($query_callback))
                    {
                        call_user_func( $query_callback , $row );
                    }
                }
                
                return $num_rows;
            }
            else
            {
                return 0;
            }
            
            mysqli_free_result($result);
        }
        else
        {
            //echo  "执行失败";
        }
    }

}
?>
