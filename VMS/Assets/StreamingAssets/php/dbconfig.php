<?php
/****************************************************************************
 *    ˵��: �����ݿ�ķ�װ
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

     //��Ա����
    private $host = "";    //���ݿ��ַ
    private $user = "";            //�û���
    private $pwd = "";                //�û�����
    private $db_name = "";        //���ݿ�
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
    //���캯��
    function __construct()
    {
        if(!$this->mysqli = mysqli_connect($this->host, $this->user, $this->pwd))
        {
            die("Cant connect into database");
        }
        else
        {
            
            //echo  "�������ݿ�ɹ�";
        }
        
        $this->select_db($this->db_name);
    } */
    
    //���캯��
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
            
            //echo  "�������ݿ�ɹ�";
        }
        
        $this->select_db($this->db_name);
    }
    
    //��������
    function __destruct()
    {
        mysqli_close($this->mysqli);
    }
    
    /*
     *    ˵��:
     */
    public function get_mysql_handle()
    {
        return $this->mysqli;
    }
    
    /*
     *    ˵��:
     */
    public function select_db($_db)
    {
        if($this->mysqli != null)
        {
            if(mysqli_select_db($this->mysqli, $_db))
            {
                //echo  "�������ݿ�ɹ�";
            }
            else
            {
                die("Cant connect into database");
            }
        }
    }
    
    /*
     *    ˵��:    ִ��һ��sql�޷���ֵ
     */
    public function execute($_sql)
    {
        if(empty($_sql))
        {
            //echo "��������Ϊ��";
            return;
        }
        
        if(!mysqli_query($this->mysqli, $_sql))
        {
            
            //echo  "ִ��ʧ��";
        }
    }
    
    /*
     *    ˵��: ִ��һ����ѯ��䣬��ִ�лص�����
     */
    public function do_query($_sql, $query_callback = "")
    {
        if(empty($_sql))
        {
            //echo  "��������Ϊ��";
            
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
            //echo  "ִ��ʧ��";
        }
    }

}
?>
